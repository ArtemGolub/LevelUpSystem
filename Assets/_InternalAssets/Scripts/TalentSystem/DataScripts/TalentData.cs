using UnityEngine;

[CreateAssetMenu(fileName = "New Talent", menuName = "Talent System/Talent")]
public class TalentData : ScriptableObject
{
    public string talentName;
    public string talentDescription;
    public Sprite talentIcon;
    public TalentState initialState;


    public int requiredLevel;
    public int cost;
    
    public TalentData[] prerequisites;
}