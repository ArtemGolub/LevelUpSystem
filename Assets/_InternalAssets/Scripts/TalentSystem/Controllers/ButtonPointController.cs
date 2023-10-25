using System;
using UnityEngine;

public class ButtonPointController : MonoBehaviour
{
    private ButtonPointModel _buttonPointModel;
    private ButtonPointView _buttonPointView;

    private void Awake()
    {
        _buttonPointModel = new ButtonPointModel();
        _buttonPointView = GetComponent<ButtonPointView>();
        _buttonPointView.Init(this);
    }

    private void Start()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        RecivePoint();
        
    }

    public void RecivePoint()
    {
        _buttonPointModel.RecivePoint();
    }

    public void ResetPoints()
    {
        _buttonPointModel.RemoveAllPoints();
    }


    
}
