using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITalentBorderView
{
    public void Init(TalentController controller);

    public void ChangeBorder(Button button, TalentState state);
}
