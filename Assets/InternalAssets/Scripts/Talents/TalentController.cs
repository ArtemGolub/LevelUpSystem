using UnityEngine;
using UnityEngine.UI;

// TODO Refactor this
public class TalentController : MonoBehaviour
{
    private TalentModel _talentModel;
    private ITalentView _talentView;
    private TalentBorderView _talentBorderView;
    
    private TalentData _currentlySelectedTalent;
    private Button _currentlySelectedTalentButton;

    private void Start()
    {
        if (TalentsData.current == null)
        {
            Debug.LogWarning("No Talents Data");
            return;
        }
        InitTalentBorderView();
        InitTalentView();
        InitTalentModel();
    }

    
    private void InitTalentView()
    {
        _talentView = GetComponent<ITalentView>();
        _talentView.Init(this);
        _talentView.RegisterTalents(TalentsData.current.buttonTalentPairs);
        
        _talentView.HideButtons();
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
                _talentBorderView.ChangeBorder(pair.button, pair.talent.initialState);
            }
        }
    }
    
    public void HandleButtonPress(TalentData talent, Button talentButton)
    {
        if (talent == null) return;
        
        if(talent.talentName == "Main") return;
        if (IsTalentUpgradedSelected() && _currentlySelectedTalent != talent)
        {
            Debug.LogWarning("An upgraded talent is already selected. Please interact with it first.");
            return;
        }

        if (IsAnyTalentSelected())
        {
            Debug.LogWarning("Selected talent");
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
                    
                    _currentlySelectedTalent = talent;
                    _currentlySelectedTalentButton = talentButton;
                    
                    _talentView.ShowConfirm(true);

                    Debug.Log("Active");
                    break;
                }
                case TalentState.Inactive:
                {
                    Debug.Log("Unable to upgrade: " + talent.name);
                    break;
                }
                case TalentState.Selected:
                {
                    _talentModel.talentsStates[talent.talentName] = TalentState.Active;
                    _talentBorderView.ChangeBorder(talentButton, _talentModel.talentsStates[talent.talentName]);
                    _talentView.HideButtons();
                    Debug.Log("Selected");
                    break;
                }
                case TalentState.Upgraded:
                {
                    Debug.Log(talent.name+ " Select upgraded");
                    _currentlySelectedTalent = talent;
                    _currentlySelectedTalentButton = talentButton;
                    
                    _talentView.ShowCancel(true);
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("Talent " + talent.talentName + " not found in the talentsStates dictionary.");
        }

    }
    private bool IsTalentUpgradedSelected()
    {
        return _currentlySelectedTalent != null && _talentModel.talentsStates[_currentlySelectedTalent.talentName] == TalentState.Upgraded;
    }
    public void HandleUpgradeButtonPress()
    {
        if (_currentlySelectedTalent != null)
        {
            _talentModel.UpgradeTalent(_currentlySelectedTalent.talentName);
            _talentBorderView.ChangeBorder(_currentlySelectedTalentButton, _talentModel.talentsStates[_currentlySelectedTalent.talentName]);
            ActivateDependentTalents(_currentlySelectedTalent.talentName);
            
            _currentlySelectedTalent = null;
            _currentlySelectedTalentButton = null;
            
            _talentView.HideButtons();
        }
        else
        {
            Debug.LogWarning("No talent is currently selected.");
        }
    }
    
    public void HandleCancelButtonPress()
    {
        if (_currentlySelectedTalent != null)
        {
            if (_talentModel.talentsStates[_currentlySelectedTalent.talentName] == TalentState.Upgraded)
            {
                if (!HasUpgradedDependents(_currentlySelectedTalent))
                {
                    _talentModel.talentsStates[_currentlySelectedTalent.talentName] = TalentState.Active;
                    _talentBorderView.ChangeBorder(_currentlySelectedTalentButton, TalentState.Active);
                }
                else
                {
                    Debug.LogWarning("Cannot downgrade the talent as there are dependent talents upgraded.");
                }
            }
        
            _currentlySelectedTalent = null;
            _currentlySelectedTalentButton = null;
            
            _talentView.HideButtons();
        }
        else
        {
            Debug.LogWarning("No talent is currently selected.");
        }
    }
    
    private bool HasUpgradedDependents(TalentData talent)
    {
        foreach (var pair in TalentsData.current.buttonTalentPairs)
        {
            var dependentTalent = pair.talent;
            if (dependentTalent.prerequisites != null && dependentTalent.prerequisites.Length > 0)
            {
                foreach (var prerequisite in dependentTalent.prerequisites)
                {
                    if (prerequisite == talent && _talentModel.talentsStates[dependentTalent.talentName] == TalentState.Upgraded)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
    
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

