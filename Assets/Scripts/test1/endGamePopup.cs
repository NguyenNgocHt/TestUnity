using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public Button buttonBackHome;
    private string sceneHomeName = "homeScreen";
    // Start is called before the first frame update
    void Start()
    {
        buttonBackHome.onClick.AddListener(BackHome);
    }
    
    private void BackHome()
    {
        SceneManager.LoadScene(sceneHomeName, LoadSceneMode.Single);
    }
}
