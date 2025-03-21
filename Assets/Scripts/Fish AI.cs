using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    Vector2 home;
    Rigidbody2D ThisRB;
    // Start is called before the first frame update
    void Start()
    {
        home = transform.position;
        ThisRB = gameObject.GetComponent<Rigidbody2D>();
        ThisRB.AddForce(new Vector2 (40, 0));
    }

    // Update is called once per frame
    void Update()
    {
        FishIdle();
    }

    private void FishIdle()
    {
        ThisRB.AddForce(new Vector2(home.x - transform.position.x, home.y - transform.position.y));
    }
}
