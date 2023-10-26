using System;
using UnityEngine;

public class TalentsData : MonoBehaviour
{
    public static TalentsData current;
    
    public TalentsPair[] buttonTalentPairs = new TalentsPair[0];
    public TalentBorderData BorderData;

    private void Awake()
    {
        current = this;
    }
}
