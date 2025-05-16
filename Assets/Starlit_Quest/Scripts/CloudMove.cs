using UnityEngine;
using DG.Tweening;

public class CloudMove : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float duration = 4f;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float height = 5f;

    private Vector3[] path;

    void Start()
    {
        path = new Vector3[361];
        for (int i = 0; i <= 360; i++)
        {
            float rad = Mathf.Deg2Rad * i;
            path[i] = new Vector3(
                centerPoint.position.x + Mathf.Cos(rad) * radius,
                height,
                centerPoint.position.z + Mathf.Sin(rad) * radius
            );
        }

        transform.position = path[0];

        transform.DOPath(path, duration, PathType.Linear)
                 .SetLoops(-1)
                 .SetEase(Ease.Linear)
                 .OnUpdate(() => FaceCenter());
    }

    void FaceCenter()
    {
        Vector3 direction = centerPoint.position - transform.position;
        direction.y = 0; 
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        // correction of the axis if needed
        // Vector3 direction = centerPoint.position - transform.position;
        // direction.y = 0; 

        // if (direction != Vector3.zero)
        // {
            
            //Quaternion lookRotation = Quaternion.LookRotation(direction);

            
            //Quaternion offset = Quaternion.Euler(0, 90, 0);

            //transform.rotation = lookRotation * offset;
        //}
    }
}
