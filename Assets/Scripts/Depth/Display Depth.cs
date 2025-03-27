/*
 * Jacob Vanderwill
 * 3/27/25
 * Last Altered: 3/27/25
 * Create a script to display depth
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayDepth : MonoBehaviour
{
    private GameObject Player;
    private TextMeshProUGUI TextMeshPro;

    private int playerStartX;
    private int startDepth;
    private int depth;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        TextMeshPro = GetComponent<TextMeshProUGUI>();

        playerStartX = Mathf.RoundToInt(Player.transform.position.x);
    }

    private void Update()
    {
        depth = (Mathf.RoundToInt(Player.transform.position.x) - playerStartX) + startDepth;

        TextMeshPro.text = "Depth: " + depth;

        if (depth > PlayerPrefs.GetInt("DepthHighscore"))
        {
            PlayerPrefs.SetInt("DepthHighscore", depth);
        }
    }
}
