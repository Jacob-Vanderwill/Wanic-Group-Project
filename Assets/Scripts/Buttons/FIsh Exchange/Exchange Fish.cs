using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExchangeFish : MonoBehaviour
{
    [Tooltip("1, 2, 3, 4")]
    public int FishIndex;

    public AudioSource Listener;
    public AudioClip Clip;

    private int[] Value;

    private void Start()
    {
        Value = new int[] { 1, 1, 1, 3, 3, 3, 3, 5, 5, 5, 15 };
    }

    public void exchangeFish(int fishIndex)
    {
        if (PlayerPrefs.GetInt("Fish" + fishIndex) > 0)
        {
            PlayerPrefs.SetInt("Fish" + fishIndex, PlayerPrefs.GetInt("Fish" + fishIndex) - 1);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + Value[fishIndex]);

            Listener.PlayOneShot(Clip);
        }
    }
    public void exchangeAllFish(int fishIndex)
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + (PlayerPrefs.GetInt("Fish"+ fishIndex) * Value[fishIndex]));
        PlayerPrefs.SetInt("Fish" + fishIndex, 0);
    }
}
