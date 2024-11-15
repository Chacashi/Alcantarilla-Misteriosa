using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform groundController;
    [SerializeField] private bool movementRigth;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask floor;
    private Rigidbody2D _compRigidbody2D;
    private RaycastHit2D isGround;

    private void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.Raycast(groundController.position, Vector2.down, rayDistance, floor);

        _compRigidbody2D.velocity = new Vector2(speed, _compRigidbody2D.velocity.y);

        if (isGround == false)
        {
            spin();
        }
    }

    void spin()
    {
        movementRigth = !movementRigth;

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        speed *= -1;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundController.transform.position, groundController.transform.position + Vector3.down * rayDistance);
    }
}
