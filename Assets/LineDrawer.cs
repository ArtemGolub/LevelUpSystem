using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineDrawer : MonoBehaviour
{
    public TalentsData data;
    public LineRenderer lineRendererPrefab;
    public Camera uiCamera;

    private void Start()
    {
        if (lineRendererPrefab == null)
        {
            Debug.LogError("LineRenderer prefab is not assigned!");
            return;
        }

        if (data == null)
        {
            Debug.LogError("TalentsData is not assigned!");
            return;
        }

        if (uiCamera == null)
        {
            Debug.LogError("UI Camera is not assigned!");
            return;
        }

        foreach (var talentPair in data.buttonTalentPairs)
        {
            if (talentPair.talent != null && talentPair.talent.prerequisites != null)
            {
                DrawLines(talentPair.button.transform as RectTransform, talentPair.talent.prerequisites);
            }
        }
    }

    private void DrawLines(RectTransform startTransform, TalentData[] prerequisites)
    {
        foreach (var prerequisite in prerequisites)
        {
            var endButton = FindButtonForTalent(prerequisite);
            if (endButton != null)
            {
                RectTransform endTransform = endButton.transform as RectTransform;
                if (endTransform != null)
                {
                    LineRenderer lineRenderer = Instantiate(lineRendererPrefab, transform);
                    lineRenderer.positionCount = 2;
                    
                    Vector2 startWorldPosition = RectTransformUtility.WorldToScreenPoint(uiCamera, startTransform.position);
                    Vector2 endWorldPosition = RectTransformUtility.WorldToScreenPoint(uiCamera, endTransform.position);
                    
                    lineRenderer.SetPosition(0, uiCamera.ScreenToWorldPoint(startWorldPosition));
                    lineRenderer.SetPosition(1, uiCamera.ScreenToWorldPoint(endWorldPosition));
                }
                else
                {
                    Debug.LogWarning("End transform is not a RectTransform!");
                }
            }
            else
            {
                Debug.LogWarning("Button for talent " + prerequisite.talentName + " not found!");
            }
        }
    }

    private Button FindButtonForTalent(TalentData talent)
    {
        foreach (var pair in data.buttonTalentPairs)
        {
            if (pair.talent == talent)
                return pair.button;
        }
        return null;
    }
}