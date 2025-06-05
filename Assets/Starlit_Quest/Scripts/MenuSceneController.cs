using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    private GameManager gameManager;

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void LoadStarlitQuestScene()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GoToStarlitQuestScene();
        }
    }
}