using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalentButtonsView : MonoBehaviour, ITalentButtonsView
{
    public TextMeshProUGUI talentPointsText;
    public Button addTalentButton;
    public Button removeAllTalents;

    private ControllerTalentsButton _controller;

    public void Init(ControllerTalentsButton controller)
    {
        _controller = controller;
        
        addTalentButton.onClick.AddListener(_controller.AddTalentPoint);
        removeAllTalents.onClick.AddListener(_controller.RemoveAllPoints);
    }

    public void HideUI()
    {
        talentPointsText.transform.gameObject.SetActive(false);
        addTalentButton.transform.gameObject.SetActive(false);
    }

    public void ShowUi()
    {
        talentPointsText.transform.gameObject.SetActive(true);
        addTalentButton.transform.gameObject.SetActive(true);
    }

    public void UpdateText(int points)
    {
        talentPointsText.text = "Current points: " + points.ToString("");
    }
}
