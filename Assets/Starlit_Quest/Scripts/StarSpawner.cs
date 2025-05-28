using UnityEngine;
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
        if (count < 3)
            SpawnStar();
    }

    private void SpawnStar()
    {
        if (starTypes.Count == 0)
            return;

        StarConfig selected = starTypes[Random.Range(0, starTypes.Count)];
        float angle = Random.Range(selected.minAngle, selected.maxAngle) * Mathf.Deg2Rad;
        Vector3 offsetXZ = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * selected.spawnRadius;
        Vector3 spawnPosition = centerPoint.position + offsetXZ + Vector3.up * selected.spawnHeight;

        GameObject star = Instantiate(selected.starPrefab, spawnPosition, Quaternion.identity);
        Vector3 directionToCenter = (centerPoint.position - spawnPosition).normalized;
        star.transform.up = directionToCenter;
    }
}
