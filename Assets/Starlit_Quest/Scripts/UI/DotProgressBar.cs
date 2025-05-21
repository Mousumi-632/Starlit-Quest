using System;
using UnityEngine;

public class DotProgressBar : MonoBehaviour
{
    private GameObject dotPrefab;

    private void Start()
    {
        throw new NotImplementedException();
    }

    public void UpdateDisplay(int countCollectedStars)
    {
        Debug.Log("Update Progress Bar, collected " + countCollectedStars + " stars");
    }
}
