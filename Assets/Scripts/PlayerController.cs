/*
 * Jacob Vanderwill
 * Created: 3/10/2025
 * Last Altered: 4/3/2025
 * Create a script to manage all player inputs
 */
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool UseCustomSpeed;
    public float CustomSpeed;
    private float Speed;
    private float startSpeed;
    private float SpeedSprint;
    [Header("Oxygen")]
    public bool UseCustomOxygenLevels;
    public float CustomMaxOxygenLevel;
    [Space]
    [Header("Death Panel")]
    public Image DeathPanel;
    public TextMeshProUGUI text;
    public Image button;
    [Space]
    [Header("Shield")]
    public float ShieldTime;
    private float shieldTimer;
    [Space]
    [Header("Dash")]
    public float DashCooldown;
    private float dashCooldownTimer;
    private float startDrag;
    public AudioClip DashAudio;

    private Vector3 inputMovement = Vector3.zero;

    private float oxygenTankSize;

    [HideInInspector]
    public bool isDead;
    private bool isSlow;

    // private AudioSource audioSource;
    private Rigidbody2D myRB;
    private Health health;
    private Animator animator;
    private SpriteRenderer mySR;
    [Space]
    [Header("Audio")]
    //Audio variables
    public AudioSource AudioSource;
    public AudioClip deathsound;
    public AudioClip netSwing;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        mySR = GetComponent<SpriteRenderer>();

        if (UseCustomOxygenLevels)
        {
            oxygenTankSize = CustomMaxOxygenLevel;
        }
        else
        {
            oxygenTankSize = PlayerPrefs.GetFloat("OxygenTankSize");
        }
        PlayerPrefs.SetFloat("OxygenLevelCurrent", oxygenTankSize);

        health.health = 1;
        isDead = false;

        if (!UseCustomSpeed)
        {
            Speed = 10;
        }
        else
        {
            Speed = CustomSpeed;
        }
        SpeedSprint = 5;

        startDrag = myRB.drag;
        startSpeed = Speed;

        DeathPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // check if dead
        if (isDead)
        {
            // death animation
            animator.SetBool("IsDead", isDead);
            StartCoroutine(backToMenu());
            return;
        }
        // play net audio
        if (Input.GetMouseButtonDown(0))
        {
            AudioSource.PlayOneShot(netSwing);
        }

        // animator things
        if (myRB.velocity.x < 0.1f && myRB.velocity.y < 0.1f)
        {
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }

        if (health.health == 0f)
        {
            isDead = true;
        }
        animator.SetBool("IsCatching", Input.GetMouseButtonDown(0));

        // get movement
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");
        isSlow = Input.GetKey(KeyCode.LeftControl);


        // Dashing
        dashCooldownTimer -= Time.deltaTime;

        if (dashCooldownTimer <= 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && inputMovement != Vector3.zero)
            {
                // audioSource.PlayOneShot(DashAudio, 1);
                StartCoroutine(dash());
                dashCooldownTimer = DashCooldown;
                //plays dash audio
                AudioSource.PlayOneShot(DashAudio);
            }
        }
        //

        // look at xVelocity
        if(inputMovement.x != 0)
        {
            mySR.flipX = inputMovement.x < 0;
        }
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sqrt(Mathf.Abs(myRB.velocity.x)) * -8 * Mathf.Sign(myRB.velocity.x));
        //Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(mousepos.y - transform.position.y, mousepos.x - transform.position.x) * Mathf.Rad2Deg - 90), 0.03f);// update IsDead
        
        // Take oxygen away and check is oxygen is gone
        PlayerPrefs.SetFloat("OxygenLevelCurrent", PlayerPrefs.GetFloat("OxygenLevelCurrent") - Time.deltaTime);
        if (PlayerPrefs.GetFloat("OxygenLevelCurrent") <= 0 || health.health <= 0)
        {
            isDead = true;
        }

        // KEEP THIS AT THE BOTTOM
        PlayerPrefs.SetInt("IsDead", isDead ? 1 : 0);
        if(isDead)
        {
            // death sound
            AudioSource.PlayOneShot(deathsound);
        }
    }
    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        // add forces
        if (isSlow)
        {
            myRB.AddForce(inputMovement.normalized * SpeedSprint);
        }
        else
        {
            myRB.AddForce(inputMovement.normalized * Speed);
        }
        if (health.isShieldActive)
        {
            if (ShieldTime > shieldTimer)
            {
                shieldTimer += Time.deltaTime;
            }
            else
            {
                shieldTimer = 0;
                health.isShieldActive = false;
            }
        }

        // KEEP THIS AT THE BOTTOM
        PlayerPrefs.SetInt("IsDead", isDead ? 1 : 0);
    }
    public void playerPickUpFish(string name)
    {
        PlayerPrefs.SetInt(name, PlayerPrefs.GetInt(name) + 1);
        PlayerPrefs.SetInt(name + "Caught", 1);

        PlayerPrefs.SetFloat("OxygenLevelCurrent", 
                             PlayerPrefs.GetFloat("OxygenLevelCurrent") + 
                            (PlayerPrefs.GetFloat("OxygenTankSize") * 0.10f));
    }
    public void boostSpeed()
    {
        StartCoroutine(speedBoost());
    }
    // dash
    IEnumerator dash()
    {
        myRB.velocity = inputMovement.normalized * 35;
        myRB.drag = myRB.drag * 9f;

        yield return new WaitForSeconds(0.2f);

        myRB.drag = startDrag;
    }
    // speed boost
    IEnumerator speedBoost()
    {
        Speed = Speed = 15f;

        yield return new WaitForSeconds(10f);

        Speed = startSpeed;
    }
    // start menu screen when dead
    IEnumerator backToMenu()
    {
        float duration = 2.5f;
        float elapsedTime = 0f;

        DeathPanel.gameObject.SetActive(true);
        Color panelColor = DeathPanel.color;

        while (elapsedTime < duration)
        {
            // Convert 180 to 0-1 range
            float alpha = Mathf.Lerp(0f, 200f / 255f, elapsedTime / duration);
            // fade the things in
            DeathPanel.color = new Color(panelColor.r, panelColor.g, panelColor.b, alpha);
            button.color = new Color(button.color.r, button.color.g, button.color.b, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.collider.tag;

        // if collided with camera border then dead etc
        switch (tag)
        {
            case "MainCamera":
                { health.health = 0; break; }
            case "Enemy":
                { health.health = 0; break; }
            case "Fish":
                { health.health = 0; break; }
            case "Shield":
                { health.isShieldActive = true; break; }
        }
    }
}
