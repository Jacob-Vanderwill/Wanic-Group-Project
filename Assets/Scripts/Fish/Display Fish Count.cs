using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayFishCount : MonoBehaviour
{
    private string playerprefsName;

    private FishExchangeDisplay fishExchangeDisplay;
    private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        fishExchangeDisplay = gameObject.transform.parent.GetComponent<FishExchangeDisplay>();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        playerprefsName = fishExchangeDisplay.PlayerPrefsName;
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt(playerprefsName + "Caught") == 0) { textMeshProUGUI.text = ""; return; }

        textMeshProUGUI.text = PlayerPrefs.GetInt(playerprefsName).ToString();   
    }
}
