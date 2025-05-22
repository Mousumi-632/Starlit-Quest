using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private GameObject cometSpawnerPrefab;
    [SerializeField] private GameObject starSpawnerPrefab;
    [SerializeField] private GameObject XROriginPrefab;
    [SerializeField] private GameObject starCounterPrefab;
    [SerializeField] private GameObject npcPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InstantiateIfNotNull(XROriginPrefab);
    }

    private void Start()
    {
        InstantiateIfNotNull(starCounterPrefab);
        InstantiateIfNotNull(starSpawnerPrefab);
        InstantiateIfNotNull(npcPrefab);

        if (StarCounter.Instance != null)
            StarCounter.Instance.OnStarsChanged += OnStarCollected;
    }

    private void OnStarCollected(int count)
    {
        
        if (count == 1)
            InstantiateIfNotNull(cloudPrefab);
        else if (count == 2)
            InstantiateIfNotNull(cometSpawnerPrefab);

        
        if (StarCounter.Instance != null && count >= StarCounter.Instance.MaxStars)
            HandleGameFinished();
    }

    private void InstantiateIfNotNull(GameObject prefab)
    {
        if (prefab != null)
            Instantiate(prefab);
    }

    private void HandleGameFinished()
    {
        // Game finished logic here
        
    }
}

