using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpeed : MonoBehaviour
{
    public int[] costsPerLevel;
    public int[] speedLevels;
    public void upgradeSpeed()
    {
        if (PlayerPrefs.GetInt("Coins") < costsPerLevel[PlayerPrefs.GetInt("SpeedLevel")])
        {
            return;
        }

        int speedLevel = PlayerPrefs.GetInt("SpeedLevel");

        if (speedLevel + 1 >= speedLevels.Length)
        {
            return;
        }

        speedLevel++;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - costsPerLevel[PlayerPrefs.GetInt("SpeedLevel")]);

        PlayerPrefs.SetFloat("Speed", speedLevels[speedLevel]);

        PlayerPrefs.SetInt("SpeedLevel", speedLevel);
    }
}
