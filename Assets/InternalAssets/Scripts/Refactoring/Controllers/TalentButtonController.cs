using UnityEngine;


public class TalentButtonController: MonoBehaviour
{
    private TalentButtonModel _talentButtonModel; 
    private TalentButtonView _talentButtonView;
    
    private void Start()
    {
        _talentButtonModel = new TalentButtonModel();
        _talentButtonView = GetComponent<TalentButtonView>();

        HandleTalentButtonPressed();
        SetTalentSprite();
        SetSpriteColor();
        SetTalentName();
        SetTalentPrice();
        HideTalentPrice();
    }

    private void SetTalentPrice()
    {
        TalentEvents.current.ShowTalentPrice.AddListener(_talentButtonView.TalentPrice);
    }

    private void HideTalentPrice()
    {
        TalentEvents.current.HideTalentPrice.AddListener(_talentButtonView.HideTalentPrice);
    }
    
    private void SetTalentName()
    {
        TalentEvents.current.SetTalentName.AddListener(_talentButtonView.SetTalentName);
    }
    
    private void HandleTalentButtonPressed()
    {
        TalentEvents.current.ChangeButtonBorder.AddListener(_talentButtonView.ChangeBorder);
    }

    private void SetTalentSprite()
    {
        TalentEvents.current.SetTalentSprite.AddListener(_talentButtonView.SetTalentImage);
    }
    
    private void SetSpriteColor()
    {
        TalentEvents.current.SetSpriteColor.AddListener(_talentButtonView.SetSpriteColor);
    }
}