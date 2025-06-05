using UnityEngine;

public class JarGlowController : MonoBehaviour
{
    [SerializeField] private Renderer jarRenderer;
    [SerializeField] private int maxStars = 5;
    [SerializeField] private float maxGlowIntensity = 5f;

    private int currentStars = 0;
    private Material glowMaterial;

    private void Start()
    {
        if (jarRenderer == null)
            jarRenderer = GetComponent<Renderer>();

        glowMaterial = jarRenderer.materials[1];
        UpdateGlow();
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
        Color baseColor = Color.yellow;
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(t * maxGlowIntensity);
        glowMaterial.SetColor("_EmissionColor", finalColor);
    }
}
