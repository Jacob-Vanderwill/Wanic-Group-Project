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
        
        if(PathDetection() && AtPointOfTravel())
        {
            
        }
        ThisRB.velocity = Speed * (PointOfTravel - new Vector2 (transform.position.x, transform.position.y)).normalized;

    }

    private void FishIdle()
    {
        float RandomAngle;
        float RandomDistance;
        RandomDistance = Random.Range(0.00f, WanderingRadius);
        RandomAngle = Random.Range(0.00f, 359.00f);
        PointOfTravel = new Vector2(RandomDistance * Mathf.Cos(RandomAngle), RandomDistance * Mathf.Sin(RandomAngle)) + home;
    }
    private void FishPlayerInteraction()
    {
        
    }
    private bool PathDetection()
    {
        Vector2 CheckBoxCenter;
        float VelocityVector;
        VelocityVector = Mathf.Sqrt(ThisRB.velocity.x * ThisRB.velocity.x + ThisRB.velocity.y * ThisRB.velocity.y);
        CheckBoxCenter = new Vector2((ThisRB.velocity.x + transform.position.x), (ThisRB.velocity.y + transform.position.y));
        return Physics2D.OverlapBox(CheckBoxCenter, new Vector2(VelocityVector * 2, ThisCollider.radius * 2), transform.rotation.eulerAngles.z);
    }
    private bool AtPointOfTravel()
    {
        Vector2 DisFromPoint;
        DisFromPoint = PointOfTravel - new Vector2(transform.position.x, transform.position.y);
        if (Mathf.Abs(DisFromPoint.x) < 0.5f && Mathf.Abs(DisFromPoint.x) < 0.5f)
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
    }
}
