using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class InitializationConfig : SingletonScriptableObject<InitializationConfig>
    {
        [SerializeField] private int initCoin; public static int InitCoin { get { return Instance.initCoin; } }
        [SerializeField] private int initGem; public static int InitGem { get { return Instance.initGem; } }

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
