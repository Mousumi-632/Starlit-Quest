using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cloudObject;           
    [SerializeField] private GameObject cometSpawnerObject;   
    [SerializeField] private GameObject starSpawnerObject;    

    private void Start()
    {
        if (starSpawnerObject != null)
            starSpawnerObject.SetActive(true);

        StarCounter.Instance.OnStarsChanged += OnStarCollected;

    }

    private void OnStarCollected(int count)
    {
        if (count == 1)
        {
            if (cloudObject != null)
                cloudObject.SetActive(true);
        }
        else if (count == 2)
        {
            if (cometSpawnerObject != null)
                cometSpawnerObject.SetActive(true);
        }
        else if (count == 3)
        {
            HandleGameFinished();
        }
    }

    private void HandleGameFinished()
    {
        // Game finished logic here
    }
}


