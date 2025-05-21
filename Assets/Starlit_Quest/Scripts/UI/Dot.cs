using UnityEngine;

public class Dot : MonoBehaviour
{
    public enum DotStatus
    {
        Off = 0,
        On = 1,
    }

    public DotStatus Status
    {
        get => status;
        set
        {
            status = value;
            UpdateDisplay();
        }
    }
    private DotStatus status;

    [SerializeField] private GameObject dotOn;
    [SerializeField] private GameObject dotOff;

    private void Awake()
    {
        Status = DotStatus.Off;
    }

    private void UpdateDisplay()
    {
        if (status == DotStatus.Off)
        {
            dotOn.SetActive(false);
            dotOff.SetActive(true);
        }
        else if (status == DotStatus.On)
        {
            dotOn.SetActive(true);
            dotOff.SetActive(false);
        }
    }

}
