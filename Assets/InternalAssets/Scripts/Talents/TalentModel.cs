
using System.Collections.Generic;

public class TalentModel
{
    public TalentState State;
    public Dictionary<string, TalentState> talentsStates = new Dictionary<string, TalentState>();
    
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
        return _currentlySelectedTalent != null &&
               talentsStates[_currentlySelectedTalent.talentName] == TalentState.Upgraded;
    }
    
    public void UpgradeTalent(string talentName)
    {
        if (talentsStates.TryGetValue(talentName, out TalentState state) && state == TalentState.Selected)
        {
            talentsStates[talentName] = TalentState.Upgraded;
        }
    }
}

