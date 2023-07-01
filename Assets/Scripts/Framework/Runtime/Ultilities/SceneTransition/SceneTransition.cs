using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

namespace Framework
{
    public enum ESceneName
    {
        Home,
        Ingame,
    }
    public class SceneTransition : CacheMonoBehaviour
    {

        enum State
        {
            // Wait for load scene command
            Idle,
            // Playing fade in animation
            FadeIn,
            // End of fade in animation, load new scene
            Loading,
            // Playing fade out animation
            FadeOut,
        }

        // Index of scene will be loaded
        ESceneName eSceneValue = ESceneName.Home;
        // Scene async
        AsyncOperation _sceneAsync;
        // State machine
        StateMachine<State> _stateMachine;
        // Tween
        Tween _tween;
        Callback _fadein;
        Callback _fadeout;

        #region MonoBehaviour

        void Start()
        {
            CacheGameObject.SetActive(false);
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        void Update()
        {
            _stateMachine.Update();
        }

        #endregion

        #region States

        void State_OnFadeInStart()
        {
            // Load scene async
            _sceneAsync = SceneManager.LoadSceneAsync(eSceneValue.ToString());
            _sceneAsync.allowSceneActivation = false;

            //Play fade in tween
            _fadein?.Invoke();

            //Wait until animation is end
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(SceneTransitionConfigSO.FadeInDuration + SceneTransitionConfigSO.LoadDuration, () =>
            {
                _stateMachine.CurrentState = State.Loading;
                _sceneAsync.allowSceneActivation = true;
            }, true);
        }

        void State_OnLoadingUpdate()
        {
            if (_sceneAsync.isDone)
            {
                _stateMachine.CurrentState = State.FadeOut;
            }
        }

        void State_OnFadeOutStart()
        {
            Debug.Log("OUT");
            //Play fade out tween
            _fadeout?.Invoke();

            //Wait until animation is end
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(SceneTransitionConfigSO.FadeOutDuration, () =>
            {
                _stateMachine.CurrentState = State.Idle;
                CacheGameObject.SetActive(false);
            }, true);
        }

        #endregion

        #region Public

        public void Load(ESceneName eSceneValue)
        {
            if (_stateMachine.CurrentState != State.Idle)
            {
                PDebug.Log("[{0}] A scene is loading, can't execute load scene command!", typeof(SceneTransition));
                return;
            }

            CacheGameObject.SetActive(true);

            this.eSceneValue = eSceneValue;
            _stateMachine.CurrentState = State.FadeIn;
        }

        public void Reload()
        {
            Load(ESceneName.Ingame);
        }

        public void Construct()
        {
            // Construct state machine
            _stateMachine = new StateMachine<State>();
            _stateMachine.AddState(State.FadeIn, State_OnFadeInStart);
            _stateMachine.AddState(State.Loading, null, State_OnLoadingUpdate);
            _stateMachine.AddState(State.FadeOut, State_OnFadeOutStart);
            _stateMachine.AddState(State.Idle);
            _stateMachine.CurrentState = State.Idle;
            _fadein += ()=> gameObject.SetChildrenRecursively<Image>((img) => { img.DOFade(1, SceneTransitionConfigSO.FadeInDuration); });
            _fadeout += ()=> GetComponent<Image>().DOFade(0,SceneTransitionConfigSO.FadeOutDuration);
        }

        #endregion
    }

    public static class SceneTransitionHelper
    {
        static SceneTransition _instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void LazyInit()
        {
            if (_instance == null)
            {
                _instance = SceneTransitionConfigSO.ObjSceneTransition.Create().GetComponent<SceneTransition>();
                _instance.Construct();

                GameObject.DontDestroyOnLoad(_instance.CacheGameObject);
            }
        }

        public static void Load(ESceneName eSceneValue)
        {
            LazyInit();
            _instance.Load(eSceneValue);
        }

        public static void Reload()
        {
            LazyInit();
            _instance.Reload();
        }
    }
}