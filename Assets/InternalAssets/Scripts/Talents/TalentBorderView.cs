using UnityEngine;
using UnityEngine.UI;

public class TalentBorderView : MonoBehaviour, ITalentBorderView
{
    private TalentController _controller;
    public void Init(TalentController controller)
    {
        _controller = controller;
    }

    public void ChangeBorder(Button button, TalentState state)
    {
        button.image.sprite = TalentsData.current.BorderData.GetSpriteForState(state);
    }
}
