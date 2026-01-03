using UnityEngine;
using System.Collections;

public class CraneGrab : MonoBehaviour
{
    [Header("Grabbers")]
    public CraneGrabber leftGrabber;
    public CraneGrabber rightGrabber;

    [Header("Points")]
    public Transform grabSensor;      // child crane
    public Transform grabTargetPoint; // world target

    public Transform holePoint;
    public Transform defaultPoint;

    [Header("Animator")]
    public Animator animator;

    private bool isBusy = false;
    public float moveSpeed = 60f;

    void Update()
    {
        if (isBusy) return;

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(GrabRoutine());
        }
    }

    IEnumerator GrabRoutine()
    {
        CraneController craneController = Object.FindFirstObjectByType<CraneController>();
        craneController.canMove = false;

        // 1. Turun + buka
        animator.SetTrigger("Open");
        yield return MoveToY(grabTargetPoint.position.y);
        yield return new WaitForSeconds(0.25f);


        // 2. Coba ambil bola (prioritas kiri → kanan)
        Ball ball = leftGrabber.TryGrab();
        if (ball == null)
            ball = rightGrabber.TryGrab();

        if (ball != null)
        {
            animator.SetTrigger("Close");

            ball.PickUp(grabSensor);
            yield return new WaitForSeconds(0.25f);

            // 3. Geser ke lubang
            yield return MoveToY(defaultPoint.position.y);
            yield return MoveToX(holePoint.position.x);
            yield return MoveToPosition(new Vector3(holePoint.position.x, transform.position.y, transform.position.z)
);


            // 4. Jatuhkan bola
            ball.Drop();
            animator.SetTrigger("Reset");
            yield return new WaitForSeconds(0.25f);
            animator.SetTrigger("Drop");
            yield return new WaitForSeconds(5f);
            animator.SetTrigger("Release");
        }

        // 5. Kembali ke posisi awal
        
        yield return MoveTo(defaultPoint.position);

        yield return new WaitForSeconds(0.5f);

        craneController.canMove = true;

        GameManager.Instance.EndPlayerTurn();
    }

     IEnumerator MoveTo(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }
    }

    IEnumerator MoveToX(float x)
    {
        while (Mathf.Abs(transform.position.x - x) > 0.01f)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.MoveTowards(pos.x, x, moveSpeed * Time.deltaTime);
            transform.position = pos;
            yield return null;
        }
    }

    IEnumerator MoveToY(float y)
    {
        while (Mathf.Abs(transform.position.y - y) > 0.01f)
        {
            Vector3 pos = transform.position;
            pos.y = Mathf.MoveTowards(pos.y, y, moveSpeed * Time.deltaTime);
            transform.position = pos;
            yield return null;
        }
    }

    IEnumerator MoveToPosition(Vector3 target)
{
    while (Vector3.Distance(transform.position, target) > 0.01f)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );
        yield return null;
    }

    transform.position = target; // snap final
}

}
