/*Hudson Ream
 * 4/4/2025
 * does everything needed to make the mine work
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplode : MonoBehaviour
{
    private Animator Animator;
    private SpriteRenderer thisSR;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        thisSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Animator.SetBool("Is Touching", true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Animator.SetBool("Is Touching", true);
    }

    public void DamageAndDelete()
    {
        thisSR.sprite = null;
        Destroy(gameObject);
    }
}   
