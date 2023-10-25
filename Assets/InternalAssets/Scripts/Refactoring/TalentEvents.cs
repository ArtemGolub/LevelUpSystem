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
    [HideInInspector] public UnityEvent<Button, TalentData> SetTalentName = new UnityEvent<Button, TalentData>();

    [HideInInspector] public UnityEvent<int> SpendTalentPoints = new UnityEvent<int>();
    [HideInInspector] public UnityEvent<int> ReciveTalentPoint = new UnityEvent<int>();
    
    [HideInInspector] public UnityEvent ShowConfromUI = new UnityEvent();
    [HideInInspector] public UnityEvent ShowCancelUI = new UnityEvent();
    [HideInInspector] public UnityEvent HideConfrimUI = new UnityEvent();
    [HideInInspector] public UnityEvent ResetCurTalent = new UnityEvent();

    [HideInInspector] public UnityEvent ActivateDependentTalents = new UnityEvent();
    [HideInInspector] public UnityEvent<TalentData, Button> SetTalentSprite = new UnityEvent<TalentData, Button>();

    [HideInInspector] public UnityEvent<Button, TalentState> SetSpriteColor = new UnityEvent<Button, TalentState>();
    [HideInInspector] public UnityEvent<TalentData> ShowTalentPrice = new UnityEvent<TalentData>();
    [HideInInspector] public UnityEvent HideTalentPrice = new UnityEvent();

    private void Awake()
    {
        current = this;
    }

    public void OnGameStart(string tileName, TalentState newState, Button button, TalentData data)
    {
        SetTalentName.Invoke(button, data);
        ChangeTileState.Invoke(tileName, newState);
        ChangeButtonBorder.Invoke(button, newState);
        AllPointsReset.Invoke();
        SetTalentSprite.Invoke(data, button);
        SetSpriteColor.Invoke(button, newState);
        HideConfrimUI.Invoke();
        HideTalentPrice.Invoke();
    }
    
    public void OnTalentSelect(string tileName, TalentState newState, Button button, TalentState prevState, TalentData data)
    {
        ChangeTileState.Invoke(tileName, newState);
        ChangeButtonBorder.Invoke(button, newState);
        SetSpriteColor.Invoke(button, newState);
        
        if (prevState == TalentState.Active)
        {
            ShowConfromUI.Invoke();
            ShowTalentPrice.Invoke(data);
        }
        else
        {
            ShowCancelUI.Invoke();
            ShowTalentPrice.Invoke(data);
        }
    }

    public void OnTalentUpgrade(string talentName, int amountPoints, Button button)
    {
        SpendTalentPoints.Invoke(amountPoints);
        ChangeTileState.Invoke(talentName, TalentState.Upgraded);
        ChangeButtonBorder.Invoke(button, TalentState.Upgraded);
        SetSpriteColor.Invoke(button, TalentState.Upgraded);
        HideConfrimUI.Invoke();
        ResetCurTalent.Invoke();
        ActivateDependentTalents.Invoke();
        HideTalentPrice.Invoke();
    }

    public void OnTalentRemove(string talentName,  int amountPoints, Button button)
    {
        ReciveTalentPoint.Invoke(amountPoints);
        ChangeTileState.Invoke(talentName, TalentState.Active);
        ChangeButtonBorder.Invoke(button, TalentState.Active);
        SetSpriteColor.Invoke(button, TalentState.Active);
        HideConfrimUI.Invoke();
        ResetCurTalent.Invoke();
        ActivateDependentTalents.Invoke();
        HideTalentPrice.Invoke();
    }
}
