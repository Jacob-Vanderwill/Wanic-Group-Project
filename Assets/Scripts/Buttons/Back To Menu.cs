using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void backToMenu()
    {
        SceneManager.LoadScene("Jacob Menu Test");
    }
}
