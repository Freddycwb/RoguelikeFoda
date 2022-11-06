using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource audio;

    public AudioClip music;
    public AudioClip cutsceneSound;

    public float musicVolume;
    public float cutsceneSoundVolume;

    private void Start()
    {
        audio = GetComponent<AudioSource>();    
    }

    public void CallCutsceneSounds()
    {
        StartCoroutine("CutsceneSounds");
    }

    public void PlayMusic()
    {
        audio.clip = music;
        audio.volume = musicVolume;
        audio.Play();
    }

    public IEnumerator CutsceneSounds()
    {
        audio.Pause();
        audio.volume = cutsceneSoundVolume;
        yield return new WaitForSeconds(2.2f);
        audio.clip = cutsceneSound;
        audio.Play();
    }
}
