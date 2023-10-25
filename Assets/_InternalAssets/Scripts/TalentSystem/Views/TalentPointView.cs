using TMPro;
using UnityEngine;

public class TalentPointView : MonoBehaviour, ITalentPointView
{
    public TextMeshProUGUI talentPointsText;
    
    public void UpdateTalentPointText(int points)
    {
        talentPointsText.text = "Current points: " + points.ToString("");
    }
}
