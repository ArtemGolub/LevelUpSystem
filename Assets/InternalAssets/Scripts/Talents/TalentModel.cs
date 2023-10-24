
using System.Collections.Generic;

public class TalentModel
{
    public TalentState State;
    public Dictionary<string, TalentState> talentsStates = new Dictionary<string, TalentState>();
    
    public void UpgradeTalent(string talentName)
    {
        if (talentsStates.TryGetValue(talentName, out TalentState state) && state == TalentState.Selected)
        {
            talentsStates[talentName] = TalentState.Upgraded;
        }
    }
}

