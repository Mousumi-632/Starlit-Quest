using UnityEngine;
using System.Collections;

public class JarGlowController : MonoBehaviour
{
    [SerializeField] private Renderer jarRenderer;
    [SerializeField] private int maxStars = 5;
    [SerializeField] private Color glowColor = Color.yellow;
    [SerializeField] private float minGlow = 0.2f;
    [SerializeField] private float maxGlow = 5f;
    [SerializeField] private float glowTransitionTime = 0.5f;
    [SerializeField] private AnimationCurve glowCurve = AnimationCurve.Linear(0, 0, 1, 1);

    private int currentStars = 0;
    private Material glowMaterial;
    private Coroutine glowCoroutine;

    private void Start()
    {
        if (jarRenderer == null)
            jarRenderer = GetComponent<Renderer>();

        glowMaterial = jarRenderer.materials[1];
        UpdateGlow(); // start with correct glow
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Star"))
        {
            currentStars = Mathf.Min(currentStars + 1, maxStars);
            UpdateGlow();
        }
    }

    private void UpdateGlow()
    {
        float t = (float)currentStars / maxStars;
        float targetIntensity = Mathf.Lerp(minGlow, maxGlow, glowCurve.Evaluate(t));

        if (glowCoroutine != null)
            StopCoroutine(glowCoroutine);

        glowCoroutine = StartCoroutine(SmoothGlowChange(targetIntensity));
    }

    private IEnumerator SmoothGlowChange(float targetIntensity)
    {
        Color currentColor = glowMaterial.GetColor("_EmissionColor");
        float currentIntensity = currentColor.maxColorComponent;
        float elapsed = 0f;

        while (elapsed < glowTransitionTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / glowTransitionTime;
            float newIntensity = Mathf.Lerp(currentIntensity, targetIntensity, t);
            Color finalColor = glowColor * newIntensity;

            glowMaterial.SetColor("_EmissionColor", finalColor);
            DynamicGI.SetEmissive(jarRenderer, finalColor);

            yield return null;
        }

        // Ensure final value is set
        Color final = glowColor * targetIntensity;
        glowMaterial.SetColor("_EmissionColor", final);
        DynamicGI.SetEmissive(jarRenderer, final);
    }
}