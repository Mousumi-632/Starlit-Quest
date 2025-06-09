using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaterialAlphaFader : MonoBehaviour
{
    [Header("URP Unlit Materials to Fade")]
    [SerializeField] private List<Material> materialsToFade = new List<Material>();

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 2f;

    [Tooltip("Automatically start fading on play")]
    [SerializeField] private bool autoStart = true;

    private List<Color> originalColors = new List<Color>();

    private void Start()
    {
        if (!Application.isPlaying)
            return;

        CacheOriginalColors();

        if (autoStart)
            StartFading();
    }

    public void StartFading()
    {
        if (!Application.isPlaying)
            return;

        for (int i = 0; i < materialsToFade.Count; i++)
        {
            Material mat = materialsToFade[i];
            if (mat != null && mat.HasProperty("_BaseColor"))
            {
                StartCoroutine(FadeAlpha(mat));
            }
        }
    }

    private IEnumerator FadeAlpha(Material mat)
    {
        Color startColor = mat.GetColor("_BaseColor");
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            Color newColor = startColor;
            newColor.a = Mathf.Lerp(1f, 0f, t);
            mat.SetColor("_BaseColor", newColor);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Color finalColor = startColor;
        finalColor.a = 0f;
        mat.SetColor("_BaseColor", finalColor);
    }

    private void CacheOriginalColors()
    {
        originalColors.Clear();
        foreach (var mat in materialsToFade)
        {
            if (mat != null && mat.HasProperty("_BaseColor"))
            {
                originalColors.Add(mat.GetColor("_BaseColor"));
            }
            else
            {
                originalColors.Add(Color.white); // fallback
            }
        }
    }

#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        RestoreOriginalAlpha();
    }

    private void OnDisable()
    {
        if (!Application.isPlaying)
            RestoreOriginalAlpha();
    }

    private void RestoreOriginalAlpha()
    {
        for (int i = 0; i < materialsToFade.Count; i++)
        {
            var mat = materialsToFade[i];
            if (mat != null && i < originalColors.Count)
            {
                Color restored = originalColors[i];
                restored.a = 1f;
                mat.SetColor("_BaseColor", restored);
            }
        }
    }
#endif
}
