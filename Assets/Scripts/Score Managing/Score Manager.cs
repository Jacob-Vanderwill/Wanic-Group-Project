/*
 * Jacob Vanderwill
 * Created: 3/12/2025
 * Last Altered: 3/12/2025
 * Create a script to manage scores
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int coinCount;
    public static int coinsThisSession;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetFloat("OxygenTankSize", 0f);
        PlayerPrefs.SetInt("IsDead", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            coinCount = PlayerPrefs.GetInt("Coins");
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        ScoreManager.coinCount++;
        ScoreManager.coinsThisSession++;
        */
    }
}
