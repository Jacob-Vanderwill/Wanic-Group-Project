using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnClick : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
