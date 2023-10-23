using UnityEngine;
using UnityEngine.UI;

// TODO Refactor this
// create regions for Unity methods and inits
public class TalentController : MonoBehaviour
{
    private TalentModel _talentModel;
    private ITalentView _talentView;
    private TalentBorderView _talentBorderView;

    private void Start()
    {
        if (TalentsData.current == null)
        {
            Debug.LogWarning("No Talents Data");
            return;
        }
           
        InitTalentModel();
        InitTalentView();
        InitTalentBorderView();
    }

    
    private void InitTalentView()
    {
        _talentView = GetComponent<ITalentView>();
        _talentView.Init(this);
        _talentView.RegisterTalents(TalentsData.current.buttonTalentPairs);
    }

    private void InitTalentBorderView()
    {
        _talentBorderView = GetComponent<TalentBorderView>();
        _talentBorderView.Init(this);
    }

    private void InitTalentModel()
    {
        _talentModel = new TalentModel();
        foreach (var pair in TalentsData.current.buttonTalentPairs)
        {
            if (!_talentModel.talentsStates.ContainsKey(pair.talent.talentName))
            {
                _talentModel.talentsStates[pair.talent.talentName] = pair.talent.initialState;
            }
        }
    }
    // TODO new MVC for buttons Upgrade | Remove Upgrade
    public void HandleButtonPress(TalentData talent, Button talentButton)
    {
        if (talent == null) return;
        if (IsAnyTalentSelected())
        {
            Debug.LogWarning("Another talent is already selected. Unselect first.");
            return;
        }
        
        if (_talentModel.talentsStates.TryGetValue(talent.talentName, out TalentState state))
        {
            switch (state)
            {
                case TalentState.Active:
                {
                    if (!CanUpgradeTalent(talent)) return;
                    _talentModel.talentsStates[talent.talentName] = TalentState.Selected;
                    _talentBorderView.ChangeBorder(talentButton, _talentModel.talentsStates[talent.talentName]);
                    break;
                }
                case TalentState.Inactive:
                {
                    Debug.Log("Unable to upgrade: " + talent.name);
                    break;
                }
                case TalentState.Selected:
                {
                    _talentModel.talentsStates[talent.talentName] = TalentState.Upgraded;
                    _talentBorderView.ChangeBorder(talentButton, _talentModel.talentsStates[talent.talentName]);
                    ActivateDependentTalents(talent.name);
                    break;
                }
                case TalentState.Upgraded:
                {
                    Debug.Log(talent.name+ " Already upgraded");
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("Talent " + talent.talentName + " not found in the talentsStates dictionary.");
        }

    }

// TODO think where can replace
    private bool CanUpgradeTalent(TalentData talent)
    {
        if (talent.prerequisites != null && talent.prerequisites.Length > 0)
        {
            foreach (var prereq in talent.prerequisites)
            {
                if (!_talentModel.talentsStates.ContainsKey(prereq.talentName) ||
                    _talentModel.talentsStates[prereq.talentName] != TalentState.Upgraded)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    // TODO think how to change
    private void ActivateDependentTalents(string upgradedTalentName)
    {
        foreach (var pair in TalentsData.current.buttonTalentPairs)
        {
            var talent = pair.talent;
            
            if (talent.prerequisites != null && talent.prerequisites.Length > 0)
            {
                bool allPrerequisitesMet = true;
                
                foreach (var prerequisite in talent.prerequisites)
                {
                    if (_talentModel.talentsStates.TryGetValue(prerequisite.talentName, out TalentState state))
                    {
                        if (state != TalentState.Upgraded)
                        {
                            allPrerequisitesMet = false;
                            break;
                        }
                    }
                    else
                    {
                        allPrerequisitesMet = false;
                        break; 
                    }
                }

                if (allPrerequisitesMet)  
                {
                    if (_talentModel.talentsStates[talent.talentName] == TalentState.Inactive)
                    {
                        _talentModel.talentsStates[talent.talentName] = TalentState.Active;
                        _talentBorderView.ChangeBorder(pair.button, TalentState.Active);
                    }
                }
            }
        }
    }
 
    // TODO think where can replace
    private bool IsAnyTalentSelected()
    {
        foreach (var state in _talentModel.talentsStates.Values)
        {
            if (state == TalentState.Selected)
            {
                return true;
            }
        }
        return false;
    }

}

