using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewTalents : MonoBehaviour, ITalentView
{
    public TextMeshProUGUI talentPointsText;
    public Button addTalentButton;

    private ControllerTalents _controller;

    public void Init(ControllerTalents controller)
    {
        _controller = controller;
        
        addTalentButton.onClick.AddListener(_controller.AddTalentPoint);
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
