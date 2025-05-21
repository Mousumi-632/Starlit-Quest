using System;
using System.Collections.Generic;
using UnityEngine;

public class DotProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject dotPrefab;

    private int totalCountTargetStars;
    private List<Dot> dots;

    public void Initialize(int totalCount)
    {
        totalCountTargetStars = totalCount;

        for (int i = 0; i < totalCountTargetStars; i++)
        {
            Instantiate(dotPrefab, transform, false);
        }
    }

    public void UpdateDisplay(int countCollectedStars)
    {
        Debug.Log("Update Progress Bar, collected " + countCollectedStars + " stars");
    }
}
