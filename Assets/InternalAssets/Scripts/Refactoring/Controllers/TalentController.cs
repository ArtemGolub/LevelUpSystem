using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentController : MonoBehaviour
{
    public event Func<TalentData, Button,  Dictionary<string, TalentState>,Dictionary<string, TalentData>, bool> OnTalentPressed;

    private TalentStateModel _talentStateModel;
    private TalentStateView _talentStateView;
    
    private TalentModel _talentModel;
    private TalentView _talentView;
    
    private void Start()
    {
        InitTalents();
        InitTalentStates();
        _talentModel.HandleGameStart();
    }
    
    public void HandleTalentPress(TalentData talent, Button button)
    {
        bool isValid = OnTalentPressed?.Invoke(talent, button, _talentStateModel.talentsStates, _talentModel.talentsData) ?? true;
        if(!isValid) return;
        
        var currentState = _talentStateModel.GetTalentState(talent.talentName);
        switch (currentState)
        {
            case TalentState.Active:
            {
                _talentStateModel.SetPrevTalentState(currentState);
                TalentEvents.current.OnTalentSelect(talent.name, TalentState.Selected, button, _talentStateModel.GetPrevTalentState());
                break;
            }
            case TalentState.Inactive:
            {
                Debug.Log("Inactive");
                break;
            }
            case TalentState.Selected:
            {
                TalentEvents.current.OnTalentSelect(talent.name, _talentStateModel.GetPrevTalentState(), button, _talentStateModel.GetPrevTalentState());
                TalentEvents.current.HideConfrimUI.Invoke();
                break;
            }
            case TalentState.Upgraded:
            {
                _talentStateModel.SetPrevTalentState(currentState);
                TalentEvents.current.OnTalentSelect(talent.name, TalentState.Selected, button, _talentStateModel.GetPrevTalentState());
                break;
            }
        }
    }
    private void InitTalents()
    {
        _talentModel = new TalentModel();
        _talentModel.SetTalentDictionary();
        _talentView = GetComponent<TalentView>();
        _talentView.Init(this);
    }

    private void InitTalentStates()
    {
        _talentStateModel = new TalentStateModel();
        _talentStateModel.SetTalentDictionary();
        _talentStateModel.AddListeners();
        _talentStateView = GetComponent<TalentStateView>();
    }
}