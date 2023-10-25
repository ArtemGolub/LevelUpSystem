using UnityEngine;

public class TalentView : MonoBehaviour, ITalentView
{
    private TalentController _controller;
    public void Init(TalentController controller)
    {
        _controller = controller;
        RegisterTalents();
    }

    private void RegisterTalents()
    {
        foreach (var pair in TalentsData.current.buttonTalentPairs)
        {
            if (pair.button == null) return;
            pair.button.onClick.AddListener(() => _controller.HandleTalentPress(pair.talent, pair.button));
        }
    }
}
