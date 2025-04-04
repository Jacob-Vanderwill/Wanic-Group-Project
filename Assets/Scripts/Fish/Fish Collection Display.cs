/*
 * Jacob Vanderwill
 * 4/3/25
 * Last Altered 4/3/25
 * Create a script to display locks when a fish hasn't been discovered
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FishCollectionDisplay : MonoBehaviour
{
    public string PlayerPrefsName;
    public Sprite FishImage;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsName + "Caught") == 0) { return; }

        image = GetComponent<Image>();
        image.sprite = FishImage;
    }
}
