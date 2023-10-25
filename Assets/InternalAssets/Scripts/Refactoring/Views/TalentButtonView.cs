using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalentButtonView : MonoBehaviour, ITalentButtonView
{
    public TextMeshProUGUI talentPrice;

    public void TalentPrice(TalentData talent)
    {
        talentPrice.transform.gameObject.SetActive(true);
        talentPrice.text = talent.cost.ToString();
    }

    public void HideTalentPrice()
    {
        talentPrice.gameObject.SetActive(false);
    }
    public void ChangeBorder(Button button, TalentState state)
    {
        button.image.sprite = TalentsData.current.BorderData.GetSpriteForState(state);
    }

    public void SetTalentName(Button button, TalentData talent)
    {
        button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = talent.talentName;
    }

    public void SetTalentImage(TalentData talent, Button button)
    {
        button.transform.GetChild(0).GetComponent<Image>().sprite = talent.talentIcon;
    }

    public void SetSpriteColor(Button button, TalentState talentState)
    {
        switch (talentState)
        {
            case TalentState.Active:
            {
                button.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
                break;
            }
            case TalentState.Inactive:
            {
                button.transform.GetChild(0).GetComponent<Image>().color = Color.black;
                break;
            }
            case TalentState.Selected:
            {
                button.transform.GetChild(0).GetComponent<Image>().color = Color.green;
                break;
            }
            case TalentState.Upgraded:
            {
                button.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                break;
            }
        }
    }
}