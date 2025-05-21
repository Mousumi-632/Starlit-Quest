using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristWatch : MonoBehaviour
{
    [Header("Target Star Display")] [SerializeField]
    private RawImage targetStarImage;

    [Header("Target Stars")] [SerializeField]
    private List<Texture> targetStars;

    private void Start()
    {
        StarCounter.Instance.OnStarsChanged += UpdateWatchStarDisplay;
    }

    private void UpdateWatchStarDisplay(int countCollectedStars)
    {
        if (countCollectedStars < targetStars.Count)
        {
            targetStarImage.texture = null;
            targetStarImage.texture = targetStars[countCollectedStars];
        }
        else
        {
            DisplayTaskCompletion();
        }
    }

    private void DisplayTaskCompletion()
    {
        Debug.Log("Task completed!");
    }
}
