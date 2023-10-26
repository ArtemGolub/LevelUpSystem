using UnityEngine;

public class TalentPointController : MonoBehaviour
{
    private TalentPointModel _talentPointModel;
    private TalentPointView _talentPointView;
    
    public int GetCurrentPoints()
    {
        return _talentPointModel.GetTalentPoints();
    }
    private void Start()
    {
        _talentPointModel = new TalentPointModel();
        _talentPointView = GetComponent<TalentPointView>();
        
        TalentEvents.current.TalentPointAdded.AddListener(AddTalentPoints);
        TalentEvents.current.ReciveTalentPoint.AddListener(ReciveTalentPoints);
        TalentEvents.current.AllPointsReset.AddListener(RemoveAllTalentPoints);
        TalentEvents.current.SpendTalentPoints.AddListener(RemoveTalentPoints);
        
        _talentPointView.UpdateTalentPointText(_talentPointModel.GetTalentPoints());
    }

    private void AddTalentPoints(int amount)
    {
        _talentPointModel.AddTalentPoint(amount);
        _talentPointView.UpdateTalentPointText(_talentPointModel.GetTalentPoints());
    }

    private void ReciveTalentPoints(int amount)
    {
        _talentPointModel.ReciveTalentPoint(amount);
        _talentPointView.UpdateTalentPointText(_talentPointModel.GetTalentPoints());
    }

    private void RemoveTalentPoints(int amount)
    {
        _talentPointModel.RemoveTalentPoint(amount);
        _talentPointView.UpdateTalentPointText(_talentPointModel.GetTalentPoints());
    }
    
    private void RemoveAllTalentPoints()
    {
        _talentPointModel.RemoveAllTalentPoints();
        _talentPointView.UpdateTalentPointText(_talentPointModel.GetTalentPoints());
    }
}
