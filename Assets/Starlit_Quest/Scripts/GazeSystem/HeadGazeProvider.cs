using UnityEngine;

public class HeadGazeProvider : MonoBehaviour, IGazeProvider
{
    public Vector3 Origin => Camera.main.transform.position;
    public Vector3 Direction => Camera.main.transform.forward;
}
