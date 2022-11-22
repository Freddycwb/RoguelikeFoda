using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource source;

    public AudioClip music;
    public AudioClip cutsceneSound;

    public float musicVolume;
    public float cutsceneSoundVolume;

    private void Start()
    {
        source = GetComponent<AudioSource>();    
    }

    public void CallCutsceneSounds()
    {
        StartCoroutine("CutsceneSounds");
    }

    public void PlayMusic()
    {
        source.clip = music;
        source.volume = musicVolume;
        source.Play();
    }

    public IEnumerator CutsceneSounds()
    {
        source.Pause();
        source.volume = cutsceneSoundVolume;
        yield return new WaitForSeconds(2);
        source.clip = cutsceneSound;
        source.Play();
    }
}
