using System;
using UnityEngine;
using System.Collections;

public class TargetStar : MonoBehaviour
{
    public float gazeSelectionDurationThreshold = 1000f;
    
    [Header("Materials")]
    [SerializeField] private Material gazeDefaultMaterial;
    [SerializeField] private Material gazeOngoingMaterial;
    [SerializeField] private Material gazeCompleteMaterial;
    
    private Renderer renderer;
    private bool isGazing = false;
    private float elpasedGazeDuration = 0f;

    private float testDemoTimer = 0f;
    
    public void OnGazeEnter()
    {
        isGazing = true;
        renderer.material = gazeOngoingMaterial;
        Debug.Log("OnGazeEnter");
    }

    public void OnGazeExit()
    {
        GazeSelectionFailed();
        renderer.material = gazeDefaultMaterial;
        Debug.Log("OnGazeExit");
    }

    public void OnGazeSelect()
    {
        renderer.material = gazeCompleteMaterial;
        // the star moves to the glass jar
        Debug.Log("OnGazeSelect");
    }
    
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = gazeDefaultMaterial;
    }

    private void Update()
    {
        if (isGazing)
        {
            elpasedGazeDuration += Time.deltaTime;
            if (elpasedGazeDuration >= gazeSelectionDurationThreshold)
            {
                OnGazeSelect();
            }
        }
    }
    
    private void GazeSelectionFailed()
    {
        isGazing = false;
        elpasedGazeDuration = 0f;
    }
}
