using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    Vector2 home;
    CircleCollider2D ThisCollider;
    Rigidbody2D ThisRB;
    Vector2 PointOfTravel;
    public float WanderingRadius;

    // Start is called before the first frame update
    void Start()
    {
        ThisCollider = GetComponent<CircleCollider2D>();
        home = transform.position;
        ThisRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FishIdle();
    }

    private void FishIdle()
    {
        float RandomAngle;
        float RandomDistance;
        RandomDistance = Random.Range (0.00f, WanderingRadius);
        RandomAngle = Random.Range(0.00f, 359.00f);
        PointOfTravel = new Vector2(RandomDistance * Mathf.Cos(RandomAngle), RandomDistance * Mathf.Sin(RandomAngle)) + home;
        
        Debug.Log(PointOfTravel);
    }
    private void FishPlayerInteraction()
    {
        
    }
    private bool PathDetection(Vector2 Point)
    {
        //Mathf.Sqrt((Point.x - transform.position.x) * (Point.y - transform.position.y)) / 2;
        //new Vector2(*Mathf.Cos(), *Mathf.Sin());
        //return Physics2D.OverlapBox(transform.position, new Vector2(distance, ThisCollider.radius * 2), transform.rotation.eulerAngles.z);
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(PointOfTravel.x, PointOfTravel.y, 10), 0.1f);
    }
}
