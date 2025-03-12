/*
 * Jacob Vanderwill
 * Created: 3/10/2025
 * Last Altered: 3/11/2025
 * Create a script to manage all player inputs
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float Speed;
    public float SpeedSprint;

    private Vector3 inputMovement = Vector3.zero;

    private bool isSprinting;
    private bool isDead;

    private Rigidbody2D myRB;
    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();

        health.health = 1;

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        // debug

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Score: " + PlayerPrefs.GetInt("Coins"));
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
        }

        //

        // check if dead
        if (isDead)
            { return; }
        if (health.health == 0f)
        {
            isDead = true;
        }
        // get movement
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");
        isSprinting = Input.GetKey(KeyCode.LeftShift);
    }
    private void FixedUpdate()
    {
        if (isDead)
            { return; }
        // add forces
        if (isSprinting)
        {
            myRB.AddForce(inputMovement.normalized * SpeedSprint);
        }
        else
        {
            myRB.AddForce(inputMovement.normalized * Speed);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if collided with camera border then dead
        if (collision.collider.CompareTag("MainCamera"))
        {
            health.health = 0;
        }
    }
}
