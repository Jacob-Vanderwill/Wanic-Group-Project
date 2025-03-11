/*
 * Jacob Vanderwill
 * 10/31/24
 * last edited: 10/31/24
 * Create a script to handle [enemy] health
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Tooltip("Current Health at the Start of a Level")]
    public uint health = 3;
    [Tooltip("The max health the object can have after healing")]
    public uint maxHealth = 3;
    public bool destroyAtZero = true;

    // Unity events
    public UnityEvent onHeal = new UnityEvent();
    public UnityEvent onDamage = new UnityEvent();
    public UnityEvent onDeath = new UnityEvent();

    public void Heal(uint amount)
    {
        onHeal.Invoke();
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void Damage(uint amount)
    {
        onDamage.Invoke();
        if (health <= amount)
        {
            health = 0;
            onDeath.Invoke();
            if (destroyAtZero)
            {
                transform.position = new Vector3(transform.position.x, 50, transform.position.z);
            }
        }
        else
        {
            health -= amount;
        }
    }
}
