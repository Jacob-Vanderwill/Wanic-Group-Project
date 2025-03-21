/*
 * Hudson
 * 3/19/2025
 * Kills this gameobject when it touches a specific tag
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnTouch : MonoBehaviour
{
    public string TagToDieTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagToDieTo))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(TagToDieTo))
        {
            Destroy(gameObject);
        }
    }
}
