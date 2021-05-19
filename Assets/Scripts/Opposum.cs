using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opposum : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [SerializeField] private float speed, rayDetectionDistance;
    [SerializeField] private LayerMask obstacleMask;
    private float currentSpeed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
    }

    void Update()
    {
        if (CheckObstacles())
        {
            currentSpeed = -currentSpeed;
            transform.Rotate(Vector3.up, 180);
        }

        rigidbody.velocity = new Vector2(currentSpeed, rigidbody.velocity.y);
    }

    private bool CheckObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, rayDetectionDistance, obstacleMask);

        return hit.collider != null;
    }
}
