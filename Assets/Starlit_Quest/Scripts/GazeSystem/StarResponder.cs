using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StarResponder : MonoBehaviour, IGazeResponder
{
    [SerializeField] private Transform moveTargetTransform;
    [SerializeField] private float moveDuration = 1f;

    [Header("Gaze Feedback")]
    [SerializeField] private Material gazeDefaultMaterial;
    [SerializeField] private Material gazeOngoingMaterial;
    [SerializeField] private Material gazeCompleteMaterial;

    [Header("Shrink Settings")]
    [SerializeField] private Vector3 startScale = Vector3.one;
    [SerializeField] private Vector3 endScale = new Vector3(0.1f, 0.1f, 0.1f);

    [Header("Gaze Failure Settings")]
    [SerializeField] private float gazeFailureDelay = 10f;

    [Header("Reposition Settings")]
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private float spawnHeight = 5f;
    [SerializeField] private float minAngle = -90f;
    [SerializeField] private float maxAngle = 90f;

    private Renderer objectRenderer;
    private bool hasBeenSelected = false;
    private Coroutine gazeFailureCoroutine;

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer == null || gazeDefaultMaterial == null || gazeOngoingMaterial == null ||
            gazeCompleteMaterial == null || moveTargetTransform == null || centerPoint == null)
        {
            Debug.LogError("Missing required serialized fields on " + gameObject.name);
            Destroy(gameObject);
        }

        objectRenderer.material = gazeDefaultMaterial;
        transform.localScale = startScale;
    }

    public void OnGazeEnter()
    {
        if (hasBeenSelected) return;

        objectRenderer.material = gazeOngoingMaterial;
        SoundManager.Instance.PlayGazeEnter();

    
        if (gazeFailureCoroutine != null)
        {
            StopCoroutine(gazeFailureCoroutine);
            gazeFailureCoroutine = null;
        }
    }

    public void OnGazeExit()
    {
        if (hasBeenSelected) return;

        objectRenderer.material = gazeDefaultMaterial;
        SoundManager.Instance.PlayGazeExit();

    
        if (gazeFailureCoroutine != null)
        {
            StopCoroutine(gazeFailureCoroutine);
        }
        gazeFailureCoroutine = StartCoroutine(GazeFailureCountdown());
    }

    public void OnGazeSelect()
    {
        if (hasBeenSelected) return;

        hasBeenSelected = true;

        if (gazeFailureCoroutine != null)
        {
            StopCoroutine(gazeFailureCoroutine);
            gazeFailureCoroutine = null;
        }

        StartCoroutine(AsyncGazeSelection());
        SoundManager.Instance.PlayGazeSelect();
    }

    private IEnumerator GazeFailureCountdown()
    {
        yield return new WaitForSeconds(gazeFailureDelay);

        if (!hasBeenSelected)
        {
            OnGazeFailure();
        }

        gazeFailureCoroutine = null;
    }

    public void OnGazeFailure()
    {
        Debug.Log($"{gameObject.name} gaze failed. Deactivating and repositioning...");

        NPCSoundManager npcSoundManager = FindAnyObjectByType<NPCSoundManager>();
        if (npcSoundManager != null)
        {
            npcSoundManager.NpcGazeFailure();
        }
        else
        {
            Debug.LogWarning("NPCSoundManager not found in the scene.");
        }

        StartCoroutine(HandleGazeFailure());
    }

    private IEnumerator HandleGazeFailure()
    {
        Debug.Log($"{gameObject.name} - Starting gaze failure handling...");

       
        SetActiveVisuals(false);

        yield return new WaitForSeconds(10f);

        float angle = Random.Range(minAngle, maxAngle) * Mathf.Deg2Rad;
        Vector3 offsetXZ = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * spawnRadius;
        Vector3 newPosition = centerPoint.position + offsetXZ + Vector3.up * spawnHeight;

        transform.position = newPosition;

        Vector3 directionToCenter = (centerPoint.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(directionToCenter);
        transform.Rotate(0f, -90f, 0f);

        SetActiveVisuals(true);

        Debug.Log($"{gameObject.name} - Repositioned and reactivated visuals.");
    }

    private void SetActiveVisuals(bool active)
    {
        if (objectRenderer != null)
            objectRenderer.enabled = active;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = active;
    }

    private IEnumerator AsyncGazeSelection()
    {
        objectRenderer.material = gazeCompleteMaterial;

        yield return transform.DOShakePosition(1f, 0.1f, 15).WaitForCompletion();

        Vector3 startPos = transform.position;
        Vector3 targetPos = moveTargetTransform.position;
        float spiralRadius = 0.5f;
        int spiralTurns = 3;
        float duration = moveDuration;

        float elapsed = 0f;
        Vector3 toTarget = targetPos - startPos;
        Vector3 axis = Vector3.Cross(Vector3.up, toTarget).normalized;
        if (axis == Vector3.zero) axis = Vector3.right;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            float angle = t * spiralTurns * 360f;
            float radius = spiralRadius * (1 - t);

            Vector3 forward = Vector3.Lerp(startPos, targetPos, t);
            Vector3 offset = Quaternion.AngleAxis(angle, toTarget.normalized) * (axis * radius);

            transform.position = forward + offset;
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        transform.position = targetPos;
        transform.localScale = endScale;
    }
}

