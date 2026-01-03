using UnityEngine;

public class CraneController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minX = -5f;
    public float maxX = 5f;
    public bool canMove = true;


    void Update()
    {
        float move = 0f;
        if (!canMove) return;

        if (Input.GetKey(KeyCode.LeftArrow))
            move = -1f;
        else if (Input.GetKey(KeyCode.RightArrow))
            move = 1f;

        Vector3 pos = transform.position;
        pos.x += move * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform.position = pos;
    }
}

