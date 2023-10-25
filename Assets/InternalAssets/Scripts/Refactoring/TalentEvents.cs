using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TalentEvents : MonoBehaviour
{
    public static TalentEvents current;
    
    [HideInInspector]public UnityEvent<int> TalentPointAdded = new UnityEvent<int>();
    [HideInInspector] public UnityEvent AllPointsReset = new UnityEvent();
    
    [HideInInspector] public UnityEvent AllTalentsReset = new UnityEvent();
    [HideInInspector] public UnityEvent<string> TalentReset = new UnityEvent<string>();
    [HideInInspector] public UnityEvent<string, TalentState> ChangeTileState = new UnityEvent<string, TalentState>();
    [HideInInspector] public UnityEvent<Button, TalentState> ChangeButtonBorder = new UnityEvent<Button, TalentState>();

    [HideInInspector] public UnityEvent<int> SpendTalentPoints = new UnityEvent<int>();
    [HideInInspector] public UnityEvent<int> ReciveTalentPoint = new UnityEvent<int>();
    
    [HideInInspector] public UnityEvent ShowConfromUI = new UnityEvent();
    [HideInInspector] public UnityEvent ShowCancelUI = new UnityEvent();
    [HideInInspector] public UnityEvent HideConfrimUI = new UnityEvent();
    [HideInInspector] public UnityEvent ResetCurTalent = new UnityEvent();

    [HideInInspector] public UnityEvent ActivateDependentTalents = new UnityEvent();

    private void Awake()
    {
        current = this;
    }

    public void OnGameStart(string tileName, TalentState newState, Button button)
    {
        ChangeTileState.Invoke(tileName, newState);
        ChangeButtonBorder.Invoke(button, newState);
        AllPointsReset.Invoke();
        HideConfrimUI.Invoke();
    }
    
    public void OnTalentSelect(string tileName, TalentState newState, Button button, TalentState prevState)
    {
        ChangeTileState.Invoke(tileName, newState);
        ChangeButtonBorder.Invoke(button, newState);
        
        if (prevState == TalentState.Active)
        {
            ShowConfromUI.Invoke();
        }
        else
        {
            ShowCancelUI.Invoke();
        }
    }

    public void OnTalentUpgrade(string talentName, int amountPoints, Button button)
    {
        SpendTalentPoints.Invoke(amountPoints);
        ChangeTileState.Invoke(talentName, TalentState.Upgraded);
        ChangeButtonBorder.Invoke(button, TalentState.Upgraded);
        HideConfrimUI.Invoke();
        ResetCurTalent.Invoke();
        ActivateDependentTalents.Invoke();
    }

    public void OnTalentRemove(string talentName,  int amountPoints, Button button)
    {
        ReciveTalentPoint.Invoke(amountPoints);
        ChangeTileState.Invoke(talentName, TalentState.Active);
        ChangeButtonBorder.Invoke(button, TalentState.Active);
        HideConfrimUI.Invoke();
        ResetCurTalent.Invoke();
        ActivateDependentTalents.Invoke();
    }
}
