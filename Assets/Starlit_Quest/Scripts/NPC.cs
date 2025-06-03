using UnityEngine;
using TMPro;
using System.Collections;

public class NPC : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI npcText;
    [SerializeField] private float introDuration = 3f; // Duration of intro in seconds

    private bool hasShownIntro = false;

    private void Start()
    {
        StartCoroutine(ShowIntroAndSubscribe());
    }

    private IEnumerator ShowIntroAndSubscribe()
    {
        // Show intro dialog
        npcText.text = "...";
        hasShownIntro = true;

        // Wait before showing star-related text
        yield return new WaitForSeconds(introDuration);

        if (StarCounter.Instance != null)
        {
            UpdateText(StarCounter.Instance.StarsCollected);
            StarCounter.Instance.OnStarsChanged += UpdateText;
        }
    }

    private void OnDestroy()
    {
        if (hasShownIntro && StarCounter.Instance != null)
        {
            StarCounter.Instance.OnStarsChanged -= UpdateText;
        }
    }

    private void UpdateText(int stars)
    {
       

        switch (stars)
        {
            case 0:
                npcText.text = "...";
                break;
            case 1:
                npcText.text = "...";
                break;
            case 2:
                npcText.text = "...";
                break;
            case 3:
                npcText.text = "...";
                break;
            default:
                npcText.text = $"You collected {stars} stars!";
                break;
        }
    }
}
