using UnityEngine;

public class JarGlowController : MonoBehaviour
{
    [SerializeField] private Renderer jarRenderer;
    [SerializeField] private int maxStars = 5;
    [SerializeField] private Color glowColor = Color.yellow;
    [SerializeField] private float minGlow = 0.2f;
    [SerializeField] private float maxGlow = 5f;

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
        float intensity = Mathf.Lerp(minGlow, maxGlow, t);
        Color finalColor = glowColor * intensity;

        glowMaterial.SetColor("_EmissionColor", finalColor);
        DynamicGI.SetEmissive(jarRenderer, finalColor);
    }
}
