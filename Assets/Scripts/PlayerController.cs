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

    private float oxygenTankSize;

    private bool isSprinting;
    private bool isDead;

    private Rigidbody2D myRB;
    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();

        oxygenTankSize = PlayerPrefs.GetFloat("OxygenTankSize");
        health.health = 1;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        // debug

        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
            Debug.Log("Score: " + PlayerPrefs.GetInt("Coins"));
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
        // look at mouse
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation= Quaternion.Euler(0, 0, Mathf.Atan2(mousepos.y - transform.position.y, mousepos.x - transform.position.x) * Mathf.Rad2Deg);// update IsDead
        PlayerPrefs.SetInt("IsDead", isDead ? 1 : 0);
    }

    void playerPickUpFish(string tag, int value)
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + value);
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
        string tag = collision.collider.tag;

        // if collided with camera border then dead/if collided with fish add value
        switch (tag)
        {
            case "MainCamera":
                {
                    health.health = 0;
                    break;
                }
            case "Enemy":
                {
                    health.health = 0;
                    break;
                }
                /* use the following for collecting fish; change the *value* to the fish's value
            case "text":
                {
                    playerPickUpFish(tag, *value*);
                    break;
                }
                */
        }
    }
}
