using System.Collections.Generic;
using UnityEngine.UI;

public class TalentUpgradeModel 
{
    public void Upgrade(string talentName,  int amountPoints, Button button)
    {
        TalentEvents.current.OnTalentUpgrade(talentName, amountPoints, button);
    }

    public void RemoveUpgrade(string talentName,  int amountPoints, Button button)
    {
        TalentEvents.current.OnTalentRemove(talentName, amountPoints, button);
    }

    public void RemoveAllTalents(Dictionary<string, TalentData> talentsData)
    {
        foreach (var talentPair in TalentsData.current.buttonTalentPairs)
        {
            if (talentsData.ContainsKey(talentPair.talent.talentName))
            {
                talentsData[talentPair.talent.talentName] = talentPair.talent;
                TalentEvents.current.OnGameStart(talentPair.talent.talentName, talentPair.talent.initialState,
                    talentPair.button);
            }
        }
        
    }

}
