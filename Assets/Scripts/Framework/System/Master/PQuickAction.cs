#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Framework
{
    public class PQuickAction : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneTransitionHelper.Reload();
            }
        }

        [MenuItem("PFramework/Clear Data")]
        public static void ClearData()
        {
            PGameMaster.ClearData();
        }
    }
}

#endif