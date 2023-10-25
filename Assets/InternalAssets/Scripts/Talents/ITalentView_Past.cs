using UnityEngine.UI;

public interface ITalentView_Past
{
    public void Init(TalentController_Past controllerPast);
    public void RegisterTalents(TalentsPair[] talentsPairs);
    public void ShowConfirm(bool isActive);
    public void ShowCancel(bool isActive);
    public void HideButtons();
}
