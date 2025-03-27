using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayDepthHighscore : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;

    private void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        m_TextMeshProUGUI.text = "Highscrore: " + PlayerPrefs.GetInt("DepthHighscore");
    }
}
