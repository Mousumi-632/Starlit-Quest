// CloudMover.cs
using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection = new Vector3(1, 0, 0); // Move right by default
    [SerializeField] private float moveSpeed = 1f; // Units per second

    void Update()
    {
        // Move the cloud constantly in the given direction
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }
}
