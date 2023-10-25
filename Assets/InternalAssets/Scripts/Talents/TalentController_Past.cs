using UnityEngine;
using UnityEngine.UI;

public class TalentController_Past : MonoBehaviour
{
    private TalentModel_Past _talentModelPast;
    private ModelButtonTalents _modelButton;
    
    private ITalentButtonsView _buttonsView;
    private ITalentView_Past _talentViewPast;
    private ITalentButtonView _talentButtonView;
    
    private TalentData _currentlySelectedTalent;
    private Button _currentlySelectedTalentButton;
    public TalentState prevTalentState;

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
        _modelButton = new ModelButtonTalents();
        _buttonsView = GetComponent<ITalentButtonsView>();
        _buttonsView.Init(this);
        
        InitTalentBorderView();
        InitTalentView();
        InitTalentModel();
    }
    
    public void HandleButtonPress(TalentData talent, Button talentButton)
    {
      //  TalentValidator.IsTalentActionValid(talent, _talentModelPast, _currentlySelectedTalent);
        
        if (_talentModelPast.talentsStates.TryGetValue(talent.talentName, out TalentState state))
        {
            switch (state)
            {
                case TalentState.Active:
                {
                    if (_talentModelPast.IsAnyTalentSelected())
                    {
                        Debug.LogWarning("Other talent selected");
                        return;
                    }
                   // if (!TalentValidator.CanUpgradeTalent(talent, _talentModelPast)) return;
                    prevTalentState = _talentModelPast.talentsStates[talent.talentName];
                    
                    _talentModelPast.talentsStates[talent.talentName] = TalentState.Selected;
                    _talentButtonView.ChangeBorder(talentButton, _talentModelPast.talentsStates[talent.talentName]);
                    
                    _currentlySelectedTalent = talent;
                    _currentlySelectedTalentButton = talentButton;
                    
                    _talentViewPast.ShowConfirm(true);

                    Debug.Log("Active: " + talent.name);
                    break;
                }
                case TalentState.Inactive:
                {
                    prevTalentState = _talentModelPast.talentsStates[talent.talentName];
                    
                    Debug.Log("Unable to upgrade: " + talent.name);
                    break;
                }
                case TalentState.Selected:
                {
                    _talentModelPast.talentsStates[talent.talentName] = prevTalentState;
                    _talentButtonView.ChangeBorder(talentButton, _talentModelPast.talentsStates[talent.talentName]);
                    _talentViewPast.HideButtons();
                    Debug.Log("Selected");
                    break;
                }
                case TalentState.Upgraded:
                {
                    Debug.Log(talent.name+ " Select upgraded");
                    prevTalentState = _talentModelPast.talentsStates[talent.talentName];

                    _talentModelPast.talentsStates[talent.talentName] = TalentState.Selected;
                    _talentButtonView.ChangeBorder(talentButton, _talentModelPast.talentsStates[talent.talentName]);
                    
                    _currentlySelectedTalent = talent;
                    _currentlySelectedTalentButton = talentButton;
                    
                    _talentViewPast.ShowCancel(true);
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
            if (_modelButton.currentTalentPoints < _currentlySelectedTalent.cost)
            {
                Debug.LogError("not enough currency");
                
                _talentModelPast.ResetTalent(_currentlySelectedTalent.talentName, this);
                _talentButtonView.ChangeBorder(_currentlySelectedTalentButton, _talentModelPast.talentsStates[_currentlySelectedTalent.talentName]);
                
                _currentlySelectedTalent = null;
                _currentlySelectedTalentButton = null;
                
                _talentViewPast.HideButtons();
                return;
            }
            
            RemoveTalentPoint(_currentlySelectedTalent.cost);
            
            _talentModelPast.UpgradeTalent(_currentlySelectedTalent.talentName);
            _talentButtonView.ChangeBorder(_currentlySelectedTalentButton, _talentModelPast.talentsStates[_currentlySelectedTalent.talentName]);
          //  TalentHelper.ActivateDependentTalents(_currentlySelectedTalent.talentName, _talentModelPast, _talentButtonView);
            
            _currentlySelectedTalent = null;
            _currentlySelectedTalentButton = null;
            
            _talentViewPast.HideButtons();
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
            if (_talentModelPast.talentsStates[_currentlySelectedTalent.talentName] == TalentState.Selected && prevTalentState == TalentState.Upgraded)
            {
              //  if (!TalentValidator.HasUpgradedDependents(_currentlySelectedTalent, _talentModelPast))
                {
                    _talentModelPast.talentsStates[_currentlySelectedTalent.talentName] = TalentState.Active;
                    _talentButtonView.ChangeBorder(_currentlySelectedTalentButton, TalentState.Active);
                    ReciveTalentPoint(_currentlySelectedTalent.cost);
                }
              //  else
                {
                    _talentModelPast.talentsStates[_currentlySelectedTalent.talentName] = prevTalentState;
                    _talentButtonView.ChangeBorder(_currentlySelectedTalentButton, prevTalentState);
                    Debug.LogWarning("Cannot downgrade the talent as there are dependent talents upgraded.");
                }
            }
        
            _currentlySelectedTalent = null;
            _currentlySelectedTalentButton = null;
            
            _talentViewPast.HideButtons();
        }
        else
        {
            Debug.LogWarning("No talent is currently selected.");
        }
    }

    private void ResetAllTalents()
    {
        foreach (var pair in TalentsData.current.buttonTalentPairs)
        {
            if (_talentModelPast.talentsStates.ContainsKey(pair.talent.talentName))
            {
                _talentButtonView.ChangeBorder(pair.button, pair.talent.initialState);
                ReciveTalentPoint(pair.talent.cost);
            }
            else
            {
                Debug.LogError("no key value");
            }
        }
        _talentModelPast.ResetAllTalents();
        _currentlySelectedTalent = null;
        _currentlySelectedTalentButton = null;
        _talentViewPast.HideButtons();
    }
    
    private void InitTalentView()
    {
        _talentViewPast = GetComponent<ITalentView_Past>();
        _talentViewPast.Init(this);
        _talentViewPast.RegisterTalents(TalentsData.current.buttonTalentPairs);
        _talentViewPast.HideButtons();
    }

    private void InitTalentBorderView()
    {
        _talentButtonView = GetComponent<TalentButtonView>();
    }

    private void InitTalentModel()
    {
        _talentModelPast = new TalentModel_Past();
        foreach (var pair in TalentsData.current.buttonTalentPairs)
        {
            if (!_talentModelPast.talentsStates.ContainsKey(pair.talent.talentName))
            {
                _talentModelPast.talentsStates[pair.talent.talentName] = pair.talent.initialState;
                _talentButtonView.ChangeBorder(pair.button, pair.talent.initialState);
                _talentModelPast.talentsDataMap[pair.talent.talentName] = pair.talent;
            }
        }
    }
    
    public void AddTalentPoint()
    {
        _modelButton.AddTalentPoint();
        _buttonsView.UpdateText(_modelButton.GetTalentPoints());
    }
    
    private void ReciveTalentPoint(int amount)
    {
        _modelButton.ReciveTalentPoint(amount);   
        _buttonsView.UpdateText(_modelButton.GetTalentPoints());
    }

    public void RemoveAllPoints()
    {
        ResetAllTalents();
        _buttonsView.UpdateText(_modelButton.GetTalentPoints());
    }

    private void RemoveTalentPoint(int amount)
    {
        if ((!_modelButton.RemoveTalentEnable(amount))) return;
        _modelButton.RemoveTalentPoint(amount);
        _buttonsView.UpdateText(_modelButton.GetTalentPoints());
    }
}