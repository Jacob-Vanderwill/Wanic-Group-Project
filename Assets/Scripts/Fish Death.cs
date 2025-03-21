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
    GameObject Player;
    GameObject NewDeadFish;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnFishDeath()
    {
        NewDeadFish = Instantiate(DeadFish);
        NewDeadFish.transform.position = gameObject.transform.position;
        NewDeadFish.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-10, 10);
        NewDeadFish.GetComponent<SpringJoint2D>().connectedBody = Player.GetComponentInChildren<Rigidbody2D>();
        NewDeadFish.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        NewDeadFish.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
        NewDeadFish.transform.localScale = gameObject.transform.localScale;
        Destroy(gameObject);
    }
}
