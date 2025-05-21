using UnityEngine;

public class JarContainer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Star"))
        {
            StarCounter.Instance.AddStar();

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }

            Collider starCollider = other.GetComponent<Collider>();
            if (starCollider != null)
            {
                starCollider.enabled = false;
            }
        }
    }
}
