using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITalentView
{
    public void Init(TalentController controller);

    public void Enable();
    public void Disable();
    public void Chosen();
}
