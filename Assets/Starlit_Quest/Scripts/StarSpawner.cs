using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarSpawner : MonoBehaviour
{
    [System.Serializable]
    public class StarConfig
    {
        public GameObject starPrefab;
        public float spawnRadius = 5f;
        public float spawnHeight = 5f;
        public float minAngle = -90f;
        public float maxAngle = 90f;
    }

    [SerializeField] private Transform centerPoint;
    [SerializeField] private List<StarConfig> starTypes = new List<StarConfig>();
    [SerializeField] private float startDelay = 10f;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private int totalStarsToSpawn = 5;

    private List<StarConfig> unusedStarTypes = new List<StarConfig>();
    private int spawnedCount = 0;

    public int CurrentStarIndex { get; private set; } = -1;

    private void Start()
    {
        RefreshStarPool();
        StartCoroutine(DelayedInitialization());
    }

    private IEnumerator DelayedInitialization()
    {
        yield return new WaitForSeconds(startDelay);
        StarCounter.Instance.OnStarsChanged += HandleStarCollected;
        StartCoroutine(SpawnStarWithDelay());
    }

    private void OnDestroy()
    {
        if (StarCounter.Instance != null)
            StarCounter.Instance.OnStarsChanged -= HandleStarCollected;
    }

    private void HandleStarCollected(int count)
    {
        if (spawnedCount < totalStarsToSpawn)
        {
            StartCoroutine(SpawnStarWithDelay());
        }
    }

    private IEnumerator SpawnStarWithDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnStar();
    }

    private void SpawnStar()
    {
        if (spawnedCount >= totalStarsToSpawn)
            return;

        if (unusedStarTypes.Count == 0)
        {
            RefreshStarPool();
            if (unusedStarTypes.Count == 0)
                return;
        }

        int randomIndex = Random.Range(0, unusedStarTypes.Count);
        StarConfig selected = unusedStarTypes[randomIndex];
        CurrentStarIndex = starTypes.IndexOf(selected);
        unusedStarTypes.RemoveAt(randomIndex);

        float angle = Random.Range(selected.minAngle, selected.maxAngle) * Mathf.Deg2Rad;
        Vector3 offsetXZ = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * selected.spawnRadius;
        Vector3 spawnPosition = centerPoint.position + offsetXZ + Vector3.up * selected.spawnHeight;

        GameObject star = Instantiate(selected.starPrefab, spawnPosition, Quaternion.identity);
        Vector3 directionToCenter = (centerPoint.position - spawnPosition).normalized;
        star.transform.rotation = Quaternion.LookRotation(directionToCenter);
        star.transform.Rotate(0f, -90f, 0f);

        spawnedCount++;
    }

    private void RefreshStarPool()
    {
        unusedStarTypes = new List<StarConfig>(starTypes);
    }
}
