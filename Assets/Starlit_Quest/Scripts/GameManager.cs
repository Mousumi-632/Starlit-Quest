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
        switch (count)
        {
            case 1:
                InstantiateIfNotNull(cloudPrefab);
                break;
            case 2:
                InstantiateIfNotNull(cometSpawnerPrefab);
                break;
            case 3:
                HandleGameFinished();
                break;
        }
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


