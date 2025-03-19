using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class DeletePassedLevel : MonoBehaviour
{
    public GameObject Player;

    private void Update()
    {
        transform.position = new Vector3(Player.transform.position.x - 60, 0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.collider.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
