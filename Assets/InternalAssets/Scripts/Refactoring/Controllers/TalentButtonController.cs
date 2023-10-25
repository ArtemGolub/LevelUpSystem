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
    }

    private void HandleTalentButtonPressed()
    {
        TalentEvents.current.ChangeButtonBorder.AddListener(_talentButtonView.ChangeBorder);
    }
}