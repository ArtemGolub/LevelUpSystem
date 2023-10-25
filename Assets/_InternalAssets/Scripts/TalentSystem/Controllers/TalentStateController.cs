using System;
using UnityEngine;

public class TalentStateController : MonoBehaviour
{
    private TalentStateModel _stateModel;
    private TalentStateView _talentStateView;
    
    private void Start()
    {
        _stateModel = new TalentStateModel();
       
        
        _talentStateView = GetComponent<TalentStateView>();
        
        _stateModel.SetTalentDictionary();
        _stateModel.AddListeners();
    }
}
