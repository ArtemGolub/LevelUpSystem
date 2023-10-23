using UnityEngine.UI;

public interface ITalentView
{
    
    public void Init(TalentController controller);
    public void RegisterTalents(TalentsPair[] talentsPairs);
    public void ShowTalentName(string talentName);
}
