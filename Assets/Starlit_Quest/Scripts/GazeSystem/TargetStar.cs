using System;
using UnityEngine;
using System.Collections;

public class TargetStar : MonoBehaviour, IGazeResponder
{
    public float gazeSelectionDurationThreshold = 1f;
    
    [Header("Materials")]
    [SerializeField] private Material gazeDefaultMaterial;
    [SerializeField] private Material gazeOngoingMaterial;
    [SerializeField] private Material gazeCompleteMaterial;
    
    private Material thisMaterial;
    private bool isGazing = false;
    private float elpasedGazeDuration = 0f;
    
    public void OnGazeEnter()
    {
        isGazing = true;
        thisMaterial = gazeOngoingMaterial;
    }

    public void OnGazeExit()
    {
        GazeSelectionFailed();
        thisMaterial = gazeDefaultMaterial;
    }

    public void OnGazeSelect()
    {
        thisMaterial = gazeCompleteMaterial;
        // the star moves to the glass jar
    }
    
    private void Start()
    {
        thisMaterial = GetComponent<Renderer>().material;
        thisMaterial = gazeDefaultMaterial;
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
