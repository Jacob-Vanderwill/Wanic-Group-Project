/*
 * Jacob Vanderwill
 * 4/3/25
 * Last Altered 4/3/25
 * Create a script to display locks when a fish hasn't been discovered
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishExchangeDisplay : MonoBehaviour
{
    public string PlayerPrefsName;
    public Sprite Sprite;

    private Image image;

    private void Start()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsName + "Caught") == 1) { return; }

        image = GetComponent<Image>();
        image.sprite = Sprite;
    }
}
