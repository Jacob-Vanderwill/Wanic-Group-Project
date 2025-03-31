/*
 * Hudson Ream
 * 3/25/2025
 * Fish AI
 */
using UnityEngine;
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
    bool IsTraveling;
    GameObject Player;
    Vector2[] BackToHome;
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
        health = GetComponent<Health>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
         if(new Vector2(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y).magnitude <= PlayerDetectionRadius)
         {
            print("blah");
             if(PathDetection(Player.transform.position - transform.position, new Collider2D[2] {ThisCollider, Player.GetComponent<Collider2D>()}))
            {
                print("blah");
                PointOfTravel = Player.transform.position;
             }
         }
        if (health.health <= 0)
        {
            return;
        }
        if (ThisRB.velocity.x != 0 || ThisRB.velocity.y != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(ThisRB.velocity.y, ThisRB.velocity.x) * Mathf.Rad2Deg + LookRotationOffest);
            ThisSprite.flipY = (ThisRB.velocity.y < 0);
        }
        if (!IsTraveling && ThisRB.velocity.magnitude < 0.3f)
        {
            FishIdle();
        }

        if (PathDetection(ThisRB.velocity, new Collider2D[1] {ThisCollider}) || AtPointOfTravel())
        {
            IsTraveling = false;
        }
        else
        {
            ThisRB.velocity += IdleSpeed * Time.deltaTime * (PointOfTravel - new Vector2 (transform.position.x, transform.position.y)).normalized;
        }
    }

    private void FishIdle()
    {
        float RandomDistance = Random.Range(0.00f, WanderingRadius);
        float RandomAngle = Random.Range(0.00f, 359.00f);
        PointOfTravel = new Vector2(RandomDistance * Mathf.Cos(RandomAngle), RandomDistance * Mathf.Sin(RandomAngle)) + home;
        IsTraveling = true;
    }
    private void FishChase()
    {
        PointOfTravel = Player.transform.position;
    }
    private void FishPlayerInteraction()
    {
        
    }
    private bool PathDetection(Vector2 Path, Collider2D[] Cignore)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(ThisRB.position, ThisCollider.radius, Path.normalized, Path.magnitude * 2);
        foreach (RaycastHit2D hit in hits)
        {
            B = true;
            foreach(Collider2D C in Cignore)
            {
                if (hit.collider == C)
                {
                 B = false;
                }
            }
            if(B == true)
            {
                return true;
            }
        }
        return false;
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
        Gizmos.DrawWireSphere(ThisRB.position+(ThisRB.velocity.normalized*ThisRB.velocity.magnitude * 2),ThisCollider.radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectionRadius);
    }
}
