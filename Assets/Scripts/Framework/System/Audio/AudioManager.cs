using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource audioSource;
    AudioTrackerDictionary audioTrackers = new AudioTrackerDictionary();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        GameObject obj = new GameObject(typeof(AudioManager).ToString());
        AudioManager audio = obj.AddComponent<AudioManager>();
        audio.audioSource = obj.AddComponent<AudioSource>();
        audio.audioSource.loop = true;
        DontDestroyOnLoad(Instance);
    }
    protected override void Awake()
    {
        base.Awake();
        ObjectPoolManager.GenerateObject<AudioSource>(PrefabFactory.AudioSourcePrefab, Instance.gameObject, PoolConfig.DefaultInitPoolSound).gameObject.SetActive(false);
    }
    public void PlaySound(SoundType sound, ClipConfig clipConfig, Transform transform, bool isFollow)
    {
        AudioSource audioSrc;
        AudioTracker audioTracker;
        if (audioTrackers.ContainsKey(sound))
        {
            audioTracker = audioTrackers[sound];
            if (audioTracker.IsFullActiveSound())
            {
                return;
            }
        }
        else
        {
            audioTracker = Instantiate(new GameObject(), this.transform).AddComponent<AudioTracker>();
            audioTracker.name = "Tracker " + sound.ToString();
            audioTracker.type = sound;
            if (!isFollow)
            {
                audioTracker.transform.parent = this.transform;
            }
            else
            {
                audioTracker.transform.parent = transform;
            }
            audioTrackers.Add(sound, audioTracker);
            if (audioTracker.IsFullActiveSound())
            {
                return;
            }
        }
        audioSrc = ObjectPoolManager.GenerateObject<AudioSource>(PrefabFactory.AudioSourcePrefab, audioTracker.gameObject);
        audioSrc.transform.parent = audioTracker.transform;
        audioSrc.clip = clipConfig.clip;
        audioSrc.spatialBlend = AudioConfigs.SoundConfigs[sound].spatial;
        Debug.Log(clipConfig.volumn);
        audioSrc.volume = clipConfig.volumn;
        audioSrc.Play();
    }
    public void PlayMusic(AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }
}

[Serializable]
public class AudioTrackerDictionary : SerializedDictionary<SoundType, AudioTracker> { }