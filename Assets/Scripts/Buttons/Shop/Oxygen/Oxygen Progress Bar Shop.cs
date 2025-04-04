using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class OxygenProgressBarShop : MonoBehaviour
{
    public Image mask;

    public Canvas canvas;

    private int cost;
    private int coins;

    private UpgradeOxygenTank UpgradeOxygenTank;

    // Update is called once per frame
    void Update()
    {
        UpgradeOxygenTank = canvas.GetComponent<UpgradeOxygenTank>();

        cost = UpgradeOxygenTank.costsPerLevel[PlayerPrefs.GetInt("OxygenLevel")];
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
        if (PlayerPrefs.GetInt("OxygenLevel") + 1 >= UpgradeOxygenTank.costsPerLevel.Length)
        {
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0.6f, 0, 1);
            mask.fillAmount = 1;
            return;
        }
        else gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color (0.6f, 1, 0.6f, 1);

        float fillAmount = (float)coins / (float)cost;
        mask.fillAmount = fillAmount;
    }
}
