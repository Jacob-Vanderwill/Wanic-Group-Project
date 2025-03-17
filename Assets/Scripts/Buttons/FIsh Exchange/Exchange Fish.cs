using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeFish : MonoBehaviour
{
    [Tooltip("Fish1, Fish2, Fish3")]
    public string WhichFish;
    public int Value;

    public void exchangeFish()
    {
        if (PlayerPrefs.GetInt(WhichFish) > 0)
        {
            PlayerPrefs.SetInt(WhichFish, PlayerPrefs.GetInt(WhichFish) - 1);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + Value);
        }
    }
    public void exchangeAllFish()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + (PlayerPrefs.GetInt(WhichFish) * Value));
        PlayerPrefs.SetInt(WhichFish, 0);
    }
}
