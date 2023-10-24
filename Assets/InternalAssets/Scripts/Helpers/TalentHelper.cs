public class TalentHelper
{
    public static bool HasUpgradedDependents(TalentData talent, TalentModel _talentModel)
    {
        foreach (var pair in TalentsData.current.buttonTalentPairs)
        {
            var dependentTalent = pair.talent;
            if (dependentTalent.prerequisites != null && dependentTalent.prerequisites.Length > 0)
            {
                foreach (var prerequisite in dependentTalent.prerequisites)
                {
                    if (prerequisite == talent &&
                        _talentModel.talentsStates[dependentTalent.talentName] == TalentState.Upgraded)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public static bool CanUpgradeTalent(TalentData talent, TalentModel _talentModel)
    {
        if (talent.prerequisites != null && talent.prerequisites.Length > 0)
        {
            foreach (var prereq in talent.prerequisites)
            {
                if (!_talentModel.talentsStates.ContainsKey(prereq.talentName) ||
                    _talentModel.talentsStates[prereq.talentName] != TalentState.Upgraded)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static void ActivateDependentTalents(string upgradedTalentName, TalentModel _talentModel,
        TalentBorderView _talentBorderView)
    {
        foreach (var pair in TalentsData.current.buttonTalentPairs)
        {
            var talent = pair.talent;

            if (talent.prerequisites != null && talent.prerequisites.Length > 0)
            {
                bool allPrerequisitesMet = true;

                foreach (var prerequisite in talent.prerequisites)
                {
                    if (_talentModel.talentsStates.TryGetValue(prerequisite.talentName, out TalentState state))
                    {
                        if (state != TalentState.Upgraded)
                        {
                            allPrerequisitesMet = false;
                            break;
                        }
                    }
                    else
                    {
                        allPrerequisitesMet = false;
                        break;
                    }
                }

                if (allPrerequisitesMet)
                {
                    if (_talentModel.talentsStates[talent.talentName] == TalentState.Inactive)
                    {
                        _talentModel.talentsStates[talent.talentName] = TalentState.Active;
                        _talentBorderView.ChangeBorder(pair.button, TalentState.Active);
                    }
                }
            }
        }
    }
}