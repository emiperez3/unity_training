using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    private SpriteRenderer renderer;
    private Animator animator;
    [SerializeField] private float speed, jumpForce, rayDistance;
    [SerializeField] private LayerMask groundMask;

    // Start se llama antes del primer Update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Obstacle"), true);

        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update se llama una vez por frame
    void Update()
    {
        float currentSpeed = 0;
        if (Input.GetKey(KeyCode.A))
        {
            renderer.flipX = true;
            currentSpeed = -speed;
        } else if (Input.GetKey(KeyCode.D))
        {
            renderer.flipX = false;
            currentSpeed = speed;
        }

        animator.SetFloat("movementSpeed", Mathf.Abs(currentSpeed));
        rigidbody.velocity = new Vector2(currentSpeed, rigidbody.velocity.y);


        if (IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
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
                animator.SetBool("jumping", true);
            } else
            {
                animator.SetBool("jumping", false);
            }
        } else
        {
            animator.SetBool("jumping", true);
        }
    }

    private bool IsGrounded()
    {
        // Si el rayo golpea el suelo (detectado por la "groundMask", hit.collider va a ser distinto de null y por lo tanto, estamos en tierra) 
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, rayDistance, groundMask);

        return hit.collider != null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            bool enemyHit = false;

            foreach(ContactPoint2D contactPoint in collision.contacts)
            {
                // Si golpeamos en la parte superior del collider, entonces hacemos daño al enemigo
                if (contactPoint.normal.y >= 0.45)
                {
                    enemyHit = true;
                    break;
                }
            }

            if (enemyHit)
            {
                // Aca vamos a dejar el comportamiento de recibir un golpe al mismo enemigo
                //Destroy(collision.gameObject);
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            } else
            {

                //Destroy(gameObject);
            }
        }
    }
}
