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


    public void PlaySound(AudioClip clip)
    {
        if (clip == null || npcSource == null) return;
        npcSource.clip = clip;
        npcSource.Play();
    }
}
