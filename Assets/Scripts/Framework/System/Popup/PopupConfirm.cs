using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public class PopupConfirm : PopupBehaviour
    {
        [Header("Reference")]
        [SerializeField] TextMeshProUGUI _txtHeader;
        [SerializeField] TextMeshProUGUI _txtContent;
        [SerializeField] Button _btnYes;
        [SerializeField] Button _btnNo;

        public event Callback<bool> OnConfirm;

        protected override void Awake()
        {
            base.Awake();

            _btnNo.onClick.AddListener(ButtonClickNoCallback);
            _btnYes.onClick.AddListener(ButtonClickYesCallback);
        }

        public void Construct(string header, string content, Callback<bool> onComfirm)
        {
            _txtHeader.text = header;
            _txtContent.text = content;

            OnConfirm += onComfirm;
        }

        void ButtonClickNoCallback()
        {
            OnConfirm?.Invoke(false);

            InternalClose();
        }

        void ButtonClickYesCallback()
        {
            OnConfirm?.Invoke(true);

            InternalClose();
        }

        protected override void HandleClose()
        {
            OnConfirm?.Invoke(false);
        }
    }
}
