using UnityEngine;

namespace Framework
{
    public class PDataSettings : PDataBlock<PDataSettings>
    {
        [SerializeField] PDataUnit<bool> _soundEnabled;
        [SerializeField] PDataUnit<bool> _musicEnabled;
        [SerializeField] PDataUnit<bool> _vibrationEnabled;

        public static bool SoundEnabled { get { return Instance._soundEnabled.Data; } set { Instance._soundEnabled.Data = value; } }
        public static bool MusicEnabled { get { return Instance._musicEnabled.Data; } set { Instance._musicEnabled.Data = value; } }
        public static bool VibrationEnabled { get { return Instance._vibrationEnabled.Data; } set { Instance._vibrationEnabled.Data = value; } }

        public static PDataUnit<bool> SoundEnabledData { get { return Instance._soundEnabled; } }
        public static PDataUnit<bool> MusicEnabledData { get { return Instance._musicEnabled; } }
        public static PDataUnit<bool> VibrationEnabledData { get { return Instance._vibrationEnabled; } }

        protected override void Init()
        {
            base.Init();

            _soundEnabled = _soundEnabled == null ? new PDataUnit<bool>(true) : _soundEnabled;
            _musicEnabled = _musicEnabled == null ? new PDataUnit<bool>(true) : _musicEnabled;
            _vibrationEnabled = _vibrationEnabled == null ? new PDataUnit<bool>(true) : _vibrationEnabled;
        }
    }
}