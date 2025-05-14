using UnityEngine;

public class ExampleStarResponder : MonoBehaviour, IGazeResponder
{
    [SerializeField] private Transform moveTarget;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Material highlightMaterial;

    private Material originalMaterial;
    private Renderer cachedRenderer;

    void Awake()
    {
        cachedRenderer = GetComponent<Renderer>();
        if (cachedRenderer != null)
        {
            originalMaterial = cachedRenderer.material;
        }
    }

    public void OnGazeEnter()
    {
        Debug.Log("Star hovered: " + name);

        if (cachedRenderer != null && highlightMaterial != null)
        {
            cachedRenderer.material = highlightMaterial;
        }
    }

    public void OnGazeExit()
    {
        Debug.Log("Star gaze exited: " + name);

        if (cachedRenderer != null && originalMaterial != null)
        {
            cachedRenderer.material = originalMaterial;
        }
    }

    public void OnGazeSelect()
    {
        Debug.Log("Star selected: " + name);

        if (moveTarget != null)
        {
            StartCoroutine(MoveToTarget());
        }
    }

    private System.Collections.IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, moveTarget.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // snap into jar, play sound, etc.
    }
}

