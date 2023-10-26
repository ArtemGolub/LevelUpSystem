using UnityEngine;

public class TalentValidatorModel
{
    public bool IsTalentActionValid(TalentData talent, TalentData currentlySelectedTalent, TalentState _currentlySelectedTalentState )
    {
        if (talent == null || talent.talentName == "Main") return false;
        if (IsTalentUpgradedSelected(currentlySelectedTalent, _currentlySelectedTalentState) && currentlySelectedTalent != talent)
        {
            Debug.LogWarning("An upgraded talent is already selected. Please interact with it first.");
            return false;
        }

        return true;
    }
    
    private bool IsTalentUpgradedSelected(TalentData _currentlySelectedTalent, TalentState _currentlySelectedTalentState)
    {
        return _currentlySelectedTalent != null && _currentlySelectedTalentState == TalentState.Upgraded;
    }
}