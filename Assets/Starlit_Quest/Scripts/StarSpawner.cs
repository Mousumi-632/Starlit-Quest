using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private Transform centerPoint; 
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private float spawnHeight = 5f;

    private void Start()
    {
        StarCounter.Instance.OnStarsChanged += HandleStarCollected;
        SpawnStar();
    }

    private void OnDestroy()
    {
        if (StarCounter.Instance != null)
            StarCounter.Instance.OnStarsChanged -= HandleStarCollected;
    }

    private void HandleStarCollected(int count)
    {
        SpawnStar();
    }

    private void SpawnStar()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 offsetXZ = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * spawnRadius;
        Vector3 spawnPosition = centerPoint.position + offsetXZ + Vector3.up * spawnHeight;

        GameObject star = Instantiate(starPrefab, spawnPosition, Quaternion.identity);

        
        Vector3 directionToCenter = (centerPoint.position - spawnPosition).normalized;
        star.transform.up = directionToCenter;
    }
}

