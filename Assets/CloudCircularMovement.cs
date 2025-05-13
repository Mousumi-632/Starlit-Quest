using UnityEngine;
using DG.Tweening;

public class CloudDOTweenMover : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float duration = 4f;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float height = 10f; // Height above ground (Y position)

    void Start()
    {
        Vector3[] path = new Vector3[361];
        for (int i = 0; i <= 360; i++)
        {
            float rad = Mathf.Deg2Rad * i;
            path[i] = new Vector3(
                centerPoint.position.x + Mathf.Cos(rad) * radius,
                height, // Fixed height
                centerPoint.position.z + Mathf.Sin(rad) * radius
            );
        }

        transform.DOPath(path, duration, PathType.Linear)
                 .SetLoops(-1)
                 .SetEase(Ease.Linear);
    }
}
