using UnityEngine;
using UnityEngine.UI;

public class TalentButtonView : MonoBehaviour, ITalentButtonView
{
    public void ChangeBorder(Button button, TalentState state)
    {
        button.image.sprite = TalentsData.current.BorderData.GetSpriteForState(state);
    }
}
