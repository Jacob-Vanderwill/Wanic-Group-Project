/*
 * Jacob Vanderwill & Hudson Ream
 * 10/31/24
 * last edited: 3/19/2025
 * Create a script to handle damage
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DamageEnemy : MonoBehaviour
{
    public uint damage = 1;
    public UnityEvent onDamage = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health h = collision.gameObject.GetComponent<Health>();
        string FishName = collision.name;
        if (collision.CompareTag("Enemy") || collision.CompareTag("Fish"))
        {
            if (h != null)
            {
                onDamage.Invoke();
                h.Damage(damage, gameObject.tag);
            }
            else
            {
                if (collision.CompareTag("Fish"))
                {
                    PlayerPrefs.SetInt(FishName, PlayerPrefs.GetInt(FishName) + 1);
                    PlayerPrefs.SetInt(FishName + "Caught", 1);

                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health h = collision.gameObject.GetComponent<Health>();
        string FishName = collision.collider.name;
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Fish"))
        {
            if (h != null)
            {
                onDamage.Invoke();
                h.Damage(damage, gameObject.tag);
            }
            else
            {
                if (collision.collider.CompareTag("Fish"))
                {
                     PlayerPrefs.SetInt(FishName, PlayerPrefs.GetInt(FishName) + 1);
                     PlayerPrefs.SetInt(FishName + "Caught", 1);
                }
            }
        }
        
    }
}
