using UnityEngine;

public class JarParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem jarParticleSystem;
    [SerializeField] private int maxStars = 5;
    [SerializeField] private int maxParticles = 100;

    private int starsInJar = 0;

    private void Start()
    {
        UpdateParticles();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Star"))
        {
            starsInJar = Mathf.Min(starsInJar + 1, maxStars);
            Debug.Log($"Star entered jar! Total stars: {starsInJar}");
            UpdateParticles();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Star"))
        {
            starsInJar = Mathf.Max(starsInJar - 1, 0);
            Debug.Log($"Star left jar! Total stars: {starsInJar}");
            UpdateParticles();
        }
    }

    private void UpdateParticles()
    {
        float t = (float)starsInJar / maxStars;

        var emission = jarParticleSystem.emission;
        var main = jarParticleSystem.main;

        emission.rateOverTime = Mathf.Lerp(0f, maxParticles, t);
        main.startColor = Color.Lerp(Color.clear, Color.yellow, t);

        if (t > 0 && !jarParticleSystem.isPlaying)
            jarParticleSystem.Play();
        else if (t == 0 && jarParticleSystem.isPlaying)
            jarParticleSystem.Stop();
    }
}