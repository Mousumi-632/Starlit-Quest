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
    }

    public void OnGazeExit()
    {
        if (hasBeenSelected) return;
        objectRenderer.material = gazeDefaultMaterial;
    }

    public void OnGazeSelect()
    {
        if (hasBeenSelected) return;

        hasBeenSelected = true;
        StartCoroutine(AsyncGazeSelection());
    }

    private IEnumerator AsyncGazeSelection()
    {
        objectRenderer.material = gazeCompleteMaterial;

        yield return transform.DOShakePosition(1f, 0.1f, 15).WaitForCompletion();

        Sequence shrinkAndMove = DOTween.Sequence();
        shrinkAndMove.Join(transform.DOMove(moveTargetTransform.position, moveDuration).SetEase(Ease.InOutSine));
        shrinkAndMove.Join(transform.DOScale(endScale, moveDuration).SetEase(Ease.InOutSine));

        yield return shrinkAndMove.WaitForCompletion();
    }
}
