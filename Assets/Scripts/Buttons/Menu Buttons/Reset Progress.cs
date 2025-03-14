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
        PlayerPrefs.SetFloat("OxygenTankSize", 25f);
        PlayerPrefs.SetFloat("OxygenLevelCurrent", 25f);
        PlayerPrefs.SetFloat("Speed", 10);
        PlayerPrefs.SetInt("IsDead", 0);

        // intinialize level
        PlayerPrefs.SetInt("OxygenLevel", 0);
        PlayerPrefs.SetInt("SpeedLevel", 0);
        PlayerPrefs.SetInt("AttackLevel", 0);
    }
}
