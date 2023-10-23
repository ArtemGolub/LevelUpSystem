using UnityEngine;

public class TalentController : MonoBehaviour
{
    private TalentModel _talentModel;
    private ITalentView _talentView;
    public TalentsData talentsData;

    private void Start()
    {
        if (talentsData == null) return;

        _talentModel = new TalentModel();
        _talentView = GetComponent<ITalentView>();
        _talentView.Init(this);
        _talentView.RegisterTalents(talentsData.buttonTalentPairs);
    }

    public void HandleButtonPress(TalentData talent)
    {
        if (talent == null) return;

        if (CanUpgradeTalent(talent))
        {
            _talentModel.talentsStates[talent.talentName] = TalentState.Upgraded;
            _talentView.ShowTalentName(talent.talentName + " upgraded");
        }
        else
        {
            _talentView.ShowTalentName("Unable to upgrade " + talent.talentName + " Check Conditions");
        }
    }

    private bool CanUpgradeTalent(TalentData talent)
    {
        if (talent.prerequisites != null && talent.prerequisites.Length > 0)
        {
            foreach (var prereq in talent.prerequisites)
            {
                if (!_talentModel.talentsStates.ContainsKey(prereq.talentName) ||
                    _talentModel.talentsStates[prereq.talentName] != TalentState.Upgraded)
                {
                    return false;
                }
            }
        }
        return true;
    }
}