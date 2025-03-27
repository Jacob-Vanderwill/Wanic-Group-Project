/*
 * Jacob Vanderwill
 * Created: 3/10/2025
 * Last Altered: 3/11/2025
 * Create a script to manage all player inputs
 */
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool UseCustomSpeed;
    public float CustomSpeed;
    private float Speed;
    private float SpeedSprint;
    [Header("Oxygen")]
    public bool UseCustomOxygenLevels;
    public float CustomMaxOxygenLevel;
    [Space]
    [Header("Death Panel")]
    public Image DeathPanel;
    public TextMeshProUGUI text;
    public TextMeshProUGUI textButton;
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

    private Vector3 inputMovement = Vector3.zero;

    private float oxygenTankSize;

    private bool isSprinting;
    [SerializeField]
    public bool isDead;
    private bool isSlow;

    private Rigidbody2D myRB;
    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();

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
            Speed = PlayerPrefs.GetFloat("Speed");
            SpeedSprint = 10;
        }
        else
        {
            Speed = CustomSpeed;
            SpeedSprint = 10;
        }

        startDrag = myRB.drag;

        DeathPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // check if dead
        if (isDead)
        {
            StartCoroutine(backToMenu());
            return;
        }
        if (health.health == 0f)
        {
            isDead = true;
        }
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
                StartCoroutine(dash());
                dashCooldownTimer = DashCooldown;
            }
        }
        //

        // look at mouse
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation= Quaternion.Euler(0, 0, Mathf.Atan2(mousepos.y - transform.position.y, mousepos.x - transform.position.x) * Mathf.Rad2Deg - 90);// update IsDead
        
        // Take oxygen away and check is oxygen is gone
        PlayerPrefs.SetFloat("OxygenLevelCurrent", PlayerPrefs.GetFloat("OxygenLevelCurrent") - Time.deltaTime);
        if (PlayerPrefs.GetFloat("OxygenLevelCurrent") <= 0 || health.health <= 0)
        {
            isDead = true;
        }

        // KEEP THIS AT THE BOTTOM
        PlayerPrefs.SetInt("IsDead", isDead ? 1 : 0);
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
    void playerPickUpFish(string name)
    {
        PlayerPrefs.SetInt(name, PlayerPrefs.GetInt(name) + 1);
        PlayerPrefs.SetInt(name + "Caught", 1);
    }
    // dash
    IEnumerator dash()
    {
        myRB.velocity = inputMovement * 35;
        myRB.drag = myRB.drag * 9f;

        yield return new WaitForSeconds(0.2f);

        myRB.drag = startDrag;
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
            textButton.color = new Color(textButton.color.r, textButton.color.g, textButton.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.collider.tag;

        // if collided with camera border then dead/if collided with fish add value
        switch (tag)
        {
            case "MainCamera":
                { health.health = 0; break; }
            case "Enemy":
                { health.health = 0; break; }
            case "Fish":
                { playerPickUpFish(collision.gameObject.name); break; }
            case "Shield":
                { health.isShieldActive = true; break; }
        }
    }
}
