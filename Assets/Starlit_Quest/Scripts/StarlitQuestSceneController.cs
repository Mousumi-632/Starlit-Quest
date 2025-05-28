using UnityEngine;

public class StarlitQuestSceneController : MonoBehaviour
{
    public static StarlitQuestSceneController Instance { get; private set; }

   
    [SerializeField] private GameObject starCounterPrefab;
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private GameObject starSpawnerPrefab;
    [SerializeField] private GameObject soundManagerPrefab;

    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private GameObject cometSpawnerPrefab;
  

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        InstantiateIfNotNull(soundManagerPrefab);
        InstantiateIfNotNull(starCounterPrefab);
        InstantiateIfNotNull(starSpawnerPrefab);
        InstantiateIfNotNull(npcPrefab);
        SoundManager.Instance.PlayBackground();

        if (StarCounter.Instance != null)
            StarCounter.Instance.OnStarsChanged += OnStarCollected;
    }

    public void Initialize(GameManager gameManager)
    {
        // 
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
