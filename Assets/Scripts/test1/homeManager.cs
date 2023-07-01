using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class homeManager : MonoBehaviour
{
    private Button myButton_goPlayScreen;
    private string playSceneName = "playScreen";
    // Start is called before the first frame update
    void Start()
    {
        myButton_goPlayScreen = GetComponent<Button>();
        myButton_goPlayScreen.onClick.AddListener(onClickButton_movePlayScreen);
    }
   private void onClickButton_movePlayScreen()
    {
        SceneManager.LoadScene(playSceneName, LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
