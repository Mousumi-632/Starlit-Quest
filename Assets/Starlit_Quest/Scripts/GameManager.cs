using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private GameObject cometSpawnerPrefab;
    [SerializeField] private GameObject starSpawnerPrefab;

    private void Start()
    {
        Instantiate(starSpawnerPrefab);
        StarCounter.Instance.OnStarsChanged += OnStarCollected;
    }

    private void OnStarCollected(int count)
    {
        if (count == 1)
        {
            Instantiate(cloudPrefab);
            Instantiate(starSpawnerPrefab);
        }
        else if (count == 2)
        {
            Instantiate(cometSpawnerPrefab);
            Instantiate(starSpawnerPrefab);
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

