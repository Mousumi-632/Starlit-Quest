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
        npcText.text = "Alright, friend — just like we talked about! The stars are waiting, and your mission starts *now*. Take a moment… look around. Nature’s beautiful tonight";
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
                npcText.text = "Look at your wrist clock,it shows the star we need. When you’re ready, gaze at the matching star";
                break;
            case 1:
                npcText.text = "Keep your eyes on it — the loading bar will fill as you focus. If you lose focus — the timer resets. Fill the glass jar with stars to complete the challenge.";
                break;
            case 2:
                npcText.text = " But watch out! Clouds and comets will try to steal your attention";
                break;
            case 3:
                npcText.text = "Let’s see how sharp your eyes really are! You’ve got this!";
                break;
            default:
                npcText.text = $"You collected {stars} stars!";
                break;
        }
    }
}
