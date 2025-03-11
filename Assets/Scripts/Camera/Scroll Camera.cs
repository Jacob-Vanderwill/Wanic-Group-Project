using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour
{
    public float ScrollSpeed;

    public float SnapToPlayerSpeed = 1;

    public GameObject Player;

    private void Start()
    {
        SnapToPlayerSpeed /= 100;
    }

    private void FixedUpdate()
    {
        if (Player.transform.position.x > transform.position.x - 2f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x + 3f, 0f, -1f), SnapToPlayerSpeed);
        }   
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + (ScrollSpeed / 50f), 0f, -1f), 1f);
        }
    }
}
