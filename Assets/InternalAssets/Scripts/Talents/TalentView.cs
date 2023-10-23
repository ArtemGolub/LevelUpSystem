using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TalentView : MonoBehaviour, ITalentView
{
    public Button talent1;
    public Button talent2;

    private TalentController _controller;
    
    public void Init(TalentController controller)
    {
        _controller = controller;
        
        
    }

    public void Enable()
    {
        throw new System.NotImplementedException();
    }

    public void Disable()
    {
        throw new System.NotImplementedException();
    }

    public void Chosen()
    {
        throw new System.NotImplementedException();
    }
}
