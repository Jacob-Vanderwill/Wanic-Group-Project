using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SpawnFish : MonoBehaviour
{
    public GameObject[] Fish;
    [Tooltip("Higher number == more common")]
    public int[] Rarity;

    private GameObject player;
    private PlayerController playerController;

    private int[] spawnNum;

    private int randomNum;
    private int randomizeAmt;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) { Debug.LogWarning("Player not found"); }
        playerController = player.GetComponent<PlayerController>();

        randomizeAmt = 0;

        // make sure all values are inputted
        if (Fish.Length != Rarity.Length)
        {
            Debug.LogError("Array lengths dont match.");
            return;
        }
        spawnNum = new int[Fish.Length];

        // create randomize numbers
        for (int i = 0; i < Fish.Length; i++)
        {
            spawnNum[i] = randomizeAmt;
            randomizeAmt += Rarity[i];
        }

        // randomize
        randomNum = Random.Range(0, randomizeAmt) + Mathf.RoundToInt(player.transform.position.x / 10);

        // check which fish to spawn
        for (int i = 0; i < Fish.Length; i++)
        {
            if (i + 1 > spawnNum.Length - 1)
            {
                if (randomNum > spawnNum[i])
                {
                    Instantiate(Fish[i], this.transform.position, Quaternion.identity);
                    break;
                }
            }
            else
            {
                if (randomNum > spawnNum[i] && randomNum < spawnNum[i + 1])
                {
                    Instantiate(Fish[i], this.transform.position, Quaternion.identity);
                    break;
                }
            }
        }
    }
}
