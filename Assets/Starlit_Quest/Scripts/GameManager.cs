using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private GameObject XROriginPrefab;

    private bool inTransition = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Instantiate(XROriginPrefab, transform);
    }

    public Coroutine GoToBootScene()
    {
        return StartCoroutine(LoadScene("BootScene", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<BootSceneController>();
            sceneController.Initialize(this);
        }));
    }

    public Coroutine GoToMenuScene()
    {
        return StartCoroutine(LoadScene("MenuScene", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<MenuSceneController>();
            sceneController.Initialize(this);
        }));
    }

    public Coroutine GoToStarlitQuestScene()
    {
        return StartCoroutine(LoadScene("Scene_Beta_Dev2", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<StarlitQuestSceneController>();
            sceneController.Initialize(this);
        }));
    }

    private IEnumerator LoadScene(string sceneName, Action sceneLoadedCallback)
    {
        if (inTransition)
            yield break;

        inTransition = true;

        yield return SceneManager.LoadSceneAsync(sceneName);
        sceneLoadedCallback?.Invoke();

        inTransition = false;
    }
}
