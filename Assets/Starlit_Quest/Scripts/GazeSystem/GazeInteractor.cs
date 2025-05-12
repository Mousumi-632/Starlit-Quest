using UnityEngine;

[RequireComponent(typeof(IGazeProvider))]
public class GazeInteractor : MonoBehaviour
{
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float dwellTime = 2f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Material highlightMaterial;

    private IGazeProvider gazeProvider;
    private GameObject currentTarget;
    private float gazeTimer;
    private Material originalMaterial;

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

                var renderer = target.GetComponent<Renderer>();
                if (renderer)
                {
                    originalMaterial = renderer.material;
                    renderer.material = highlightMaterial;
                }

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
            var renderer = currentTarget.GetComponent<Renderer>();
            if (renderer && originalMaterial)
                renderer.material = originalMaterial;

            currentTarget.GetComponent<IGazeResponder>()?.OnGazeExit();
        }

        currentTarget = null;
        gazeTimer = 0f;
        originalMaterial = null;
    }
}
