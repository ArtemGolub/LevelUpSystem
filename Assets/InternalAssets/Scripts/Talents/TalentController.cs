using System;
using UnityEngine;

public class TalentController : MonoBehaviour
{
    private TalentModel _talentModel;
    private ITalentView _talentView;

    private void Start()
    {
        _talentModel = new TalentModel();
        _talentView = GetComponent<ITalentView>();
        _talentView.Init(this);
    }

    public void ChoseTalent()
    {
        _talentView.Chosen();
    }
}
