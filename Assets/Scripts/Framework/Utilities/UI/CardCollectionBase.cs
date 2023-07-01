using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Framework
{
    public abstract class CardCollectionBase<T> : CacheMonoBehaviour where T : struct
    {
        protected List<CardBase<T>> cards;
        [SerializeField] GameObject cardPrefab;
        [SerializeField] Transform contentRoot;
        protected virtual void BuildUI(List<T> infos)
        {
            cards = contentRoot.GetComponentsInChildren<CardBase<T>>().ToList();
            for (int i = 0; i < infos.Count; i++)
            {
                CardBase<T> card = Instantiate(cardPrefab, contentRoot.transform).GetComponent<CardBase<T>>();
                card.BuildUI(infos[i]);
                cards.Add(card);
            }
        }
    }
}