using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentOxygenLevel : MonoBehaviour
{
    private TextMeshProUGUI TextMeshPro;

    private void Start()
    {
        TextMeshPro = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        TextMeshPro.text = "Level: " + (PlayerPrefs.GetInt("OxygenLevel") + 1);
    }
}
