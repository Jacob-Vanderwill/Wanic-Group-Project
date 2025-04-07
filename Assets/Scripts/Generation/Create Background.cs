/*
 * Jacob Vanderwill
 * 4/3/25
 * Last Altered 4/3/25
 * Create a script that will create the background
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBackground : MonoBehaviour
{
    public GameObject background;

    private GameObject player;

    private float thisX;

    private float lastX;
    private float startY;
    private float startX;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if( player == null) { Debug.LogWarning("Player Not Found: " + gameObject.name); return; }

        startX = -10f;

        startY = 0.56f;
        thisX = startX;

        Instantiate(background, new Vector3(thisX, startY, 0), Quaternion.identity);

        lastX = thisX;
    }

    private void Update()
    {
        if (player.transform.position.x + 35 - lastX >= 19.614753f)
        {
            thisX = lastX + 19.614753f;

            Instantiate(background, new Vector3(thisX, startY, 0), Quaternion.identity);

            lastX = thisX;
        }
    }
}
