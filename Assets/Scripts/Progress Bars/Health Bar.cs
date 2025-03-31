using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Fish;
    private Health Health;

    public Image mask;

    public float MaxOxygen;
    public float Current;

    private void Start()
    {
        Health = Fish.GetComponent<Health>();
    }
    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }
    void GetCurrentFill()
    {
        float fillAmount = Health.health / Health.maxHealth;
        mask.fillAmount = fillAmount;
    }
}
