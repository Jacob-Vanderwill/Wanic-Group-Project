/*
 * Jacob Vanderwill
 * 4/3/25
 * Last Altered: 4/3/25
 * Create a script to update net
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeNet : MonoBehaviour
{
    public int[] costsPerLevel;
    public int[] NetDamageLevels;
    public void upgradeNet()
    {
        int netLevel = PlayerPrefs.GetInt("NetLevel");

        if (netLevel + 1 >= NetDamageLevels.Length)
        {
            return;
        }

        if (PlayerPrefs.GetInt("Coins") < costsPerLevel[PlayerPrefs.GetInt("NetLevel")])
        {
            return;
        }

        netLevel++;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - costsPerLevel[PlayerPrefs.GetInt("NetLevel")]);

        PlayerPrefs.SetFloat("NetDamage", NetDamageLevels[netLevel]);

        PlayerPrefs.SetInt("NetLevel", netLevel);
    }
}
