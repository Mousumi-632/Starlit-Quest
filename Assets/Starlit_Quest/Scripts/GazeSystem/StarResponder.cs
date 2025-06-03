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

    private Renderer objectRenderer;
    private bool hasBeenSelected = false;

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer == null || gazeDefaultMaterial == null || gazeOngoingMaterial == null ||
            gazeCompleteMaterial == null || moveTargetTransform == null)
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
    }

    public void OnGazeExit()
    {
        if (hasBeenSelected) return;
        objectRenderer.material = gazeDefaultMaterial;
        SoundManager.Instance.PlayGazeExit();
    }

    public void OnGazeSelect()
    {
        if (hasBeenSelected) return;

        hasBeenSelected = true;
        StartCoroutine(AsyncGazeSelection());
        SoundManager.Instance.PlayGazeSelect();
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
        if (axis == Vector3.zero) axis = Vector3.right; // fallback if movement is vertical

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
