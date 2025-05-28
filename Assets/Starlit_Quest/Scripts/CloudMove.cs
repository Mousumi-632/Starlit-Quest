using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class CloudMove : MonoBehaviour
{
    [System.Serializable]
    public class CloudConfig
    {
        public Transform cloudTransform;
        public float radius = 5f;
        public float duration = 4f;
        public float height = 5f;
        public Transform centerPoint;       // Center of the orbit (e.g. XR Origin)
        public float startAngle = 0f;       // Starting angle on the orbit in degrees
    }

    [SerializeField] private List<CloudConfig> clouds = new List<CloudConfig>();

    void Start()
    {
        foreach (var config in clouds)
        {
            Vector3[] path = GenerateCircularPath(config);

            config.cloudTransform.position = path[0];

            // Set initial 45° X-axis tilt
            config.cloudTransform.rotation = Quaternion.Euler(45f, 0f, 0f);

            config.cloudTransform.DOPath(path, config.duration, PathType.Linear)
                .SetLoops(-1)
                .SetEase(Ease.Linear)
                .OnUpdate(() => FaceCenter(config.cloudTransform, config.centerPoint));
        }
    }

    Vector3[] GenerateCircularPath(CloudConfig config)
    {
        Vector3[] path = new Vector3[361];
        for (int i = 0; i <= 360; i++)
        {
            float angleDeg = i + config.startAngle;
            float rad = Mathf.Deg2Rad * angleDeg;

            path[i] = new Vector3(
                config.centerPoint.position.x + Mathf.Cos(rad) * config.radius,
                config.height,
                config.centerPoint.position.z + Mathf.Sin(rad) * config.radius
            );
        }
        return path;
    }

    void FaceCenter(Transform cloud, Transform centerPoint)
    {
        Vector3 direction = centerPoint.position - cloud.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Quaternion currentRotation = cloud.rotation;

            // Preserve X-axis tilt while rotating to face the center
            cloud.rotation = Quaternion.Euler(
                currentRotation.eulerAngles.x,
                lookRotation.eulerAngles.y,
                currentRotation.eulerAngles.z
            );
        }
    }
}
