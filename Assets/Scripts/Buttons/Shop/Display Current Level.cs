using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCurrentLevel : MonoBehaviour
{
    [Tooltip("PlayerPrefs.GetInt(*string*)")]
    public string PlayerPrefsVariableName;

    private TextMeshProUGUI TextMeshPro;

    private void Start()
    {
        TextMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        TextMeshPro.text = "Level: " + (PlayerPrefs.GetInt(PlayerPrefsVariableName) + 1);
    }
}
