using UnityEngine;

public class TalentView : MonoBehaviour, ITalentView
{
    private TalentController _controller;

    public void Init(TalentController controller)
    {
        _controller = controller;
    }

    public void RegisterTalents(TalentsPair[] talentsPairs)
    {
        foreach (var pair in talentsPairs)
        {
            if (pair.button == null) return;
            pair.button.onClick.AddListener(() => _controller.HandleButtonPress(pair.talent));
        }
    }
    
    public void ShowTalentName(string talentName)
    {
        Debug.Log(talentName);
    }
}
