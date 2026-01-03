using UnityEngine;

public class CraneGrabber : MonoBehaviour
{
    public float grabRadius = 0.4f;
    public LayerMask ballLayer;

    public Ball TryGrab()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            grabRadius,
            ballLayer
        );

        if (hit == null) return null;
        return hit.GetComponent<Ball>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRadius);
    }
}

