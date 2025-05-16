using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private Transform cloudTransform;
    [SerializeField] private float behindDistance = 1f;
    [SerializeField] private float starHeight = 5f;

    void Start()
    {
        SpawnStarBehindCloud();
    }

    void SpawnStarBehindCloud()
    {
        // 🧠 Use the opposite of cloud's forward direction to find "behind"
        Vector3 spawnPosition = cloudTransform.position - cloudTransform.forward * behindDistance;

        // Match the height to the cloud
        spawnPosition.y = starHeight;

        // Spawn the star at the calculated position
        Instantiate(starPrefab, spawnPosition, Quaternion.identity);
    }
}
