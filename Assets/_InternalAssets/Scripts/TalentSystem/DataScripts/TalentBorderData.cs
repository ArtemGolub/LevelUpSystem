using UnityEngine;
[CreateAssetMenu(fileName = "New Border Data", menuName = "Talent System/New Border Data")]
public class TalentBorderData : ScriptableObject
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public Sprite selectedSprite;
    public Sprite upgradedSprite;
    
    public Sprite GetSpriteForState(TalentState state)
    {
        switch (state)
        {
            case TalentState.Active:
                return activeSprite;
            case TalentState.Inactive:
                return inactiveSprite;
            case TalentState.Selected:
                return selectedSprite;
            case TalentState.Upgraded:
                return upgradedSprite;
            default:
                return null;
        }
    }
}
