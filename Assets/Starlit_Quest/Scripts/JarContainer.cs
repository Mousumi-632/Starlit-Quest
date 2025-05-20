using UnityEngine;

public class JarContainer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Star"))
        {
            StarCounter.Instance.AddStar();
        }
    }
}