using System;
using System.Collections.Generic;
using UnityEngine;

public class DotProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject dotPrefab;

    private int totalCountTargetStars;
    private List<Dot> dots = new List<Dot>();

    public void Initialize(int totalCount)
    {
        totalCountTargetStars = totalCount;

        for (int i = 0; i < totalCountTargetStars; i++)
        {
            Dot dot = Instantiate(dotPrefab, transform, false).GetComponent<Dot>();
            dot.Status = Dot.DotStatus.Off;
            dots.Add(dot);
        }
        
        dots[0].Status = Dot.DotStatus.On;
    }

    public void UpdateDisplay(int countCollectedStars)
    {
        dots[countCollectedStars].Status = Dot.DotStatus.On; 
    }
}
