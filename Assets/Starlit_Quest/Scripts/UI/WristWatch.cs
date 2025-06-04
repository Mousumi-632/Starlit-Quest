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
    private int numCollectedStars = 0;
    private StarSpawner starSpawner;
    private int currentStarIndex;

    private void Start()
    {
        // set default texture
        targetStarImage.texture = null;
        targetStarImage.texture = targetStars[targetStars.Count - 1]; 
    }

    private void Update()
    {
        if (!isWatchInitialized) Initialize();
    }

    private void Initialize()
    {
        starSpawner = FindFirstObjectByType<StarSpawner>();
        if (starSpawner == null) return;
        starSpawner.OnStarSpawned += DisplayNewStar;
        
        if (StarCounter.Instance == null) return;
        dotProgressBar.Initialize(StarCounter.Instance.MaxStars);
        StarCounter.Instance.OnStarsChanged += CelebrateAStarCollected;
        
        isWatchInitialized = true;
    }
    
    private void CelebrateAStarCollected(int count)
    {
        numCollectedStars = count;
        // play celebration animation
        return;
    }

    private void DisplayNewStar(int starIndex)
    {
        currentStarIndex = starIndex;
        if (numCollectedStars < targetStars.Count - 1)
        {
            StartCoroutine(AsyncDisplayNewStar());
        }
        else
        {
            CompleteTask();
        }
    }

    private IEnumerator AsyncDisplayNewStar()
    {
        yield return targetStarImage.transform.DOScale(0f, 1f).SetEase(Ease.InOutQuad).WaitForCompletion();
        UpdateTargetStarDisplay();
        yield return targetStarImage.transform.DOScale(1f, 1f).SetEase(Ease.InOutQuad).WaitForCompletion();
        
        dotProgressBar.UpdateDisplay(numCollectedStars);
    }

    private void UpdateTargetStarDisplay()
    {
        targetStarImage.texture = null;
        if (currentStarIndex < 0) currentStarIndex = 0;
        if (currentStarIndex >= targetStars.Count - 1) currentStarIndex = targetStars.Count - 2;
        
        // Debug.Log("======= watch UI index: " + currentStarIndex);
        targetStarImage.texture = targetStars[currentStarIndex];
    }

    private void CompleteTask()
    {
        Debug.Log("Task completed!");
    }
}

// TODOs
//     flesh out complete task step