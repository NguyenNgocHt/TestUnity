using Framework;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public abstract class CardBase<T> : CacheMonoBehaviour where T : struct
    {
        public Button Button;
        public virtual void BuildUI(T info)
        {
            var infoFields = info.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            var cardFields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < cardFields.Length; i++)
            {
                cardFields[i].SetValue(this, infoFields.GetValue(i));
                Debug.Log(cardFields[i].Name + "_" + cardFields[i].GetValue(this));
            }
            if (Button)
                Button.onClick.AddListener(() => OnClicked(info));
        }
        protected abstract void OnClicked(T info);
    }
}
