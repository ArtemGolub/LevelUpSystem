using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(TalentsData))]
public class TalentsDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TalentsData talentsData = (TalentsData)target;
        
        DrawDefaultInspector();
        
        if (talentsData.buttonTalentPairs == null)
            talentsData.buttonTalentPairs = new TalentsPair[0];
        
        int newSize = EditorGUILayout.IntField("Number of Pairs", talentsData.buttonTalentPairs.Length);
        if (newSize != talentsData.buttonTalentPairs.Length)
        {
            System.Array.Resize(ref talentsData.buttonTalentPairs, newSize);
        }

        for (int i = 0; i < talentsData.buttonTalentPairs.Length; i++)
        {
            if (talentsData.buttonTalentPairs[i] == null)
                talentsData.buttonTalentPairs[i] = new TalentsPair();

            EditorGUILayout.BeginHorizontal();
            
            talentsData.buttonTalentPairs[i].button = (Button)EditorGUILayout.ObjectField(talentsData.buttonTalentPairs[i].button, typeof(Button), true);
            talentsData.buttonTalentPairs[i].talent = (TalentData)EditorGUILayout.ObjectField(talentsData.buttonTalentPairs[i].talent, typeof(TalentData), true);
            
            EditorGUILayout.EndHorizontal();
        }
        
        if (GUI.changed)
        {
            EditorUtility.SetDirty(talentsData);
        }
    }
}