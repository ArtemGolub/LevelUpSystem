using System.Collections.Generic;

public class TalentModel
{
    public Dictionary<string, TalentData> talentsData = new Dictionary<string, TalentData>();
    
    public void HandleGameStart()
    {
        foreach (var talentPair in TalentsData.current.buttonTalentPairs)
        {
            if (talentsData.ContainsKey(talentPair.talent.talentName))
            {
                talentsData[talentPair.talent.talentName] = talentPair.talent;
                TalentEvents.current.OnGameStart(talentPair.talent.talentName, talentPair.talent.initialState, talentPair.button, talentPair.talent);
            }
        }
    }
    public void SetTalentDictionary()
    {
        foreach (var talentPair in TalentsData.current.buttonTalentPairs)
        {
            if (!talentsData.ContainsKey(talentPair.talent.talentName))
            {
                talentsData[talentPair.talent.talentName] = talentPair.talent;
                
            }
        }
    }
}