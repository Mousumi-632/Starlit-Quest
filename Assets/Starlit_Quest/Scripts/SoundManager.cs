using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Mixer Groups")]
    public AudioMixerGroup backgroundGroup;
    public AudioMixerGroup gazeGroup;
    public AudioMixerGroup starGroup;
    public AudioMixerGroup npcGroup;

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

    private AudioSource backgroundSource;
    private AudioSource gazeSource;
    private AudioSource starSource;
    private AudioSource npcSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;

        
        backgroundSource = CreateAudioSource("Background", backgroundGroup, true);
        gazeSource = CreateAudioSource("Gaze", gazeGroup, false);
        starSource = CreateAudioSource("Star", starGroup, false);
        npcSource = CreateAudioSource("NPC", npcGroup, false);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private AudioSource CreateAudioSource(string name, AudioMixerGroup group, bool loop)
    {
        var obj = new GameObject($"{name}AudioSource");
        obj.transform.parent = transform;
        var src = obj.AddComponent<AudioSource>();
        src.outputAudioMixerGroup = group;
        src.loop = loop;
        return src;
    }

    public void PlayBackground()
    {
        backgroundSource.clip = backgroundClip;
        backgroundSource.Play();
    }

    public void PlayGazeEnter() => PlaySound(gazeSource, onGazeEnterClip);
    public void PlayGazeExit() => PlaySound(gazeSource, onGazeExitClip);
    public void PlayGazeSelect() => PlaySound(gazeSource, onGazeSelectClip);
    public void PlayStarCollected() => PlaySound(starSource, starCollectedClip);

    public void PlayNPCDialogue(int starsCollected, int maxStars)
    {
        if (starsCollected == 0)
            PlaySound(npcSource, npcIntroClip);
        else if (starsCollected == 1)
            PlaySound(npcSource, npcOneClip);
        else if (starsCollected == 2)
            PlaySound(npcSource, npcTwoClip);
        else if (starsCollected == maxStars)
            PlaySound(npcSource, npcThreeClip);
    }

    private void PlaySound(AudioSource source, AudioClip clip)
    {
        if (clip == null || source == null) return;
        source.PlayOneShot(clip);
    }
}
