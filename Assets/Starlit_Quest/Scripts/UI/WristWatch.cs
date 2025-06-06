using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class WristWatch : MonoBehaviour
{
    [Header("Target Stars")] 
    [SerializeField] private RawImage targetStarImage;
    [SerializeField] private List<Texture> targetStars;

    [Header("Progress Bar")]
    [SerializeField] private DotProgressBar dotProgressBar;

    [Header("UI VFX")]
    [SerializeField] private Transform vfxPlaceholder;
    [SerializeField] private GameObject vfxAStarFound;
    [SerializeField] private GameObject vfxAllStarsFound;
    [SerializeField] private List<Texture> emojis;

    [Header("Audio Feedback")]
    [SerializeField] private AudioClip AStarIsCollectedClip;
    [SerializeField] private AudioClip AllStarsAreCollectedClip;

    private bool isWatchInitialized = false;
    private int numCollectedStars = 0;
    private StarSpawner starSpawner;
    private int currentStarIndex;
    private AudioSource audioSource;

    private void Start()
    {
        // initialize audio setup
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 15f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        
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
        starSpawner.OnStarSpawned += DisplayNewStar; // event invoked when a new star spawned in sky
        
        if (StarCounter.Instance == null) return;
        dotProgressBar.Initialize(StarCounter.Instance.MaxStars);
        StarCounter.Instance.OnStarsChanged += CelebrateAStarCollected; // event invoked when a found star reaches glass jar
        
        isWatchInitialized = true;
    }
    
    private void CelebrateAStarCollected(int count)
    {
        numCollectedStars = count;
        StartCoroutine(AsyncCelebrateAStarCollected());
    }

    private IEnumerator AsyncCelebrateAStarCollected()
    {
        yield return new WaitForSeconds(1f);
        if (numCollectedStars < StarCounter.Instance.MaxStars)
        {
            Instantiate(vfxAStarFound, vfxPlaceholder);
        }
        else if (numCollectedStars == StarCounter.Instance.MaxStars)
        {
            Instantiate(vfxAllStarsFound, vfxPlaceholder);
            audioSource.clip = AllStarsAreCollectedClip;
            audioSource.Play();
        }
        else
        {
            yield break;
        }
        
        yield return targetStarImage.transform.DOScale(0f, 1f).SetEase(Ease.InOutQuad).WaitForCompletion();
        targetStarImage.texture = null;
        if (numCollectedStars > emojis.Count) numCollectedStars = emojis.Count;
        targetStarImage.texture = emojis[numCollectedStars - 1];
        yield return targetStarImage.transform.DOScale(1.2f, 0.7f).SetEase(Ease.InOutQuad).WaitForCompletion();
        yield return targetStarImage.transform.DOScale(0.8f, 0.4f).SetEase(Ease.InOutQuad).WaitForCompletion();
        yield return targetStarImage.transform.DOScale(1.2f, 0.4f).SetEase(Ease.InOutQuad).WaitForCompletion();
        yield return targetStarImage.transform.DOScale(0.8f, 0.4f).SetEase(Ease.InOutQuad).WaitForCompletion();
        yield return targetStarImage.transform.DOScale(1.2f, 0.4f).SetEase(Ease.InOutQuad).WaitForCompletion();
        yield return targetStarImage.transform.DOScale(1f, 0.2f).SetEase(Ease.InOutQuad).WaitForCompletion();
    }

    private void DisplayNewStar(int starIndex)
    {
        currentStarIndex = starIndex;
        if (numCollectedStars < targetStars.Count - 1)
        {
            StartCoroutine(AsyncDisplayNewStar());
        }
    }

    private IEnumerator AsyncDisplayNewStar()
    {
        yield return targetStarImage.transform.DOScale(0f, 1f).SetEase(Ease.InOutQuad).WaitForCompletion();
        UpdateTargetStarDisplay();
        audioSource.clip = AStarIsCollectedClip;
        audioSource.Play();
        yield return targetStarImage.transform.DOScale(1f, 1f).SetEase(Ease.InOutQuad).WaitForCompletion();
        
        dotProgressBar.UpdateDisplay(numCollectedStars);
    }

    private void UpdateTargetStarDisplay()
    {
        targetStarImage.texture = null;
        if (currentStarIndex < 0) currentStarIndex = 0;
        if (currentStarIndex >= targetStars.Count - 1) currentStarIndex = targetStars.Count - 2;
        
        targetStarImage.texture = targetStars[currentStarIndex];
    }
}
