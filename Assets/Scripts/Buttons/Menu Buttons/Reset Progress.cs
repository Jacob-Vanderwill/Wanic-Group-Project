/*
 * Jacob Vanderwill
 * Created 3/14/25
 * Last Altered: 4/3/25
 * Create a script to reset ALL progress when needed
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    public void resetProgress()
    {
        // initialize basic data
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins"));
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("NetDamage", 1);
        PlayerPrefs.SetFloat("OxygenTankSize", 25f);
        PlayerPrefs.SetFloat("OxygenLevelCurrent", 25f);
        PlayerPrefs.SetInt("IsDead", 0);

        // highscore (depth)
        PlayerPrefs.SetInt("DepthHighscore", 0);

        // initialize if fish have been caught
        PlayerPrefs.SetInt("Fish1Caught", 0);
        PlayerPrefs.SetInt("Fish2Caught", 0);
        PlayerPrefs.SetInt("Fish3Caught", 0);
        PlayerPrefs.SetInt("Fish4Caught", 0);
        PlayerPrefs.SetInt("Fish5Caught", 0);
        PlayerPrefs.SetInt("Fish6Caught", 0);
        PlayerPrefs.SetInt("Fish7Caught", 0);
        PlayerPrefs.SetInt("Fish8Caught", 0);
        PlayerPrefs.SetInt("Fish9Caught", 0);
        PlayerPrefs.SetInt("Fish10Caught", 0);
        PlayerPrefs.SetInt("Fish11Caught", 0);
        PlayerPrefs.SetInt("Fish12Caught", 0);
        PlayerPrefs.SetInt("Fish13Caught", 0);
        PlayerPrefs.SetInt("Fish14Caught", 0);
        PlayerPrefs.SetInt("Fish15Caught", 0);
        PlayerPrefs.SetInt("Fish16Caught", 0);
        PlayerPrefs.SetInt("Fish17Caught", 0);
        PlayerPrefs.SetInt("Fish18Caught", 0);
        PlayerPrefs.SetInt("Fish19Caught", 0);
        PlayerPrefs.SetInt("Fish20Caught", 0);

        // initialize fish count
        PlayerPrefs.SetInt("Fish1", 0);
        PlayerPrefs.SetInt("Fish2", 0);
        PlayerPrefs.SetInt("Fish3", 0);
        PlayerPrefs.SetInt("Fish4", 0);
        PlayerPrefs.SetInt("Fish5", 0);
        PlayerPrefs.SetInt("Fish6", 0);
        PlayerPrefs.SetInt("Fish7", 0);
        PlayerPrefs.SetInt("Fish8", 0);
        PlayerPrefs.SetInt("Fish9", 0);
        PlayerPrefs.SetInt("Fish10", 0);
        PlayerPrefs.SetInt("Fish11", 0);
        PlayerPrefs.SetInt("Fish12", 0);
        PlayerPrefs.SetInt("Fish13", 0);
        PlayerPrefs.SetInt("Fish14", 0);
        PlayerPrefs.SetInt("Fish15", 0);
        PlayerPrefs.SetInt("Fish16", 0);
        PlayerPrefs.SetInt("Fish17", 0);
        PlayerPrefs.SetInt("Fish18", 0);
        PlayerPrefs.SetInt("Fish19", 0);
        PlayerPrefs.SetInt("Fish20", 0);

        // intinialize level
        PlayerPrefs.SetInt("OxygenLevel", 0);
        PlayerPrefs.SetInt("NetLevel", 0);

        // Show its active
        PlayerPrefs.SetInt("Active", 1);
    }
}
