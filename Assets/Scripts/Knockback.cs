using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockback = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Fish"))
        {
            // knockback
            Rigidbody2D colliderRB = collision.collider.GetComponent<Rigidbody2D>();
            if (colliderRB != null)
            {
                if (colliderRB.bodyType == RigidbodyType2D.Dynamic)
                {
                    Vector3 knockbackForce = -(transform.position - collision.transform.position).normalized * knockback;
                    colliderRB.velocity = knockbackForce;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish"))
        {
            // knockback
            Rigidbody2D colliderRB = collision.GetComponent<Rigidbody2D>();
            if (colliderRB != null)
            {
                if (colliderRB.bodyType == RigidbodyType2D.Dynamic)
                {
                    Vector3 knockbackForce = -(transform.position - collision.transform.position).normalized * knockback;
                    colliderRB.velocity = knockbackForce;
                }
            }
        }
    }
}
