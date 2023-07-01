using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scene_manager : MonoBehaviour
{
    public GameObject sceneManager;
    private SceneLoader sceneLoader;
    private Button myButton;
    // Start is called before the first frame update
    private void Awake()
    {
        sceneLoader = sceneManager.GetComponent<SceneLoader>();
    }
    private void Start()
    {
        myButton = GameObject.Find("Button").GetComponent<Button>();
        myButton.onClick.AddListener(OnClickGoHome);
    }
    public void OnClickGoHome()
    {
        if (sceneLoader)
        {
            sceneLoader.LoadHome();
        } 
    }
}
