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
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");
        isSprinting = Input.GetKeyDown(KeyCode.LeftShift);
    }
    private void FixedUpdate()
    {
        if (isSprinting)
        {
            myRB.AddForce(inputMovement.normalized * SpeedSprint);
            Debug.Log("Sprinting");
        }
        else
        {
            myRB.AddForce(inputMovement.normalized * Speed);
        }
    }
}
