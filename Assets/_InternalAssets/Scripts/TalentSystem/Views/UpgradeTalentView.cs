using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeTalentView : MonoBehaviour, IUpgradeTalentView
{
    private TalentUpgradeController _controller;
    public Button confirmButton;
    public Button cancelButton;
    public Button removeAllUpgradesButton;

    public void Init(TalentUpgradeController controller)
    {
        _controller = controller;
        
        TalentEvents.current.ShowConfromUI.AddListener(ShowConfirm);
        TalentEvents.current.ShowCancelUI.AddListener(ShowCancel);
        TalentEvents.current.HideConfrimUI.AddListener(HideUI);
        
        ConfirmUpgrade();
    }

    public void ConfirmUpgrade()
    {
        confirmButton.onClick.AddListener(_controller.UpgradeTalent);
        cancelButton.onClick.AddListener(_controller.RemoveUpgrade);
        removeAllUpgradesButton.onClick.AddListener(_controller.RemoveAllTalents);
    }

    public void ShowConfirm()
    {
        confirmButton.gameObject.SetActive(true);
    }

    private void ShowCancel()
    {
        cancelButton.gameObject.SetActive(true);
    }
    

    public void HideUI()
    { 
        confirmButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }
}
