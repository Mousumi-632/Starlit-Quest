using UnityEngine;

[RequireComponent(typeof(IGazeProvider))]
public class GazeInteractor : MonoBehaviour
{
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float dwellTime = 2f;
    [SerializeField] private LayerMask targetLayer;

    private IGazeProvider gazeProvider;
    private GameObject currentTarget;
    private float gazeTimer;

    void Awake()
    {
        gazeProvider = GetComponent<IGazeProvider>();
        if (gazeProvider == null)
            Debug.LogError("GazeInteractor: No IGazeProvider found.");
    }

    void Update()
    {
        Ray ray = new Ray(gazeProvider.Origin, gazeProvider.Direction);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, targetLayer))
        {
            var target = hit.collider.gameObject;

            if (target != currentTarget)
            {
                ClearCurrentTarget();

                currentTarget = target;
                gazeTimer = 0f;

                target.GetComponent<IGazeResponder>()?.OnGazeEnter();
            }

            gazeTimer += Time.deltaTime;
            if (gazeTimer >= dwellTime)
            {
                currentTarget.GetComponent<IGazeResponder>()?.OnGazeSelect();
                ClearCurrentTarget(); // Reset after selection
            }
        }
        else
        {
            ClearCurrentTarget();
        }
    }

    private void ClearCurrentTarget()
    {
        if (currentTarget)
        {
            currentTarget.GetComponent<IGazeResponder>()?.OnGazeExit();
        }

        currentTarget = null;
        gazeTimer = 0f;
    }
}

