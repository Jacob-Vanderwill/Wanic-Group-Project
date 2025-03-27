/*
 * Hudson Ream
 * 3/25/2025
 * Fish AI
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class FishAI : MonoBehaviour
{
    Vector2 home;
    CircleCollider2D ThisCollider;
    Rigidbody2D ThisRB;
    Vector2 PointOfTravel;
    bool IsTraveling;
    public float WanderingRadius;
    public float Speed;


    // Start is called before the first frame update
    void Start()
    {
        ThisCollider = GetComponent<CircleCollider2D>();
        home = transform.position;
        ThisRB = GetComponent<Rigidbody2D>();
        IsTraveling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsTraveling)
        {
            FishIdle();
        }
        
        if ((PathDetection() || AtPointOfTravel()) && ThisRB.velocity != Vector2.zero)
        {
            IsTraveling = false;
        }
        else
        {
            ThisRB.velocity += Speed * Time.deltaTime * (PointOfTravel - new Vector2 (transform.position.x, transform.position.y)).normalized;
        }
        
        ThisCollider.enabled = true;
    }

    private void FishIdle()
    {
        float RandomAngle;
        float RandomDistance;
        RandomDistance = Random.Range(0.00f, WanderingRadius);
        RandomAngle = Random.Range(0.00f, 359.00f);
        PointOfTravel = new Vector2(RandomDistance * Mathf.Cos(RandomAngle), RandomDistance * Mathf.Sin(RandomAngle)) + home;
        IsTraveling = true;
    }
    private void FishPlayerInteraction()
    {
        
    }
    private bool PathDetection()
    {
        /*if (ThisRB.velocity.magnitude <= 0.1)
        {
            return false;
        }*/
        float VelocityAngle;
        VelocityAngle = Mathf.Atan2(ThisRB.velocity.y, ThisRB.velocity.x) * Mathf.Rad2Deg;
        ThisCollider.enabled = false;
        return Physics2D.CircleCast(ThisRB.position, ThisCollider.radius, ThisRB.velocity.normalized, ThisRB.velocity.magnitude);
    }
    private bool AtPointOfTravel()
    {
        Vector2 DisFromPoint;
        DisFromPoint = PointOfTravel - new Vector2(transform.position.x, transform.position.y);
        return (Mathf.Abs(DisFromPoint.x) < 0.1f && Mathf.Abs(DisFromPoint.x) < 0.1f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(PointOfTravel.x, PointOfTravel.y, 10), 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(home, WanderingRadius);
        Gizmos.color = Color.cyan;
        ThisCollider = GetComponent<CircleCollider2D>();
        ThisRB = GetComponent<Rigidbody2D>();
        Gizmos.DrawWireSphere(ThisRB.position+(ThisRB.velocity.normalized*ThisRB.velocity.magnitude),ThisCollider.radius);
    }
}
