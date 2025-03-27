using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayFishCount : MonoBehaviour
{
    [Tooltip("(ex) Fish1, Fish2")]
    public string FishName;
    private TextMeshProUGUI TextMeshPro;

    private void Start()
    {
        TextMeshPro = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        TextMeshPro.text = "" + PlayerPrefs.GetInt(FishName);
    }
}
