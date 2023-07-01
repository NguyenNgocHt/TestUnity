using Framework;
using UnityEngine;
using UnityEngine.XR;

public static class AudioHelper
{
    public static void PlaySound(this SoundType sound, Transform transform = null)
    {
        if (!PDataSettings.SoundEnabled) return;
        SoundConfig soundConfig = AudioConfigs.SoundConfigs[sound];
        AudioManager.Instance.PlaySound(sound, soundConfig.clipConfigs[Random.Range(0, AudioConfigs.SoundConfigs[sound].clipConfigs.Length)], transform, soundConfig.isFollow);
    }

    public static void StopAllSound(string soundName)
    {
    }
    public static void PlayMusic(this MusicType music)
    {
        if (!PDataSettings.MusicEnabled) return;
        AudioManager.Instance.PlayMusic(AudioConfigs.MusicConfigs[music].clip, AudioConfigs.MusicConfigs[music].volume);
    }

    public static void StopMusic()
    {
        AudioManager.Instance.audioSource.Stop();
    }

    public static void PauseMusic()
    {
        AudioManager.Instance.audioSource.Pause();
    }
    public static void ResumeMusic()
    {
        if (!PDataSettings.MusicEnabled) return;
        AudioManager.Instance.audioSource.UnPause();
    }
}
