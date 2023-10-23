using UnityEngine;

public class ControllerTalentsButton : MonoBehaviour
{
    private ModelButtonTalents _modelButton;
    private ITalentButtonsView _buttonsView;

    private void Start()
    {
        _modelButton = new ModelButtonTalents();
        _buttonsView = GetComponent<ITalentButtonsView>();
        _buttonsView.Init(this);
    }
    
    public void AddTalentPoint()
    {
       _modelButton.AddTalentPoint();
       _buttonsView.UpdateText(_modelButton.GetTalentPoints());
    }

    public void RemoveAllPoints()
    {
        _modelButton.RemoveAllTalentPoints();
        _buttonsView.UpdateText(_modelButton.GetTalentPoints());
    }

    public void RemoveTalentPoint()
    {
        if ((!_modelButton.RemoveTalentEnable())) return;
        _modelButton.RemoveTalentPoint();
        _buttonsView.UpdateText(_modelButton.GetTalentPoints());
    }

    public void HideUI()
    {
        _buttonsView.HideUI();
    }

    public void ShowUI()
    {
        _buttonsView.ShowUi();
    }
}
