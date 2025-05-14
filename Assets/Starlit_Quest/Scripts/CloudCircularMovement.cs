using UnityEngine;
using DG.Tweening;

public class CloudDOTweenMover : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float duration = 4f;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float height = 5f;  // Height above ground

    void Start()
    {
        Vector3[] path = new Vector3[361];
        for (int i = 0; i <= 360; i++)
        {
            float rad = Mathf.Deg2Rad * i;
            // Set Y to desired sky height instead of centerPoint's Y
            path[i] = new Vector3(
                centerPoint.position.x + Mathf.Cos(rad) * radius,
                height,
                centerPoint.position.z + Mathf.Sin(rad) * radius
            );
        }

        // Place cloud at the start of the path (in the sky)
        transform.position = path[0];

        // Start circular tween
        transform.DOPath(path, duration, PathType.Linear)
                 .SetLoops(-1)
                 .SetEase(Ease.Linear);
    }
}
