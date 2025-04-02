using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource AudioPlayer;
    // Start is called before the first frame update
    public void SoundPlay(AudioClip AC)
    {
        AudioPlayer.PlayOneShot(AC);
    }
}
