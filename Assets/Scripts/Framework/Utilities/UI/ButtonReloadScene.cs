namespace Framework
{
    public class ButtonReloadScene : ButtonBase
    {
        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();

            SceneTransitionHelper.Reload();
        }
    }
}