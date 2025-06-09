using UnityEngine;
using System.Collections;

public class MenuSceneController : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject startButtonPrefab;

    private AudioSource audioSource;

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ShowStartButtonAfterAudio());
    }

    private IEnumerator ShowStartButtonAfterAudio()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        Instantiate(startButtonPrefab, transform);
    }

    public void LoadStarlitQuestScene()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GoToStarlitQuestScene();
        }
    }
}
