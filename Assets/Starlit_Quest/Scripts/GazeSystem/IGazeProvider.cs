using UnityEngine;

public interface IGazeProvider
{
 Vector3 Origin {  get; }
 Vector3 Direction { get; }
}
