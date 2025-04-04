/*
 * Hudson Ream
 * 3/11/2025
 * allows the player to attack with the net
 * 
 * Edited by Jacob Vanderwill
 * 4/3/25
 * Changed cooldown and speed to be controlled by level system
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class NetSwing : MonoBehaviour
{
    public UnityEvent OnSwing = new UnityEvent();
    public GameObject Net;
    private float cooldown;
    public float AttackDegree;
    private float AttackSpeed;
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

        if (PlayerPrefs.GetInt("NetLevel") + 1 == 3)
        {
            cooldown = 0.2f;
            AttackSpeed = 15f;
        }
        else if (PlayerPrefs.GetInt("NetLevel") + 1 >= 2)
        {
            cooldown = 0.3f;
            AttackSpeed = 10f;
        }
        else if (PlayerPrefs.GetInt("NetLevel") + 1 == 1)
        {
            cooldown = 0.5f;
            AttackSpeed = 8f;
        }
        else { Debug.LogError("Net level was not handled properly"); }
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
            OnSwing.Invoke();

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
