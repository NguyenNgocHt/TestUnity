using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initMeteorite : MonoBehaviour
{
    public GameObject audioSound;
    public GameObject meteorite;
    private List<GameObject> objectPool;
    private setGameObjMoving gameObjectMoving_cs;
    private int poolSize = 20;
    public float spawnTime;
    float m_spawnTime;
    bool m_isGameOver;
    float screenWidth = 720f;
    float screenHeight = 1560f;
    Vector3 limitPosX1;
    Vector3 limitPosX2;
    Vector3 limitPosY1;
    Vector3 limitPosY2;
    private Camera cameraMain;
    int objectCount = 0;
    int m_score;
    // Start is called before the first frame update
    void Start()
    {
        limitPosX1 = new Vector3(0, 0, 0);
        limitPosX2 = new Vector3(720, 0, 0);
        limitPosY1 = new Vector3(0, 1560, 0);
        limitPosY2 = new Vector3(0, 3960, 0);
        m_spawnTime = 1;
        cameraMain = Camera.main;
        InitObjectPool();
    }
    private void InitObjectPool()
    {
        Vector3 screenLimitPosX1 = cameraMain.ScreenToWorldPoint(limitPosX1);
        Vector3 screenLimitPosX2 = cameraMain.ScreenToWorldPoint(limitPosX2);
        Vector3 screenLimitPosY1 = cameraMain.ScreenToWorldPoint(limitPosY1);
        Vector3 screenLimitPosY2 = cameraMain.ScreenToWorldPoint(limitPosY2);
       
        objectPool = new List<GameObject>();
        for(int i = 0; i < poolSize; i++)
        { 
               Vector3 spawPos = new Vector3(Random.Range(screenLimitPosX1.x, screenLimitPosX2.x), Random.Range(screenLimitPosY1.y, screenLimitPosY2.y), 0);
               GameObject newObj = Instantiate(meteorite, spawPos, Quaternion.identity);
               newObj.transform.parent = transform;
               string nameGameObj = "meteorite" + (i + 1).ToString();
               string nameChildGameObject = "Meteorite" + (i + 1).ToString();
               newObj.transform.Find("Meteorite").gameObject.name = nameChildGameObject;
               newObj.name = nameGameObj;
               objectPool.Add(newObj);  
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        this.destroyMeteorite();
    }
    public void setScore(int value)
    {
        m_score = value;
    }
    public int getScore()
    {
        return m_score;
    }
    public void setScore()
    {
        m_score++;
    }
    public bool isGameOver()
    {
        return m_isGameOver;
    }
    public void setGameOverState(bool gameStatus)
    {
        m_isGameOver = gameStatus;
    }
    public void destroyMeteorite()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            Transform childForChildTransform = objectPool[i].transform.Find("Meteorite" + (i + 1).ToString());
            Vector3 worldPos = childForChildTransform.position;
            Vector3 screenPos = cameraMain.WorldToScreenPoint(worldPos);
            if (screenPos.y <= 0)
            {
                gameObjectMoving_cs = childForChildTransform.GetComponent<setGameObjMoving>();
                gameObjectMoving_cs.setPositionGameObject(childForChildTransform.gameObject);
            }
        }
    }
}
