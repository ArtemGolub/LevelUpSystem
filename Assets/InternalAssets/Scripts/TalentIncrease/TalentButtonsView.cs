using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalentButtonsView : MonoBehaviour, ITalentButtonsView
{
    public TextMeshProUGUI talentPointsText;
    public Button addTalentButton;
    public Button removeAllTalents;
    public Button removeOneTalent;

    private ControllerTalentsButton _controller;

    public void Init(ControllerTalentsButton controller)
    {
        _controller = controller;
        
        addTalentButton.onClick.AddListener(_controller.AddTalentPoint);
        removeAllTalents.onClick.AddListener(_controller.RemoveAllPoints);
        removeOneTalent.onClick.AddListener(_controller.RemoveTalentPoint);
    }

    public void HideUI()
    {
        talentPointsText.transform.gameObject.SetActive(false);
        addTalentButton.transform.gameObject.SetActive(false);
        removeOneTalent.transform.gameObject.SetActive(false);
    }

    public void ShowUi()
    {
        talentPointsText.transform.gameObject.SetActive(true);
        addTalentButton.transform.gameObject.SetActive(true);
        removeOneTalent.transform.gameObject.SetActive(true);
    }

    public void UpdateText(int points)
    {
        talentPointsText.text = "Current points: " + points.ToString("");
    }
}
