using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOxygenTank : MonoBehaviour
{
    public int[] costsPerLevel;
    public int[] oxygenTankLevels;
    public void upgradeOxygenTank()
    {
        int oxygenLevel = PlayerPrefs.GetInt("OxygenLevel");

        if (oxygenLevel + 1 >= oxygenTankLevels.Length)
        {
            return;
        }

        if (PlayerPrefs.GetInt("Coins") < costsPerLevel[PlayerPrefs.GetInt("OxygenLevel")])
        {
            return;
        }

        oxygenLevel++;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - costsPerLevel[PlayerPrefs.GetInt("OxygenLevel")]);

        PlayerPrefs.SetFloat("OxygenTankSize", oxygenTankLevels[oxygenLevel]);

        PlayerPrefs.SetInt("OxygenLevel", oxygenLevel);
    }
}
