using UnityEngine;
using System;

public class StarCounter : MonoBehaviour, IStarCounter
{
    public static StarCounter Instance { get; private set; }

    public int StarsCollected { get; private set; } = 0;

    [SerializeField]
    private int maxStars = 3; 

    public int MaxStars => maxStars; 

    public event Action<int> OnStarsChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddStar()
    {
        if (StarsCollected >= maxStars) return;

        StarsCollected++;
        Debug.Log($"[StarCounter] Stars Collected: {StarsCollected}");
        OnStarsChanged?.Invoke(StarsCollected);
        SoundManager.Instance.PlayStarCollected();
    }
}
