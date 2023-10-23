using UnityEngine;

public class ControllerTalents : MonoBehaviour
{
    private ModelTalents _model;
    private ITalentView _view;

    private void Start()
    {
        _model = new ModelTalents();
        _view = GetComponent<ITalentView>();
        _view.Init(this);
    }
    
    public void AddTalentPoint()
    {
       _model.AddTalentPoint();
       _view.UpdateText(_model.GetTalentPoints());
    }

    private void RemoveTalentPoint()
    {
        if ((!_model.RemoveTalentEnable())) return;
        
        _model.RemoveTalentPoint();
        _view.UpdateText(_model.GetTalentPoints());
    }
}
