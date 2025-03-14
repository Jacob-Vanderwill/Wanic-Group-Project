using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOxygenTank : MonoBehaviour
{
    public int[] costsPerLevel;
    public int[] oxygenTankLevels = { 25, 50, 75, 100, 200, 300, 400, 500, 750, 1000 }; // etc...
    public void upgradeOxygenTank()
    {
        if (PlayerPrefs.GetInt("Coins") < costsPerLevel[PlayerPrefs.GetInt("OxygenLevel")])
        {
            return;
        }

        int oxygenLevel = PlayerPrefs.GetInt("OxygenLevel");

        // starts with lvl 0 (25)

        if (oxygenLevel > oxygenTankLevels.Length)
        {
            return;
        }

        oxygenLevel++;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - costsPerLevel[PlayerPrefs.GetInt("OxygenLevel")]);

        PlayerPrefs.SetFloat("OxygenTankSize", oxygenTankLevels[oxygenLevel]);

        PlayerPrefs.SetInt("OxygenLevel", oxygenLevel);
    }
}
