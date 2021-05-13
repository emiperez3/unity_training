using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    [SerializeField] private float speed, jumpForce, rayDistance;
    [SerializeField] private LayerMask groundMask;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        float currentSpeed = 0;
        if (Input.GetKey(KeyCode.A))
        {
            currentSpeed = -speed;
        } else if (Input.GetKey(KeyCode.D))
        {
            currentSpeed = speed;
        }

        rigidbody.velocity = new Vector2(currentSpeed, rigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            /**
             * AddForce va a aplicar una fuerza, el movimiento va a depender de la magnitud y dirección
             * de la misma, pero también de la masa del personaje (en el Rigidbody2D).
             * 
             * velocity es la velocidad ya aplicada, la masa del personaje no importa, lo maneja por 
             * detrás el motor de físicas.
             */

            //rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
    }

    private bool IsGrounded()
    {
        // Si el rayo golpea el suelo (detectado por la "groundMask", hit.collider va a ser distinto de null y por lo tanto, estamos en tierra) 
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, rayDistance, groundMask);

        return hit.collider != null;
    }
}
