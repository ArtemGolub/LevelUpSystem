using UnityEngine;
using UnityEngine.UI;

public class ButtonPointView : MonoBehaviour, IButtonPointView
{

    private ButtonPointController _Controller;
    public Button reciveTalentButton;

    public void Init(ButtonPointController controller)
    {
        _Controller = controller;
    }
    private void Start()
    {
       reciveTalentButton.onClick.AddListener(_Controller.RecivePoint);
    }
}
