using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentUpgradeController : MonoBehaviour
{
    public event Func<TalentData> GetCurrentTalent;
    public event Func<Button> GetCurrentButton;
    public event Func<Dictionary<string, TalentData>> GetCurrentData;
    

    private TalentUpgradeModel _talentUpgradeModel;
    private UpgradeTalentView _upgradeTalentView;

    private void Start()
    {
        _talentUpgradeModel = new TalentUpgradeModel();
        _upgradeTalentView = GetComponent<UpgradeTalentView>();
        _upgradeTalentView.Init(this);
    }

    public void UpgradeTalent()
    {
        TalentData talentData = GetCurrentTalent?.Invoke();
        Button button = GetCurrentButton.Invoke();
        
        _talentUpgradeModel.Upgrade(talentData.talentName, talentData.cost, button);
    }

    public void RemoveUpgrade()
    {
        TalentData talentData = GetCurrentTalent?.Invoke();
        Button button = GetCurrentButton.Invoke();
        
        _talentUpgradeModel.RemoveUpgrade(talentData.talentName, talentData.cost, button);
    }

    public void RemoveAllTalents()
    {
        var data = GetCurrentData.Invoke();
        
        _talentUpgradeModel.RemoveAllTalents(data);
    }
}
