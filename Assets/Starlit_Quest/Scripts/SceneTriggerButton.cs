using UnityEngine;

public class SceneTriggerButton : MonoBehaviour
{
    [SerializeField] private MenuSceneController sceneController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller") && sceneController != null)
        {
            sceneController.LoadStarlitQuestScene();
        }
    }
}
