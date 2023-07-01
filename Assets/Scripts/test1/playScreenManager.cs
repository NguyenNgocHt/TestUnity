using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class playScreenManager : MonoBehaviour
{ 
    private string sceneHomeName = "homeScreen";
    public GameObject endGamePopup;
    public TextMeshProUGUI showTimer;
    public Canvas canvasMain;
    public GameObject spaceShip;
    private GameObject meteorite;
    private Vector2 posSpaceShip;
    private float timeEndGame;
    private List<float> playerScore;
    float timeGameStart;
    float timeCureent = 0f;
    Transform textTimer;
    Transform textRecord;
    private float startTime;
    private float currentTime;
    private const string  PlayerScoreKey = "PlayerScore";
    float recordValue;
    // Start is called before the first frame update
    void Start()
    {
        this.ResetTime();
        if (!PlayerPrefs.HasKey(PlayerScoreKey))
        {
            playerScore = new List<float>();
            Debug.Log("init playerPrefs");
            playerScore.Add(0f);
            string floatArrToString = FloatArrayToString(playerScore.ToArray());
            PlayerPrefs.SetString(PlayerScoreKey, floatArrToString);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (spaceShip)
        {
            spaceShipControler spaceShipControler_cs = spaceShip.transform.GetComponent<spaceShipControler>();
            currentTime = (int)(Time.time - startTime);
            if (currentTime % 1 == 0)
            {
                showTimer.text = currentTime.ToString();
            }
            if (spaceShipControler_cs.end_Game)
            {
                playerScore = new List<float>();
                Vector3 posEndGamePopup = new Vector3(0, 0, 0);
                GameObject newPopupEndGame = Instantiate(endGamePopup, posEndGamePopup, Quaternion.identity);
                timeEndGame = currentTime;
                textTimer = newPopupEndGame.transform.Find("Canvas/Image/Text");
                textRecord = newPopupEndGame.transform.Find("Canvas/record/Text");
                textTimer.GetComponent<TextMeshProUGUI>().text = timeEndGame.ToString();
                if (PlayerPrefs.HasKey(PlayerScoreKey))
                {
                    Debug.Log("set playerPrefs");
                    string floatArrayString = PlayerPrefs.GetString("PlayerScore");
                    float[] loadedFloatArray = StringToFloatArray(floatArrayString);
                    playerScore = loadedFloatArray.ToList();
                    playerScore.Add(timeEndGame);
                    string floatArrToString = FloatArrayToString(playerScore.ToArray());
                    PlayerPrefs.SetString("PlayerScore", floatArrToString);
                    float maxRecor = 0f;
                    for(int i  = 0; i < playerScore.Count; i++)
                    {
                        Debug.Log(playerScore[i]);
                        if(playerScore[i] >= maxRecor)
                        {
                            maxRecor = playerScore[i];
                        }
                    }
                    recordValue = maxRecor;
                }
                textRecord.GetComponent<TextMeshProUGUI>().text = recordValue.ToString();
                spaceShipControler_cs.end_Game = false;
                Destroy(spaceShip);
            }
        }   
    }
    float[] StringToFloatArray(string arrayString)
    {
        string[] stringArray = arrayString.Split(',');
        float[] floatArray = new float[stringArray.Length];

        for (int i = 0; i < stringArray.Length; i++)
        {
            floatArray[i] = float.Parse(stringArray[i]);
        }

        return floatArray;
    }
    string FloatArrayToString(float[] array)
    {
        string arrayString = string.Join(",", array);
        return arrayString;
    }
    public  void ResetTime()
    {
        startTime = (int)Time.time;
        currentTime = 0;
    }
}
