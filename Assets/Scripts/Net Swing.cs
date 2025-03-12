/*
 * Hudson
 * 3/11/2025
 * allows the player to attack with the net
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSwing : MonoBehaviour
{
    
    public GameObject Net;
    GameObject ThisNet;
    HingeJoint2D ThisJoint;
    Rigidbody2D ThisRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        ThisRigidbody= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            {
            ThisNet = Instantiate(Net, transform.position, transform.rotation, transform);
            ThisNet.GetComponent<Rigidbody2D>().angularVelocity = 100;
            ThisJoint = ThisNet.GetComponent<HingeJoint2D>();
            ThisNet.transform.position = ThisJoint.connectedAnchor;
            ThisJoint.anchor = new Vector2(-0.5f, 0);
            ThisJoint.connectedBody = ThisRigidbody;
            ThisJoint.useMotor= true;
            }

    }
}
