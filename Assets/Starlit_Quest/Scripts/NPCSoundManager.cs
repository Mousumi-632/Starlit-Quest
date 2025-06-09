using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class NPCSoundManager : MonoBehaviour
{
    [Header("Mixer Group")]
    public AudioMixerGroup npcGroup;

    [Header("NPC Dialogues (3D Audio)")]
    public AudioClip npcIntroClip;
    public AudioClip npcDistracionsClip;
    public AudioClip npcFailureClipA;
    public AudioClip npcFailureClipB;
    public AudioClip npcFailureClipC;
    public AudioClip npcEndClip;

    [Header("Animation")]
    public Animator npcAnimator;
    private readonly string talkingParam = "IsTalking";

    private AudioSource npcSource;
    private int gazeFailureCount = 0;

    private void Awake()
    {
        npcSource = GetComponent<AudioSource>();
        npcSource.outputAudioMixerGroup = npcGroup;
        npcSource.spatialBlend = 1f;
        npcSource.minDistance = 1f;
        npcSource.maxDistance = 15f;
        npcSource.rolloffMode = AudioRolloffMode.Linear;
        npcSource.playOnAwake = false;
        npcSource.loop = false;
    }

    private void Start()
    {
        StartCoroutine(WaitForStarCounter());
    }

    private IEnumerator WaitForStarCounter()
    {
        while (StarCounter.Instance == null)
            yield return null;

        StarCounter.Instance.OnStarsChanged += OnStarsChanged;
        OnStarsChanged(StarCounter.Instance.StarsCollected);
    }

    private void OnDestroy()
    {
        if (StarCounter.Instance != null)
            StarCounter.Instance.OnStarsChanged -= OnStarsChanged;
    }

    private void OnStarsChanged(int starsCollected)
    {
        int maxStars = StarCounter.Instance.MaxStars;
        PlayNPCDialogue(starsCollected, maxStars);
    }

    public void PlayNPCDialogue(int starsCollected, int maxStars)
    {
        if (starsCollected == 0)
            PlaySound(npcIntroClip);
        else if (starsCollected == 1)
            PlaySound(npcDistracionsClip);
        else if (starsCollected == maxStars)
            PlaySound(npcEndClip);
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null || npcSource == null) return;

        npcSource.clip = clip;
        npcSource.Play();

        if (npcAnimator != null)
            npcAnimator.SetBool(talkingParam, true);

        
        StartCoroutine(StopTalkingWhenDone());
    }

    private IEnumerator StopTalkingWhenDone()
    {
        
        while (npcSource.isPlaying)
            yield return null;

        if (npcAnimator != null)
            npcAnimator.SetBool(talkingParam, false);
    }

    public void NpcGazeFailure()
    {
        AudioClip failureClip = null;

        switch (gazeFailureCount % 3)
        {
            case 0:
                failureClip = npcFailureClipA;
                break;
            case 1:
                failureClip = npcFailureClipB;
                break;
            case 2:
                failureClip = npcFailureClipC;
                break;
        }

        PlaySound(failureClip);
        gazeFailureCount++;
    }
}
