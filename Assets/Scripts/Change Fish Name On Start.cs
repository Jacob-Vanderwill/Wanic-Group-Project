using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFishNameOnStart : MonoBehaviour
{
    public string Name;

    private void Start()
    {
        gameObject.name = Name;
    }
}
