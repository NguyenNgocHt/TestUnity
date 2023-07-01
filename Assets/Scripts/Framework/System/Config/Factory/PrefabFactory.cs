using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    [Serializable]
    public class PrefabFactory : SingletonScriptableObject<PrefabFactory>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            if (_instance == null)
            {
                Instance.ToString();
            }
        }
        #region PrimitiveAsset
        [SerializeField] private GameObject textPrefab; public static GameObject TextPrefab { get { return Instance.textPrefab; } }
        [SerializeField] private GameObject audioSourcePrefab; public static GameObject AudioSourcePrefab { get { return Instance.audioSourcePrefab; } }
        #endregion

    }
}

