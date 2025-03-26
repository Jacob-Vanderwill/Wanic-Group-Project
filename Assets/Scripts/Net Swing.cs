/*
 * Hudson Ream
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
    public float AttackArea;
    public GameObject DeadFish;
    float degreesmoved;
    float currentrotation;
    float cooldownCount = 0;
    bool IsAttacking;
    GameObject ThisNet;
    PlayerController PlayerController;
    // Start is called before the first frame update
    void Start()
    {
        ThisNet = null;  
        PlayerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.isDead == true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldownCount <= 0)
        {
            cooldownCount = cooldown;
            degreesmoved = AttackDegree;
            ThisNet = Instantiate(Net, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + (AttackDegree * 0.5f)));
            ThisNet.transform.localScale = new Vector2(0, 0);
            currentrotation = transform.eulerAngles.z - (AttackDegree * 0.5f);
            IsAttacking= true;
            
        }
        if(cooldownCount > 0)
        {
            cooldownCount -= Time.deltaTime;
        }

        if (IsAttacking == true)
        {
            ThisNet.transform.position = transform.position;
        }
       
    }
    void FixedUpdate()
    {
        if (PlayerController.isDead == true)
        {
            return;
        }

        if (IsAttacking && degreesmoved > 0)
        {
            degreesmoved -= AttackDegree / AttackSpeed;
            ThisNet.transform.rotation = Quaternion.Euler(0, 0, ThisNet.transform.eulerAngles.z - AttackDegree / AttackSpeed);
            float NetSize;
            NetSize = (-1 * Mathf.Pow(((degreesmoved / AttackDegree) - 0.5f)*2, 4) + 1);
            ThisNet.transform.localScale= new Vector2(NetSize * AttackArea, NetSize * AttackArea);

        }
        else
        {
            IsAttacking= false;
            if(ThisNet != null)
            {
                Destroy(ThisNet);
                ThisNet = null;
            }
            
        }
    }
}
