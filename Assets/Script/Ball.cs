using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallType type;

    private bool activated = false;

    [HideInInspector] public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PickUp(Transform grabPoint)
    {
        rb.simulated = false;
        transform.position = grabPoint.position;
        transform.SetParent(grabPoint);
    }

    public void Drop()
    {
        transform.SetParent(null);
        rb.simulated = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.CompareTag("Hole"))
        {
            activated = true;
            GameManager.Instance.ResolveBall(this);
            Destroy(gameObject);
        }
    }
}
