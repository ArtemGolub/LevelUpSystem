using System.Collections.Generic;

public class TalentStateModel
{
    private TalentState prevTalentState;
    public Dictionary<string, TalentState> talentsStates = new Dictionary<string, TalentState>();
    public TalentState GetPrevTalentState()
    {
        return prevTalentState;
    }

    public void SetPrevTalentState(TalentState prevTalent)
    {
        prevTalentState = prevTalent;
    }
    
    public TalentState GetTalentState(string talentName)
    {
        if (talentsStates.ContainsKey(talentName))
        {
            return talentsStates[talentName];
        }

        return TalentState.Inactive;
    }

    public void SetTalentDictionary()
    {
        foreach (var talentPair in TalentsData.current.buttonTalentPairs)
        {
            if (!talentsStates.ContainsKey(talentPair.talent.talentName))
            {
                talentsStates[talentPair.talent.talentName] = talentPair.talent.initialState;
            }
        }
    }
    
    private void ResetTalentStates()
    {
        foreach (var talentPair in TalentsData.current.buttonTalentPairs)
        {
            if (talentsStates.ContainsKey(talentPair.talent.talentName))
            {
                talentsStates[talentPair.talent.talentName] = talentPair.talent.initialState;
            }
        }
    }

    private void ActivateDependentTalent()
    {
        TalentHelper.ActivateDependentTalents(talentsStates);
    }

    private void ChangeTalentState(string talentName, TalentState state)
    {
        talentsStates[talentName] = state;
    }
    
    private void ResetTalent(string talentName)
    {
        talentsStates[talentName] = prevTalentState;
    }

    public void AddListeners()
    {
        ResetAllTalents();
        ResetTalent();
        ChangeTileState();
        ActivateDependentTalents();
    }

    private void ActivateDependentTalents()
    {
        TalentEvents.current.ActivateDependentTalents.AddListener(ActivateDependentTalent);
    }
    
    private void ResetAllTalents()
    {
        TalentEvents.current.AllTalentsReset.AddListener(ResetTalentStates);
    }

    private void ResetTalent()
    {
        TalentEvents.current.TalentReset.AddListener(ResetTalent);
    }

    private void ChangeTileState()
    {

        TalentEvents.current.ChangeTileState.AddListener(ChangeTalentState);

    }
}