using UnityEngine;

namespace Framework
{
    public static class PopupHelper
    {
        public static Transform PopupRoot;

        public static T Create<T>(GameObject prefab) where T : PopupBehaviour
        {
            if (PopupRoot == null)
                PopupRoot = GameObject.FindObjectOfType<PopupRootSetter>().transform;

            T popup = prefab.Create(PopupRoot, false).GetComponent<T>();
            popup.transform.SetAsLastSibling();

            return popup;
        }

        public static PopupBehaviour Create(GameObject prefab)
        {
            return Create<PopupBehaviour>(prefab);
        }

        public static PopupConfirm CreateConfirm(GameObject prefab, string header, string content, Callback<bool> onConfirm)
        {
            PopupConfirm popup = Create<PopupConfirm>(prefab);
            popup.Construct(header, content, onConfirm);

            return popup;
        }

        public static PopupMessage CreateMessage(GameObject prefab, string msg)
        {
            PopupMessage popup = Create<PopupMessage>(prefab);
            popup.Construct(msg);

            return popup;
        }
    }
}