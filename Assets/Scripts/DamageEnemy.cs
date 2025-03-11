/*
 * Jacob Vanderwill
 * 10/31/24
 * last edited: 10/31/24
 * Create a script to handle damage
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageEnemy : MonoBehaviour
{
    public uint damage = 1;
    public UnityEvent onDamage = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health h = collision.GetComponent<Health>();
        if (!collision.CompareTag("Enemy"))
        {
            if (h != null)
            {
                onDamage.Invoke();
                h.Damage(damage);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health h = collision.gameObject.GetComponent<Health>();
        if (!collision.collider.CompareTag("Enemy"))
        {
            if (h != null)
            {
                onDamage.Invoke();
                h.Damage(damage);
            }
        }
    }
}
