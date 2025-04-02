using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class FishHurt : MonoBehaviour
{
    private Health health;
    private SpriteRenderer spriteRenderer;

    //screen shake values 
    float ShakeMagnitude = 0;
    float ShakeDuration = 0;
    float ShakeStartDuration = 0;

    private uint lastHealth;

    public Color HurtColor;
    private Color startColor;

    IEnumerator fishHurt(Color hurtColor)
    {
        spriteRenderer.color = hurtColor;

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = startColor;
    }
    private void Start()
    {
        health = GetComponent<Health>();

        lastHealth = health.health;
    }

    // Update is called once per frame
    void Update()
    {
        //screenshake stuff 
        if (lastHealth != health.health)
        {
            fishHurt(HurtColor);
        }
    }
    private void FixedUpdate()
    {
        //shake duration 
        if (ShakeDuration > 0)
        {
            Vector3 randShake = Random.insideUnitCircle * (ShakeMagnitude * (ShakeDuration / ShakeStartDuration));
            transform.position += randShake;
            ShakeDuration -= Time.fixedDeltaTime;

        }
        else
        {
            ShakeMagnitude = 0;
        }
    }
}
