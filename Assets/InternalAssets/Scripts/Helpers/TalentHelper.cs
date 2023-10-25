using System.Collections.Generic;

public class TalentHelper
{
  public static void ActivateDependentTalents(Dictionary<string, TalentState> talentsStates)
    {
        foreach (var pair in TalentsData.current.buttonTalentPairs)
        {
            var talent = pair.talent;

            if (talent.prerequisites != null && talent.prerequisites.Length > 0)
            {
                bool allPrerequisitesMet = false;

                foreach (var prerequisite in talent.prerequisites)
                {
                    if (talentsStates.TryGetValue(prerequisite.talentName, out TalentState state))
                    {
                        if (state == TalentState.Upgraded)
                        {
                            allPrerequisitesMet = true;
                            break;
                        }
                    }
                    else
                    {
                        TalentEvents.current.ChangeTileState.Invoke(talent.talentName, TalentState.Inactive);
                        TalentEvents.current.ChangeButtonBorder.Invoke(pair.button, TalentState.Inactive);
                        TalentEvents.current.ResetCurTalent.Invoke();
                    }
                }

                if (allPrerequisitesMet)
                {
                    if (talentsStates[talent.talentName] == TalentState.Inactive)
                    {
                        TalentEvents.current.ChangeTileState.Invoke(talent.talentName, TalentState.Active);
                        TalentEvents.current.ChangeButtonBorder.Invoke(pair.button, TalentState.Active);
                        TalentEvents.current.ResetCurTalent.Invoke();
                    }
                }
                else
                {
                    TalentEvents.current.ChangeTileState.Invoke(talent.talentName, TalentState.Inactive);
                    TalentEvents.current.ChangeButtonBorder.Invoke(pair.button, TalentState.Inactive);
                    TalentEvents.current.ResetCurTalent.Invoke();
                }
            }
        }
    }
}