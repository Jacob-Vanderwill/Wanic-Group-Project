/*
 * Jacob Vanderwill
 * Created: 3/10/2025
 * Last Altered: 3/11/2025
 * Create a script to manage all player inputs
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float Speed;
    public float SpeedSprint;

    private Vector3 inputMovement = Vector3.zero;
    private bool isSprinting;

    private Rigidbody2D myRB;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // get movement
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");
        isSprinting = Input.GetKey(KeyCode.LeftShift);
    }
    private void FixedUpdate()
    {
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
}
