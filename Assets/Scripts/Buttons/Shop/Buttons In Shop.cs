using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ButtonsInShop : MonoBehaviour
{
    public void upgradeOxygenTank(int cost)
    {
        if (PlayerPrefs.GetInt("Coins") < cost)
        {
            return;
        }
        int oxygenLevel = PlayerPrefs.GetInt("OxygenLevel");

        // starts with lvl 0 (25)
        int[] oxygenTankLevels = { 25, 50, 75, 100, 200, 300, 400, 500, 750, 1000 }; // etc...

        if (oxygenLevel + 1 > oxygenTankLevels.Length)
        {
            return;
        }
        oxygenLevel++;

        PlayerPrefs.SetFloat("OxygenTankSize", oxygenTankLevels[oxygenLevel]);
    }
    public void upgradeSpeed(int cost)
    {

    }
    public void upgradeAttack(int cost)
    {

    }
}
