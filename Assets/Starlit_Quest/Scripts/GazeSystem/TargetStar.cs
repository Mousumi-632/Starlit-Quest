using System;
using UnityEngine;
using System.Collections;

public class TargetStar : MonoBehaviour
{
    public float gazeSelectionDurationThreshold = 2f;
    
    [Header("Materials")]
    [SerializeField] private Material gazeDefaultMaterial;
    [SerializeField] private Material gazeOngoingMaterial;
    [SerializeField] private Material gazeCompleteMaterial;
    
    private Renderer objectRenderer;
    private bool isGazing = false;
    private float elapsedGazeDuration = 0f;

    
    public void OnGazeEnter()
    {
        isGazing = true;
        objectRenderer.material = gazeOngoingMaterial;
        Debug.Log("OnGazeEnter");
    }

    public void OnGazeExit()
    {
        GazeSelectionFailed();
        objectRenderer.material = gazeDefaultMaterial;
        Debug.Log("OnGazeExit");
    }

    public void OnGazeSelect()
    {
        objectRenderer.material = gazeCompleteMaterial;
        // the star moves to the glass jar
        Debug.Log("OnGazeSelect");
    }
    
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material = gazeDefaultMaterial;
    }

    private void Update()
    {
        if (isGazing)
        {
            elapsedGazeDuration += Time.deltaTime;
            if (elapsedGazeDuration >= gazeSelectionDurationThreshold)
            {
                OnGazeSelect();
                GazeSelectionFailed(); 
            }
        }
    }
    
    private void GazeSelectionFailed()
    {
        isGazing = false;
        elapsedGazeDuration = 0f;
    }
}
