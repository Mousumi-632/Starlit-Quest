using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI npcText;

    private void Start()
    {
        if (StarCounter.Instance != null)
        {
            UpdateText(StarCounter.Instance.StarsCollected);
            StarCounter.Instance.OnStarsChanged += UpdateText;
        }
    }

    private void OnDestroy()
    {
        if (StarCounter.Instance != null)
        {
            StarCounter.Instance.OnStarsChanged -= UpdateText;
        }
    }

    private void UpdateText(int stars)
    {
        switch (stars)
        {
            case 0:
                npcText.text = "Let's find the star!";
                break;
            case 1:
                npcText.text = "Now with cloud distraction!";
                break;
            case 2:
                npcText.text = "Now with comet distraction!";
                break;
            case 3:
                npcText.text = "Great job!";
                break;
            default:
                npcText.text = $"You collected {stars} stars!";
                break;
        }
    }
}
