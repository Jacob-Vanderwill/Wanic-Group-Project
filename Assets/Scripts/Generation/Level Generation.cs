/*
 * Jacob Vanderwill
 * 3/18/25
 * Last Altered 3/18/25
 * Create a script that will spawn the map as the player goes
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] Tiles;

    private float lastX;
    private int selectedTile;

    private void Start()
    {
        selectedTile = Random.Range(0, Tiles.Length);

        Vector3 spawnPos = new Vector3(transform.position.x + 15, 0, 1);

        GameObject tile = Instantiate(Tiles[selectedTile], spawnPos, Quaternion.identity);

        // +5 gives empty space between tiles
        lastX = spawnPos.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x + 40 >= lastX)
        {
            placeTile();
        }
    }
    private void placeTile()
    {
        selectedTile = Random.Range(0, Tiles.Length);

        Vector3 spawnPos = new Vector3(lastX + 28, 0, 1);

        GameObject tile = Instantiate(Tiles[selectedTile], spawnPos, Quaternion.identity);

        // +10 gives empty space between tiles
        lastX = spawnPos.x;
    }
}