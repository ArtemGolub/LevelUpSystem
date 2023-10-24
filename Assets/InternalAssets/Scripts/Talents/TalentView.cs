using UnityEngine;
using UnityEngine.UI;

public class TalentView : MonoBehaviour, ITalentView
{
    private TalentController _controller;
    public Button ConfirmButton;
    public Button CancelButton;
    
    public void Init(TalentController controller)
    {
        _controller = controller;
        ConfirmButton.onClick.AddListener(_controller.HandleUpgradeButtonPress);
        CancelButton.onClick.AddListener(_controller.HandleCancelButtonPress);
    }

    public void RegisterTalents(TalentsPair[] talentsPairs)
    {
        foreach (var pair in talentsPairs)
        {
            if (pair.button == null) return;
            pair.button.onClick.AddListener(() => _controller.HandleButtonPress(pair.talent, pair.button));
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

    public void ShowTalentName(string talentName)
    {
        Debug.Log(talentName);
    }
}
