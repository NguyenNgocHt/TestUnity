using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public  void LoadMainGame()
    {
        LoadScene("test1_shootmeteors");
    }
    public void LoadHome()
    {
        LoadScene("homeScreen");
    }
    public void LoadPlayGame()
    {
        LoadScene("playScreen");
    }
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
