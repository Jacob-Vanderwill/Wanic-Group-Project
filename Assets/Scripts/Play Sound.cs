using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource AudioPlayer;
    public float volume;
    // Start is called before the first frame update
    public void Sound(AudioClip AC)
    {
        AudioPlayer.PlayOneShot(AC);
        AudioPlayer.volume = volume;
    }
}
