


/*
 * Hudson
 * 3/20/2024
 * Attracts an object to another
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour
{
    public GameObject PointOfAttration;
    public float Magnitude;
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Time.deltaTime * Magnitude * (PointOfAttration.transform.position.normalized - transform.position.normalized));
    }
}
