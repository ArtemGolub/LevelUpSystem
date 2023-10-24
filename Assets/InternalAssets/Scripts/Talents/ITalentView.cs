using UnityEngine.UI;

public interface ITalentView
{
    
    public void Init(TalentController controller);
    public void RegisterTalents(TalentsPair[] talentsPairs);
    public void ShowConfirm(bool isActive);
    public void ShowCancel(bool isActive);
    public void HideButtons();
}
