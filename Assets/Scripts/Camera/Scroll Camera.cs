/*
 * Jacob Vanderwill
 * Created: 3/11/2025
 * Last Altered: 3/12/2025
 * Create a script that will scroll the camera (horizontally) and follow player inf the player gets ahead
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
    public float ScrollSpeed;

    public float SnapToPlayerSpeed = 1;

    public GameObject Player;

    private float startY;
    private float endX;

    private void Start()
    {
        SnapToPlayerSpeed /= 100;
        startY = transform.position.y;
    }

    private void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("IsDead") == 1)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(endX + 2, startY, -1f), 0.03f);
        }
        else
        {
            // if the player is ahead then scroll to player rather than at a set speed
            if (Player.transform.position.x > transform.position.x - 2f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x + 3f, startY, -1f), SnapToPlayerSpeed);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + (ScrollSpeed / 50f), startY, -1f), 1f);
            }
            endX = transform.position.x;
        }
    }
}
