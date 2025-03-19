/*
 * Hudson Ream
 * 3/19/2025
 * when a fish is killed then the player collects it with a cool animation
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDeath : MonoBehaviour
{
    public GameObject DeadFish;
    public GameObject Player;
    GameObject NewDeadFish;
    public void OnFishDeath()
    {
        NewDeadFish = Instantiate(DeadFish);
        NewDeadFish.transform.position = this.gameObject.transform.position;
        NewDeadFish.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-10, 10);
        NewDeadFish.GetComponent<SpringJoint2D>().connectedBody = Player.GetComponent<Rigidbody2D>();
        NewDeadFish.GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
        Destroy(this.gameObject);
    }
}
