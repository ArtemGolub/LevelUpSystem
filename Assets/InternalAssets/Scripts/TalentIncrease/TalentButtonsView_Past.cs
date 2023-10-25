using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalentButtonsView_Past : MonoBehaviour, ITalentButtonsView
{
    public TextMeshProUGUI talentPointsText;
    public Button addTalentButton;
    public Button removeAllTalents;

    private TalentController_Past _controllerPast;

    public void Init(TalentController_Past controllerPast)
    {
        _controllerPast = controllerPast;
        
        addTalentButton.onClick.AddListener(_controllerPast.AddTalentPoint);
        removeAllTalents.onClick.AddListener(_controllerPast.RemoveAllPoints);
    }
    
    public void UpdateText(int points)
    {
        talentPointsText.text = "Current points: " + points.ToString("");
    }
}
