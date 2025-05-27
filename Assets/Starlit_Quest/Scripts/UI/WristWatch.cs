using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WristWatch : MonoBehaviour
{
    [Header("Target Stars")] 
    [SerializeField] private RawImage targetStarImage;
    [SerializeField] private List<Texture> targetStars;

    [Header("Progress Bar")]
    [SerializeField] private DotProgressBar dotProgressBar;

    private bool isWatchInitialized = false;
    private int countCollectedStars = 0;
    
    private void Update()
    {
        if (!isWatchInitialized) Initialize();
    }

    private void Initialize()
    {
        if (StarCounter.Instance == null) return;
        
        dotProgressBar.Initialize(StarCounter.Instance.MaxStars);
        StarCounter.Instance.OnStarsChanged += UpdateWatchUI;
        isWatchInitialized = true;
    }

    private void UpdateWatchUI(int count)
    {
        countCollectedStars = count;
        if (countCollectedStars < targetStars.Count)
        {
            StartCoroutine(AsyncUpdateWatchUI());
        }
        else
        {
            CompleteTask();
        }
    }

    private IEnumerator AsyncUpdateWatchUI()
    {
        yield return targetStarImage.transform.DOScale(0f, 1f).SetEase(Ease.InOutQuad).WaitForCompletion();
        UpdateTargetStarDisplay();
        yield return targetStarImage.transform.DOScale(1f, 1f).SetEase(Ease.InOutQuad).WaitForCompletion();
        
        dotProgressBar.UpdateDisplay(countCollectedStars);
    }

    private void UpdateTargetStarDisplay()
    {
        targetStarImage.texture = null;
        targetStarImage.texture = targetStars[countCollectedStars];
    }

    private void CompleteTask()
    {
        Debug.Log("Task completed!");
    }
}

// TODOs
//     flesh out complete task step