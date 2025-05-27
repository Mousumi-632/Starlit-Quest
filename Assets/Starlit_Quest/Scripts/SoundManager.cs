using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Group 1: Background")]
    public AudioClip backgroundClip;

    [Header("Group 2: Gaze Interactions")]
    public AudioClip onGazeEnterClip;
    public AudioClip onGazeExitClip;
    public AudioClip onGazeSelectClip;

    [Header("Group 3: Star Collection")]
    public AudioClip starCollectedClip;

    [Header("Group 4: NPC Dialogues")]
    public AudioClip npcIntroClip;      
    public AudioClip npcOneClip;        
    public AudioClip npcTwoClip;        
    public AudioClip npcThreeClip;      

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBackground()
    {
        audioSource.loop = true;
        audioSource.clip = backgroundClip;
        audioSource.Play();
    }

    public void PlayGazeEnter() => PlaySound(onGazeEnterClip);
    public void PlayGazeExit() => PlaySound(onGazeExitClip);
    public void PlayGazeSelect() => PlaySound(onGazeSelectClip);
    public void PlayStarCollected() => PlaySound(starCollectedClip);

    public void PlayNPCDialogue(int starsCollected, int maxStars)
    {
        if (starsCollected == 0)
            PlaySound(npcIntroClip);
        else if (starsCollected == 1)
            PlaySound(npcOneClip);
        else if (starsCollected == 2)
            PlaySound(npcTwoClip);
        else if (starsCollected == maxStars)
            PlaySound(npcThreeClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip);
    }
}
