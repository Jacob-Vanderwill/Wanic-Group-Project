/* Hudson Ream
 * 4/1/2025
 * Plays the correct tracks for the givin depth the player has traveled
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource[] musicPlayer;
    public AudioClip BaseMusicTrack;
    public AudioClip FirstDepthAddon;
    public AudioClip SecondDepthAddon;
    // Start is called before the first frame update
    void Start()
    {
        
        musicPlayer = GetComponents<AudioSource>();
        musicPlayer[0].volume = 0.5f;
        musicPlayer[1].volume = 0;
        musicPlayer[2].volume = 0;
        musicPlayer[0].clip = BaseMusicTrack;
        musicPlayer[1].clip = FirstDepthAddon;
        musicPlayer[2].clip = SecondDepthAddon;
        musicPlayer[0].Play();
        musicPlayer[1].Play();
        musicPlayer[2].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= 300 && musicPlayer[1].volume <= 0.5f)
        {  
            musicPlayer[1].volume += Time.deltaTime;
        }
        if(transform.position.x >= 600 && musicPlayer[2].volume <= 0.5f)
        {
            musicPlayer[2].volume += Time.deltaTime/3;
        }
    }
}
