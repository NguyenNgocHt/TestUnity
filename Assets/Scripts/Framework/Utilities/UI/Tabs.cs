using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public class Tabs : CacheMonoBehaviour
    {
        List<Button> buttons;
        [SerializeField] GameObject rootContent;
        List<GameObject> contents;
        GameObject activeContent;
        private void Awake()
        {
            contents = new List<GameObject>();
            buttons = GetComponentsInChildren<Button>().ToList();
            for (int i = 0; i < rootContent.transform.childCount; i++)
            {
                contents.Add(rootContent.transform.GetChild(i).gameObject);
                contents[i].SetActive(false);
            }
            activeContent = contents[0];
            activeContent.SetActive(true);
            for (int i = 0; i < buttons.Count; i++)
            {
                int _i = i;
                buttons[i].onClick.AddListener(() =>
                {
                    activeContent.SetActive(false);
                    activeContent = contents[_i];
                    activeContent.SetActive(true);
                });
            }
        }
    }
}
