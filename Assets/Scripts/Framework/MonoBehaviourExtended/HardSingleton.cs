namespace Framework
{
    /// <summary>
    /// Also a singleton, but won't be destroyed when new scene loaded
    /// </summary>
    public class HardSingleton<T> : Singleton<T> where T : CacheMonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(CacheGameObject);
        }

        protected override void OnDestroy()
        {

        }
    }
}