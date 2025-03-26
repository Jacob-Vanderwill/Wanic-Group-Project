/*
 * Hudson Ream
 * 3/25/2025
 * Fish AI
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    Vector2 home;
    CircleCollider2D ThisCollider;
    Rigidbody2D ThisRB;
    Vector2 PointOfTravel;
    bool IsTraveling;
    public LayerMask layermask;
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

        if (PathDetection() || AtPointOfTravel())
        {
            ThisRB.velocity= Vector2.zero;
            IsTraveling = false;
        }
        else
        {
            ThisRB.velocity += Speed * Time.deltaTime * (PointOfTravel - new Vector2 (transform.position.x, transform.position.y)).normalized;
        }
        

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
        Vector2 CheckBoxCenter;
        Vector2 ColliderRadPoint;
        float VelocityVector;
        float VelocityAngle;

        VelocityAngle = Mathf.Atan2(ThisRB.velocity.y, ThisRB.velocity.x) * Mathf.Rad2Deg;
        ColliderRadPoint = new Vector2(ThisCollider.radius * Mathf.Cos(VelocityAngle), ThisCollider.radius * Mathf.Sin(VelocityAngle));
        VelocityVector = Mathf.Sqrt(ThisRB.velocity.x * ThisRB.velocity.x + ThisRB.velocity.y * ThisRB.velocity.y);
        CheckBoxCenter = new Vector2((ThisRB.velocity.x + transform.position.x + ColliderRadPoint.x * 1.05f), (ThisRB.velocity.y + transform.position.y + ColliderRadPoint.y * 1.05f));

        Debug.Log(ColliderRadPoint);

        return Physics2D.OverlapBox(CheckBoxCenter, new Vector2((VelocityVector - 1f) * 2 - ThisCollider.radius, ThisCollider.radius * 2), VelocityAngle);
    }
    private bool AtPointOfTravel()
    {
        Vector2 DisFromPoint;
        DisFromPoint = PointOfTravel - new Vector2(transform.position.x, transform.position.y);
        if (Mathf.Abs(DisFromPoint.x) < 0.1f && Mathf.Abs(DisFromPoint.x) < 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(PointOfTravel.x, PointOfTravel.y, 10), 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(home, WanderingRadius);
    }
}
