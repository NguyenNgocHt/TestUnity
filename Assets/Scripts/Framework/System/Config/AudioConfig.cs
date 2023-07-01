using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class AudioConfigs : SingletonScriptableObject<AudioConfigs>
    {
        [SerializeField] private MusicConfigDictionary musicConfigs; public static MusicConfigDictionary MusicConfigs { get { return Instance.musicConfigs; } }
        [SerializeField] private SoundConfigDictionary soundConfigs; public static SoundConfigDictionary SoundConfigs { get { return Instance.soundConfigs; } }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            if (_instance == null)
            {
                Instance.ToString();
            }
        }
    }


    [Serializable]
    public class MusicConfigDictionary : SerializedDictionary<MusicType, MusicConfig> { }
    [Serializable]
    public class SoundConfigDictionary : SerializedDictionary<SoundType, SoundConfig> { }
}
