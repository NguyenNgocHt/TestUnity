using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMono : MonoBehaviour
{
    AudioSource audio;
    public float time;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        time = audio.time / audio.clip.length;
        if (time==1 || !audio.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}
