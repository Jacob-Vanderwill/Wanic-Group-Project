/*
 * Jacob Vanderwill
 * Created 4/4/25
 * Last Altered: 4/4/25
 * Create a script to spawn speed boosts based on a chance
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpeed : MonoBehaviour
{
    public GameObject speed;
    public int PercChance;
    private void Start()
    {
        if (PercChance <= Random.Range(0,100))
        {
            Instantiate(speed, transform.position, Quaternion.identity);
        }
    }
}
