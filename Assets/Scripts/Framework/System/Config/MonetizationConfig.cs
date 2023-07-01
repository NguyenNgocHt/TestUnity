using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class MonetizationConfig : SingletonScriptableObject<MonetizationConfig>
    {
        [SerializeField] private string bannerAdsIdAndroid; public static string BannerAdsIdAndroid { get { return Instance.bannerAdsIdAndroid; } }
        [SerializeField] private string bannerAdsIdIOS; public static string BannerAdsIdIOS { get { return Instance.bannerAdsIdIOS; } }
        [SerializeField] private string interstialAdsIdAndroid; public static string IntertialAdsIdAndroid { get { return Instance.interstialAdsIdAndroid; } }
        [SerializeField] private string interstialAdsIdIOS; public static string IntertialAdsIdIOS { get { return Instance.interstialAdsIdIOS; } }
        [SerializeField] private string rewardAdsIdAndroid; public static string RewardAdsIdAndroid { get { return Instance.rewardAdsIdAndroid; } }
        [SerializeField] private string rewardAdsIdIOS; public static string RewardAdsIdIOS { get { return Instance.rewardAdsIdIOS; } }

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
