using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentValidator : MonoBehaviour
{
    public TalentController TalentController;
    public TalentUpgradeController TalentUpgradeController;
    public TalentPointController TalentPointController;

    private TalentData curTalent;
    private Button curButton;
    private Dictionary<string, TalentData> curData;

    private void Start()
    {
        if (TalentController != null)
        {
            TalentController.OnTalentPressed += ValidateTalent;
        }

        if (TalentUpgradeController != null)
        {
            TalentUpgradeController.GetCurrentTalent += GetCurrentTalent;
            TalentUpgradeController.GetCurrentButton += GetCurrentbutton;
            TalentUpgradeController.GetCurrentData += GetCurrentData;
        }

        TalentEvents.current.ResetCurTalent.AddListener(ResetCurrentTalent);
    }

    private void ResetCurrentTalent()
    {
         curTalent = null;
         curButton = null;
    }


    private TalentData GetCurrentTalent()
    {
        return curTalent;
    }

    private Button GetCurrentbutton()
    {
        return curButton;
    }

    private Dictionary<string, TalentData> GetCurrentData()
    {
        return curData;
    }

    private bool ValidateTalent(TalentData talent, Button button, Dictionary<string, TalentState> talentsStates, Dictionary<string, TalentData> data)
    {
        InactiveCheck(talentsStates);
        if (talentsStates[talent.talentName] == TalentState.Upgraded)
        {
            if (!CanDeactivateTalent(talent, talentsStates))
            {
                Debug.LogError("Cant Deactivate");
                return false;
            }
        }
        else
        {
            if (HasUpgradedDependents(talent, talentsStates))
            {
                Debug.LogError("Has Upgraded Dependents");
                return false;
            }
            if (EnoughPoints(talent))
            {
                Debug.LogError("Not Enough Points");
                return false;
            }
        }
        
        if (IsAnyTalentSelected(talentsStates, talent, button))
        {
            Debug.LogError("Trying to get: " + talent.talentName);
            return false;
        }
        if (talent.talentName == "Main")
        {
            Debug.LogError("Main Talent is not selectable");
            return false;
        }


        curTalent = talent;
        curButton = button;
        curData = data;
        return true;
    }

    private void InactiveCheck(Dictionary<string, TalentState> talentsStates)
    {
        Debug.Log(curTalent);
        if (curTalent != null && talentsStates.TryGetValue(curTalent.talentName, out TalentState state))
        {
            if (state == TalentState.Inactive)
            {
                Debug.Log("Current talent is Inactive");
                curTalent = null;
                curButton = null;
            }
            else
            {
                Debug.Log("Current talent is not Inactive");
            }
        }
        else
        {
            Debug.LogError("Current talent is not found in the talentsStates dictionary");
        }
    }
    
    private bool EnoughPoints(TalentData talent)
    {
        return TalentPointController.GetCurrentPoints() < talent.cost;
    }

    private bool IsAnyTalentSelected(Dictionary<string, TalentState> talentsStates, TalentData talentData, Button button)
    {
        foreach (var kvp in talentsStates)
        {
            if (kvp.Key == talentData.talentName) continue;

            if (kvp.Value == TalentState.Selected)
            {
                if(curTalent != null && curTalent.talentName != talentData.talentName)
                {
                    Debug.Log("Selected: " + kvp.Key);
                    return true;
                }
            }
        }
        return false;
    }


    private bool HasUpgradedDependents(TalentData talent, Dictionary<string, TalentState> talentsStates)
    {
        foreach (var pair in TalentsData.current.buttonTalentPairs)
        {
            var dependentTalent = pair.talent;
            if (dependentTalent.prerequisites != null && dependentTalent.prerequisites.Length > 0)
            {
                bool isPrerequisite = false;
                bool allPrerequisitesMet = true;
            
                foreach (var prerequisite in dependentTalent.prerequisites)
                {
                    if (prerequisite == talent)
                    {
                        isPrerequisite = true;
                    }

                    if (!talentsStates.TryGetValue(prerequisite.talentName, out TalentState state) || state != TalentState.Upgraded)
                    {
                        allPrerequisitesMet = false;
                        break;
                    }
                }
            
                if (isPrerequisite && allPrerequisitesMet && talentsStates[dependentTalent.talentName] == TalentState.Upgraded)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool CanDeactivateTalent(TalentData talent, Dictionary<string, TalentState> talentsStates)
    {
        foreach (var pair in TalentsData.current.buttonTalentPairs)
        {
            var dependentTalent = pair.talent;
            if (dependentTalent.prerequisites != null && dependentTalent.prerequisites.Length > 0)
            {
                foreach (var prerequisite in dependentTalent.prerequisites)
                {
                    if (prerequisite == talent && talentsStates[dependentTalent.talentName] == TalentState.Upgraded)
                    {
                        bool otherPrerequisitesMet = true;
                        foreach (var otherPrerequisite in dependentTalent.prerequisites)
                        {
                            if (otherPrerequisite != talent && (!talentsStates.TryGetValue(otherPrerequisite.talentName, out TalentState state) || state != TalentState.Upgraded))
                            {
                                otherPrerequisitesMet = false;
                                break;
                            }
                        }
                        if (!otherPrerequisitesMet)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}