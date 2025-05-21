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
        targetStarImage.texture = targetStars[countCollectedStars];
    }
}


// TODOs
//     call StarCounter AddStar()
//     verify raw image texture switch
//     add Game Manager game object, verify the rest
//     