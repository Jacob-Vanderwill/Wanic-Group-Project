using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateShopProgressBars : MonoBehaviour
{
    public Image mask;

    [Tooltip("PlayerPrefs.GetInt(*string*)")]
    public string PlayerPrefsVariableName;

    private UpgradeOxygenTank UpgradeOxygenTank;

    private void Start()
    {
        UpgradeOxygenTank = GetComponentInParent<UpgradeOxygenTank>();
        if (UpgradeOxygenTank == null )
        {
            Debug.LogError("UpgradeOxygenTank not found in parent");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }
    void GetCurrentFill()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsVariableName) + 1 <= UpgradeOxygenTank.costsPerLevel.Length)
        {
            float fillAmount = (float)PlayerPrefs.GetInt("Coins") / UpgradeOxygenTank.costsPerLevel[PlayerPrefs.GetInt(PlayerPrefsVariableName)];
            mask.fillAmount = fillAmount;
        }
        else
        {
            mask.fillAmount = 1;
        }
    }
}
