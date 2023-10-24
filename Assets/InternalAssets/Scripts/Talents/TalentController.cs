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
        
        InitMVC();
    }

    private void InitMVC()
    {
        InitTalentBorderView();
        InitTalentView();
        InitTalentModel();
    }
    
    public void HandleButtonPress(TalentData talent, Button talentButton)
    {
        if (talent == null) return;
        
        if(talent.talentName == "Main") return;
        if (_talentModel.IsTalentUpgradedSelected(_currentlySelectedTalent) && _currentlySelectedTalent != talent)
        {
            Debug.LogWarning("An upgraded talent is already selected. Please interact with it first.");
            return;
        }

        if (_talentModel.IsAnyTalentSelected())
        {
            Debug.LogWarning("Other talent selected");
            return;
        }
        
        if (_talentModel.talentsStates.TryGetValue(talent.talentName, out TalentState state))
        {
            switch (state)
            {
                case TalentState.Active:
                {
                    if (!TalentHelper.CanUpgradeTalent(talent, _talentModel)) return;
                    _talentModel.talentsStates[talent.talentName] = TalentState.Selected;
                    _talentBorderView.ChangeBorder(talentButton, _talentModel.talentsStates[talent.talentName]);
                    
                    _currentlySelectedTalent = talent;
                    _currentlySelectedTalentButton = talentButton;
                    
                    _talentView.ShowConfirm(true);

                    Debug.Log("Active: " + talent.name);
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

    public void HandleUpgradeButtonPress()
    {
        if (_currentlySelectedTalent != null)
        {
            _talentModel.UpgradeTalent(_currentlySelectedTalent.talentName);
            _talentBorderView.ChangeBorder(_currentlySelectedTalentButton, _talentModel.talentsStates[_currentlySelectedTalent.talentName]);
            TalentHelper.ActivateDependentTalents(_currentlySelectedTalent.talentName, _talentModel, _talentBorderView);
            
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
                if (!TalentHelper.HasUpgradedDependents(_currentlySelectedTalent, _talentModel))
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
}