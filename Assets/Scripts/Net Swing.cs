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
    
    public GameObject FishNet;
    private GameObject thisFishNet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            {
            thisFishNet = Instantiate(FishNet, transform.position, Quaternion.identity, transform);
            }
    }
}
