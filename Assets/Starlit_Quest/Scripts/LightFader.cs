using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightFader : MonoBehaviour
{
    [Header("Target Intensity")]
    [SerializeField] private float startIntensity = 0.4f;
    [SerializeField] private float targetIntensity = 1.5f;

    [Header("Target Color Temperature")]
    [SerializeField] private float targetTemperature = 5000f;

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 3f;
    [SerializeField] private bool autoStart = true;

    private Light directionalLight;
    private float originalIntensity;
    private float originalTemperature;

    private void Awake()
    {
        directionalLight = GetComponent<Light>();
        if (directionalLight.type != LightType.Directional)
        {
            Debug.LogWarning("LightFader should be on a Directional Light.");
        }
    }

    private void Start()
    {
        if (!Application.isPlaying) return;

        originalIntensity = directionalLight.intensity;
        originalTemperature = directionalLight.colorTemperature;

        directionalLight.useColorTemperature = true;

        if (autoStart)
        {
            StartFading();
        }
    }

    public void StartFading()
    {
        StartCoroutine(FadeLight());
    }

    private IEnumerator FadeLight()
    {
        float elapsed = 0f;
        float startTemp = directionalLight.colorTemperature;
        float startInt = directionalLight.intensity;

        while (elapsed < transitionDuration)
        {
            float t = elapsed / transitionDuration;

            directionalLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            directionalLight.colorTemperature = Mathf.Lerp(startTemp, targetTemperature, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final values are set
        directionalLight.intensity = targetIntensity;
        directionalLight.colorTemperature = targetTemperature;
    }
}