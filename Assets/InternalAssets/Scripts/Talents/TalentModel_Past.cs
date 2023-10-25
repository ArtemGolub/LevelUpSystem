
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TalentModel_Past
{
    public Dictionary<string, TalentState> talentsStates = new Dictionary<string, TalentState>();
    public Dictionary<string, TalentData> talentsDataMap = new Dictionary<string, TalentData>();
    
    public bool IsAnyTalentSelected()
    {
        foreach (var state in talentsStates.Values)
        {
            if (state == TalentState.Selected)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsTalentUpgradedSelected(TalentData _currentlySelectedTalent)
    {
        return _currentlySelectedTalent != null && talentsStates[_currentlySelectedTalent.talentName] == TalentState.Upgraded;
    }
    
    public void UpgradeTalent(string talentName)
    {
        if (talentsStates.TryGetValue(talentName, out TalentState state) && state == TalentState.Selected)
        {
            talentsStates[talentName] = TalentState.Upgraded;
        }
    }
    public void ResetTalent(string talentName, TalentController_Past controllerPast)
    {
        if (talentsStates.TryGetValue(talentName, out TalentState state) && state == TalentState.Selected)
        {
            talentsStates[talentName] = controllerPast.prevTalentState;
        }
    }

    public void ResetAllTalents()
    {
        foreach (var talentName in talentsStates.Keys.ToList())
        {
            if (talentsDataMap.TryGetValue(talentName, out TalentData talentData))
            {
                talentsStates[talentName] = talentData.initialState;
            }
            else
            {
                Debug.LogWarning($"No TalentData found for {talentName}.");
            }
        }
    }
}

