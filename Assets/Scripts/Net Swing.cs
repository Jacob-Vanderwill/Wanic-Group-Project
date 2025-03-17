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
    public float cooldown;
    public float AttackDegree;
    public float AttackSpeed;
    float degreesmoved;
    float currentrotation;
    float cooldownCount = 0;
    bool IsAttacking;
    GameObject ThisNet;
    Rigidbody2D ThisRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        ThisRigidbody= GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldownCount <= 0)
        {
            cooldownCount = cooldown;
            degreesmoved = AttackDegree;
            ThisNet = Instantiate(Net, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + (AttackDegree * 0.5f)));
            currentrotation = transform.eulerAngles.z - (AttackDegree * 0.5f);
            IsAttacking= true;
            
        }
        if(cooldownCount > 0)
        {
            cooldownCount -= Time.deltaTime;
        }
        Debug.Log(cooldownCount);

        if (IsAttacking == true)
        {
            ThisNet.transform.position = transform.position;
        }
        if (ThisNet.transform.eulerAngles.z > currentrotation)
        {

        }
       
    }
    void FixedUpdate()
    {
        if (IsAttacking && degreesmoved > 0)
        {
            degreesmoved -= AttackDegree / AttackSpeed;
            ThisNet.transform.rotation = Quaternion.Euler(0, 0, ThisNet.transform.eulerAngles.z - AttackDegree / AttackSpeed);
        }
        else
        {
            IsAttacking= false;
            if(ThisNet !=null)
            {
                Destroy(ThisNet);
            }
            
        }
    }
}
