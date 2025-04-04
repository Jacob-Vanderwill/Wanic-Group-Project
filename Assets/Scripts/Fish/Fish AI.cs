/*
 * Hudson Ream
 * 3/25/2025
 * Fish AI
 */

using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;
using Debug = System.Diagnostics.Debug;

public class FishAI : MonoBehaviour
{
    bool B;
    Vector2 home;
    CircleCollider2D ThisCollider;
    Rigidbody2D ThisRB;
    SpriteRenderer ThisSprite;
    Vector2 PointOfTravel;
    private Health health;
    bool isIdle;
    bool IsTraveling;
    bool IsChasing;
    bool IsGoingHome;
    GameObject Player;
    Stack<Vector3> BackToHome = new Stack<Vector3>();

    public float WanderingRadius;
    public float PlayerDetectionRadius;
    public float IdleSpeed;
    public float ChaseSpeed;
    public float LookRotationOffest;


    // Start is called before the first frame update
    void Start()
    {
        ThisCollider = GetComponent<CircleCollider2D>();
        home = transform.position;
        ThisRB = GetComponent<Rigidbody2D>();
        ThisSprite = GetComponent<SpriteRenderer>();
        IsTraveling = false;
        IsChasing = false;
        health = GetComponent<Health>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        IsChasing = false;
        IsGoingHome = false;
        isIdle = false;
        if (health.health <= 0)
        {
            return;
        }

        //if player is in detection radius and path to player is clear
        if(new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y).magnitude <= PlayerDetectionRadius && Vector2.zero == PathDetection(Player.transform.position - transform.position, new Collider2D[2] {ThisCollider, Player.GetComponent<Collider2D>()}))
        {
            FishChase();
        }
        else
        {
            if (!IsTraveling && BackToHome.Count == 0)    
            {
               FishIdle();
            }
            else if(BackToHome.Count != 0)
            {
               FishGoHome();
            }       
        }

        if (Vector2.zero != PathDetection(ThisRB.velocity, new Collider2D[2] {ThisCollider, Player.GetComponent<Collider2D>()}) || AtPointOfTravel())
        {
            IsTraveling = false;
            if (IsChasing)
            {
                ThisRB.velocity += PathDetection(ThisRB.velocity, new Collider2D[2] { ThisCollider, Player.GetComponent<Collider2D>() }).normalized * ChaseSpeed * Time.deltaTime;
            }
            else
            {
                ThisRB.velocity += PathDetection(ThisRB.velocity, new Collider2D[2] { ThisCollider, Player.GetComponent<Collider2D>() }).normalized * IdleSpeed * Time.deltaTime;
            }
        }
        else
        {
            if(IsChasing)
            {
                ThisRB.velocity += ChaseSpeed * Time.deltaTime * (PointOfTravel - new Vector2(transform.position.x, transform.position.y)).normalized;
            }
            else
            {
                ThisRB.velocity += IdleSpeed * Time.deltaTime * (PointOfTravel - new Vector2 (transform.position.x, transform.position.y)).normalized;
            }
        }

        // fish rotation
        if (ThisRB.velocity.x != 0 || ThisRB.velocity.y != 0)
        {
            ThisSprite.flipY = (ThisRB.velocity.y < 0);
            if(ThisSprite.flipY)
            {
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(ThisRB.velocity.y, ThisRB.velocity.x) * Mathf.Rad2Deg + LookRotationOffest);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(ThisRB.velocity.y, ThisRB.velocity.x) * Mathf.Rad2Deg - LookRotationOffest);
            }
        }
    }

    private void FishIdle()
    {
        float RandomDistance = Random.Range(0.00f, WanderingRadius);
        float RandomAngle = Random.Range(0.00f, 359.00f);
        PointOfTravel = new Vector2(RandomDistance * Mathf.Cos(RandomAngle), RandomDistance * Mathf.Sin(RandomAngle)) + home;
        isIdle = true;
        IsTraveling = true;
    }
    private void FishChase()
    {
        IsChasing = true;
        IsTraveling = true;
        PointOfTravel = Player.transform.position;
        if (BackToHome.Count == 0)
        {
            BackToHome.Push(transform.position);
        }
        else if ((BackToHome.Peek() - transform.position).magnitude >= 1f)
        {
            BackToHome.Push(transform.position);
        }
    }

    private void FishGoHome()
    {
        IsGoingHome = true;
        IsTraveling = true;
        PointOfTravel = BackToHome.Peek();
        if (AtPointOfTravel())
        {
            BackToHome.Pop();
        }
    }
    private Vector2 PathDetection(Vector2 Path, Collider2D[] Cignore)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(ThisRB.position, ThisCollider.radius, Path.normalized, Path.magnitude * 2);
        foreach (RaycastHit2D hit in hits)
        {
            B = true;
            foreach(Collider2D C in Cignore)
            {
                if (hit.collider == C)
                {
                    if(IsChasing && hit.collider.CompareTag("Fish"))
                    {
                        B = false;
                    }
                 B = false;
                }
            }
            if(B == true)
            {
                return hit.centroid - hit.point;
            }
        }
        return Vector2.zero;
    }
    private bool AtPointOfTravel()
    {
        Vector2 DisFromPoint;
        DisFromPoint = PointOfTravel - new Vector2(transform.position.x, transform.position.y);
        return (Mathf.Abs(DisFromPoint.x) < 0.1f && Mathf.Abs(DisFromPoint.y) < 0.1f);
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
        Gizmos.DrawWireSphere(ThisRB.position+(ThisRB.velocity.normalized*ThisRB.velocity.magnitude * 2),ThisCollider.radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectionRadius);

        Gizmos.color = Color.red;
        foreach (Vector3 v in BackToHome)
        {
            Gizmos.DrawSphere(v, 0.1f);
        }
        
    }
}
