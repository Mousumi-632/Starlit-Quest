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
    public AudioClip npcOneClip;
    public AudioClip npcTwoClip;
    public AudioClip npcThreeClip;

    private AudioSource npcSource;

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

        // Subscribe to star updates
        StarCounter.Instance.OnStarsChanged += OnStarsChanged;

        // Play current state just in case stars were added before this subscribed
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
        Debug.Log($"NPC Dialogue Triggered: Stars = {starsCollected}, Max = {maxStars}");

        if (starsCollected == 0)
            PlaySound(npcIntroClip);
        else if (starsCollected == maxStars)
            PlaySound(npcThreeClip);
        else if (starsCollected == 2)
            PlaySound(npcTwoClip);
        else if (starsCollected == 1)
            PlaySound(npcOneClip);
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null || npcSource == null) return;
        npcSource.clip = clip;
        npcSource.Play();
    }
}
