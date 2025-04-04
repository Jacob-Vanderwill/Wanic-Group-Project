/*
 * Jacob Vanderwill
 * 4/3/25
 * Last Altered: 4/3/25
 * Create a script to update shop sprites
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UpdateShopSprite : MonoBehaviour
{
    public string PlayerPrefsName;
    public Sprite[] Sprites;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        image.sprite = Sprites[PlayerPrefs.GetInt(PlayerPrefsName)];
    }
}
