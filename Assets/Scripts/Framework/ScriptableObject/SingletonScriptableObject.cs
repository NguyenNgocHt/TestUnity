﻿using UnityEditor;
using UnityEngine;

namespace Framework
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static readonly string SOSFolderName = "Config";

        static protected T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<T>(string.Format("{0}/{1}", SOSFolderName, typeof(T).Name));

#if UNITY_EDITOR
                    if (_instance == null)
                    {
                        string configPath = string.Format("Assets/Resources/{0}/", SOSFolderName);
                        if (!System.IO.Directory.Exists(configPath))
                            System.IO.Directory.CreateDirectory(configPath);

                        _instance = ScriptableObjectHelper.CreateAsset<T>(configPath, typeof(T).Name.ToString());
                    }
                    else
                    {
                        ScriptableObjectHelper.SaveAsset(_instance);
                    }
#endif
                }

                return _instance;
            }
        }
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            if (_instance == null)
            {
                Instance.ToString();
            }
        }
    }

}