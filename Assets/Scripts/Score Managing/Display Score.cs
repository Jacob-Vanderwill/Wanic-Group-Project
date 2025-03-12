using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        TextMeshPro.print(PlayerPrefs.GetInt("Coins"));
    }
}
