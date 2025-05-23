using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        // For debugging: load StarlitQuestScene after 5 seconds
        Invoke(nameof(LoadStarlitQuestScene), 5f);
    }

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void LoadStarlitQuestScene()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GoToStarlitQuestScene();
        }
    }
}

