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
    [SerializeField]
    public AudioClip CollectSound;
    public AudioSource source;
    public string TagToDieTo;
    SpriteRenderer thisSR;
    private void Start()
    {
        source.PlayOneShot(CollectSound, 1);
        thisSR = GetComponent<SpriteRenderer>();    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagToDieTo))
        {
            thisSR.sprite = null;
            Destroy(gameObject, 5);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(TagToDieTo))
        {
            thisSR.sprite = null;
            Destroy(gameObject, 5);
        }
    }
}
