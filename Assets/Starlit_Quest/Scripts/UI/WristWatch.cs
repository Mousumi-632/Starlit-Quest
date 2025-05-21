using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristWatch : MonoBehaviour
{
    [Header("Target Stars")] [SerializeField]
    private RawImage targetStarImage;
    private List<Texture> targetStars;

    [Header("Progress Bar")]
    private DotProgressBar dotProgressBar;

    private void Start()
    {
        StarCounter.Instance.OnStarsChanged += UpdateWatchUI;
    }

    private void UpdateWatchUI(int countCollectedStars)
    {
        if (countCollectedStars < targetStars.Count)
        {
            UpdateTargetStarDisplay(countCollectedStars);
            dotProgressBar.UpdateDisplay(countCollectedStars);
        }
        else
        {
            CompleteTask();
        }
    }

    private void UpdateTargetStarDisplay(int countCollectedStars)
    {
        targetStarImage.texture = null;
        targetStarImage.texture = targetStars[countCollectedStars];
    }

    private void CompleteTask()
    {
        Debug.Log("Task completed!");
    }
}
