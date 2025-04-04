/*
 * Jacob Vanderwill
 * 4/4/25
 * Last Altered 4/4/25
 * Boost player when entered
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpeed : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var playerController = collision.GetComponent<PlayerController>();
            if (playerController == null) { Debug.Log("No Playercontroller: " + gameObject); return; }

            playerController.boostSpeed();
            Destroy(this.gameObject);
        }
        
    }
}
