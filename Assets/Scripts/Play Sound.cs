using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private List<AudioSource> NewAudioSources;
    private List<AudioSource> audiosources;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        audiosources.Clear();
        foreach(AudioSource source in NewAudioSources)
        {
            Destroy(source);
            if (!source.isPlaying)
            {
                audiosources.Add(source);
            }
            
            
        }
        foreach(AudioSource source in audiosources)
        {
            NewAudioSources.Remove(source);
        }
    }
    public void SoundPlay(AudioClip AC)
    {
        AudioSource NewAudioSource = this.gameObject.AddComponent<AudioSource>();
        NewAudioSource.clip = AC;
        NewAudioSource.Play();
        NewAudioSources.Add(NewAudioSource);
    }
}
