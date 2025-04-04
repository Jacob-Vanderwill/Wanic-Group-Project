using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCost : MonoBehaviour
{
    [Tooltip("PlayerPrefs.GetInt(*string*)")]
    public string PlayerPrefsVariableName;

    private TextMeshProUGUI TextMeshPro;

    private UpgradeOxygenTank UpgradeOxygenTank;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsVariableName))
            { Debug.LogError("No key '" + PlayerPrefsVariableName + "' found"); }

        TextMeshPro = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefsVariableName == "OxygenLevel")
            { UpgradeOxygenTank = GetComponentInParent<UpgradeOxygenTank>(); }
    }
    private void Update()
    {
        if (PlayerPrefsVariableName == "OxygenLevel")
        {
            if (PlayerPrefs.GetInt(PlayerPrefsVariableName) + 1 <= UpgradeOxygenTank.costsPerLevel.Length)
            {
                TextMeshPro.text = "Upgrade Cost: " + UpgradeOxygenTank.costsPerLevel[PlayerPrefs.GetInt(PlayerPrefsVariableName)];
            }
            else
            {
                TextMeshPro.text = "Upgrade cost: MAX";
            }
        }

    }
}
