using UnityEngine;

public class ExampleStarResponder : MonoBehaviour, IGazeResponder
{
    [SerializeField] private Transform moveTarget;
    [SerializeField] private float moveSpeed = 1f;

    public void OnGazeEnter()
    {
        Debug.Log("Star hovered: " + name);
        
    }

    public void OnGazeExit()
    {
        Debug.Log("Star gaze exited: " + name);
        
    }

    public void OnGazeSelect()
    {
        Debug.Log("Star selected: " + name);
        if (moveTarget != null)
            StartCoroutine(MoveToTarget());
    }

    private System.Collections.IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, moveTarget.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // snap into jar, play sound, etc.
    }
}
