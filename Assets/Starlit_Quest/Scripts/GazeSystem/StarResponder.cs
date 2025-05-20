using UnityEngine;
using DG.Tweening;

public class StarResponder : MonoBehaviour, IGazeResponder
{
    [SerializeField] private Vector3 moveTargetPosition = new Vector3(-1.03f, 1.74f, 0.617f);
    [SerializeField] private float moveDuration = 1f;

    [Header("Materials")]
    [SerializeField] private Material gazeDefaultMaterial;
    [SerializeField] private Material gazeOngoingMaterial;
    [SerializeField] private Material gazeCompleteMaterial;

    private Renderer objectRenderer;
    private bool hasBeenSelected = false;

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null && gazeDefaultMaterial != null)
        {
            objectRenderer.material = gazeDefaultMaterial;
        }
    }

    public void OnGazeEnter()
    {
        if (hasBeenSelected) return;

        Debug.Log("Star hovered: " + name);
        if (objectRenderer != null && gazeOngoingMaterial != null)
        {
            objectRenderer.material = gazeOngoingMaterial;
        }
    }

    public void OnGazeExit()
    {
        if (hasBeenSelected) return;

        Debug.Log("Star gaze exited: " + name);
        if (objectRenderer != null && gazeDefaultMaterial != null)
        {
            objectRenderer.material = gazeDefaultMaterial;
        }
    }

    public void OnGazeSelect()
    {
        if (hasBeenSelected) return;

        hasBeenSelected = true;

        Debug.Log("Star selected: " + name);

        if (objectRenderer != null && gazeCompleteMaterial != null)
        {
            objectRenderer.material = gazeCompleteMaterial;
        }
        else
        {
            Debug.LogWarning("Renderer or Gaze Complete Material missing on " + name);
        }

        
        transform.DOMove(moveTargetPosition, moveDuration).SetEase(Ease.InOutSine);
        
    }
}

