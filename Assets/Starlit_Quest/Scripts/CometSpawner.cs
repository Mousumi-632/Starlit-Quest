using UnityEngine;

public class CometSpawner : MonoBehaviour
{
    [Header("Comet Settings")]
    [SerializeField] private GameObject cometPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float cometSpeed = 5f;
    [SerializeField] private float cometLifetime = 5f;

    [Header("Spawn Area")]
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float spawnHeight = 10f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnComet), 1f, spawnInterval);
    }

    private void SpawnComet()
    {
        if (cometPrefab == null)
        {
            Debug.LogError("🚨 Comet Prefab is not assigned!");
            return;
        }

        // Random horizontal direction (XZ only)
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        // Spawn position: random XZ offset + upward height
        Vector3 offsetXZ = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0f, Random.Range(-spawnRadius, spawnRadius));
        Vector3 spawnPosition = transform.position + offsetXZ + Vector3.up * spawnHeight;

        // Create comet
        GameObject comet = Instantiate(cometPrefab, spawnPosition, Quaternion.identity);

        // Apply movement
        Rigidbody rb = comet.GetComponent<Rigidbody>();
        if (rb == null) rb = comet.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = randomDir * cometSpeed;

        // Destroy comet after set time
        Destroy(comet, cometLifetime);
    }
}
