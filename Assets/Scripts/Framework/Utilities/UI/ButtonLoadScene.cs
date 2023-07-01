using UnityEngine;

namespace Framework
{
    public class ButtonLoadScene : ButtonBase
    {
        [SerializeField] ESceneName eSceneValue;

        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();

            SceneTransitionHelper.Load(eSceneValue);
        }
    }
}