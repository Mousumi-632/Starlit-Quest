using UnityEngine;

public interface IGazeResponder
{
    void OnGazeEnter();
    void OnGazeExit();
    void OnGazeSelect();
}
