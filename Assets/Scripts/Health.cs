/*
 * Jacob Vanderwill
 * 10/31/24
 * last edited: 10/31/24
 * Create a script to handle health
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    public uint health = 1;

    public bool destroyAtZero = true;

    public bool isShieldActive;

    // Unity events
    public UnityEvent onDamage = new UnityEvent();
    public UnityEvent onDeath = new UnityEvent();
    public UnityEvent onPlayerKillDeath = new UnityEvent();

    public void Damage(uint amount, string Tag)
    {
        if (!isShieldActive)
        {
            onDamage.Invoke();
            if (health <= amount)
            {
                health = 0;
                onDeath.Invoke();
                if(Tag == "Net")
                {
                    onPlayerKillDeath.Invoke();
                }
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
}
