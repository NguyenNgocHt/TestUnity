using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public AudioSource audioPlay;
    public AudioClip shotting_sound;
    public AudioClip rocket_sound;
    public AudioClip boom_sound;
    public AudioClip bullet_collision;
    // Start is called before the first frame update
    void Start()
    {
        audioPlay.volume = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void effect_boom()
    {
        if (audioPlay && boom_sound)
        {
            audioPlay.PlayOneShot(boom_sound);
        }
    }
    public void effect_shotting()
    {
        if (audioPlay && shotting_sound)
        {
            audioPlay.PlayOneShot(shotting_sound);
        }
    }
    public void effect_bulletColision()
    {
        if(audioPlay && bullet_collision)
        {
            audioPlay.PlayOneShot(bullet_collision);
        }
    }
    public void effect_rocket()
    {
        if(audioPlay && rocket_sound)
        {
            audioPlay.PlayOneShot(rocket_sound);
        }
    }
}
