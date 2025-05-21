using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private GameObject cometSpawnerPrefab;
    [SerializeField] private GameObject starSpawnerPrefab;
    [SerializeField] private GameObject XROriginPrefab;
    [SerializeField] private GameObject starCounterPrefab;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instantiate(XROriginPrefab);   
    }

        private void Start()
    {
        if (starCounterPrefab != null)
            Instantiate(starCounterPrefab);
        if (starSpawnerPrefab != null)
            Instantiate(starSpawnerPrefab);

        StarCounter.Instance.OnStarsChanged += OnStarCollected;
    }

    private void OnStarCollected(int count)
    {
        if (count == 1)
        {
            if (cloudPrefab != null)
                Instantiate(cloudPrefab);

            if (starSpawnerPrefab != null)
                Instantiate(starSpawnerPrefab);
        }
        else if (count == 2)
        {
            if (cometSpawnerPrefab != null)
                Instantiate(cometSpawnerPrefab);

            if (starSpawnerPrefab != null)
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


