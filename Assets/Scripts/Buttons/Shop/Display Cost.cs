using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCost : MonoBehaviour
{
    private TextMeshProUGUI TextMeshPro;
    private UpgradeOxygenTank UpgradeOxygenTank;

    private void Start()
    {
        TextMeshPro = GetComponent<TextMeshProUGUI>();
        UpgradeOxygenTank = GetComponentInParent<UpgradeOxygenTank>();
    }
    private void Update()
    {
        TextMeshPro.text = "Upgrade Cost: " + UpgradeOxygenTank.costsPerLevel[PlayerPrefs.GetInt("OxygenLevel")];
        
        // debug

        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerPrefs.SetInt("Coins", 100);
        }

        //
    }
}
