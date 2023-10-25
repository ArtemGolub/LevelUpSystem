using UnityEngine;
using UnityEngine.UI;

public class TalentViewPastPast : MonoBehaviour, ITalentView_Past
{
    private TalentController_Past _controllerPast;
    public Button ConfirmButton;
    public Button CancelButton;
    
    public void Init(TalentController_Past controllerPast)
    {
        _controllerPast = controllerPast;
        ConfirmButton.onClick.AddListener(_controllerPast.HandleUpgradeButtonPress);
        CancelButton.onClick.AddListener(_controllerPast.HandleCancelButtonPress);
    }

    public void RegisterTalents(TalentsPair[] talentsPairs)
    {
        foreach (var pair in talentsPairs)
        {
            if (pair.button == null) return;
            pair.button.onClick.AddListener(() => _controllerPast.HandleButtonPress(pair.talent, pair.button));
        }
    }

    public void ShowConfirm(bool isActive)
    {
        ConfirmButton.transform.gameObject.SetActive(isActive);
        CancelButton.transform.gameObject.SetActive(!isActive);
    }

    public void ShowCancel(bool isActive)
    {
        ConfirmButton.transform.gameObject.SetActive(!isActive);
        CancelButton.transform.gameObject.SetActive(isActive);
    }

    public void HideButtons()
    {
        ConfirmButton.transform.gameObject.SetActive(false);
        CancelButton.transform.gameObject.SetActive(false);
    }
}
