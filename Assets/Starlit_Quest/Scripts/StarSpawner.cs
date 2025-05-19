using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private float spawnDistance = 5f;
    [SerializeField] private float starHeight = 5f;

    private GameObject spawnedStar;

    void Start()
    {
        SpawnOrMoveStar();
    }

    void SpawnOrMoveStar()
    {
        // Generate random direction on the XZ plane
        Vector2 randomDirection2D = Random.insideUnitCircle.normalized;
        Vector3 randomDirection3D = new Vector3(randomDirection2D.x, 0f, randomDirection2D.y);

        // Final spawn position at a distance and height
        Vector3 spawnPosition = transform.position + randomDirection3D * spawnDistance;
        spawnPosition.y = starHeight;

        if (spawnedStar == null)
        {
            // Instantiate only once
            spawnedStar = Instantiate(starPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            // Just move the existing one
            spawnedStar.transform.position = spawnPosition;
        }

        // Optional: Make the star face the spawner (or camera/player)
        spawnedStar.transform.LookAt(transform);
    }
}
