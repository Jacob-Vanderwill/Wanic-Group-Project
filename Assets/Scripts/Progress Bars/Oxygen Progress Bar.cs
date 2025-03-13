using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class OxygenProgressBar : MonoBehaviour
{
    public Image mask;

    public float MaxOxygen;
    public float Current;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }
    void GetCurrentFill()
    {
        float fillAmount = PlayerPrefs.GetFloat("OxygenLevelCurrent") / PlayerPrefs.GetFloat("OxygenTankSize");
        mask.fillAmount = fillAmount;
    }
}
