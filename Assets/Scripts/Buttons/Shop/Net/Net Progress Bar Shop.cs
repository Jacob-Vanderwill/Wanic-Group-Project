/*
 * Jacob Vanderwill
 * 4/3/25
 * Last Altered: 4/3/25
 * Create a script to update shop progress bar for net
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetProgressBarShop : MonoBehaviour
{
    public Image mask;

    public Canvas canvas;

    private int cost;
    private int coins;

    private UpgradeNet UpgradeNet;

    // Update is called once per frame
    void Update()
    {
        UpgradeNet = canvas.GetComponent<UpgradeNet>();

        cost = UpgradeNet.costsPerLevel[PlayerPrefs.GetInt("NetLevel")];
        coins = PlayerPrefs.GetInt("Coins");

        GetCurrentFill();


        // debug

        if (Input.GetKey(KeyCode.G))
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
        }

        //
    }
    void GetCurrentFill()
    {
        if (PlayerPrefs.GetInt("NetLevel") + 1 >= UpgradeNet.costsPerLevel.Length)
        {
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0.6f, 0, 1);
            mask.fillAmount = 1;
            return;
        }
        else gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.6f, 1, 0.6f, 1);

        float fillAmount = (float)coins / (float)cost;
        mask.fillAmount = fillAmount;
    }
}
