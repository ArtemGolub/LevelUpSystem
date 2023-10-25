using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentHandler
{
    private readonly TalentModel_Past _talentModelPast;
    private readonly TalentButtonView _talentButtonView;
    private readonly ITalentView_Past _talentViewPast;
    
    public TalentHandler(TalentModel_Past talentModelPast, TalentButtonView talentButtonView, ITalentView_Past talentViewPast)
    {
        _talentModelPast = talentModelPast;
        _talentButtonView = talentButtonView;
        _talentViewPast = talentViewPast;
    }

    public void ActiveTalentHandle(TalentData talent, Button talentButton)
    {
        if (_talentModelPast.IsAnyTalentSelected())
        {
            Debug.LogWarning("Other talent selected");
            return;
        }
   //     if (!TalentValidator.CanUpgradeTalent(talent, _talentModelPast)) return;
        
        _talentModelPast.talentsStates[talent.talentName] = TalentState.Selected;
        _talentButtonView.ChangeBorder(talentButton, _talentModelPast.talentsStates[talent.talentName]);
        _talentViewPast.ShowConfirm(true);

        Debug.Log("Active: " + talent.name);
    }

    public void InactiveTalentHandle(TalentData talent, Button talentButton)
    {
        
    }
    
    public void SelectedTalentHandle(TalentData talent, Button talentButton)
    {
       
        _talentButtonView.ChangeBorder(talentButton, _talentModelPast.talentsStates[talent.talentName]);
        _talentViewPast.HideButtons();
    }
    
    public void UpgradedTalentHandle(TalentData talent, Button talentButton)
    {
        
        _talentModelPast.talentsStates[talent.talentName] = TalentState.Selected;
        _talentButtonView.ChangeBorder(talentButton, _talentModelPast.talentsStates[talent.talentName]);
    }
    
}
