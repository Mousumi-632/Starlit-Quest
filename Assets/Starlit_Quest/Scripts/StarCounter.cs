using UnityEngine;
using System;

public class StarCounter : MonoBehaviour, IStarCounter
{
    public static StarCounter Instance { get; private set; }

    public int StarsCollected { get; private set; } = 0;
    public event Action<int> OnStarsChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddStar()
    {
        StarsCollected++;
        Debug.Log($"[StarCounter] Stars Collected: {StarsCollected}");
        OnStarsChanged?.Invoke(StarsCollected);
    }
}