using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class initDan : MonoBehaviour
{
    public GameObject dan;
    public float spawTime;
    private float m_spawTime;
    int objectCount = 0;
    float movingUp;
    public float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 spawDanPos = transform.position;
        m_spawTime = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        this.MouseOnClick();
    }
    public void MouseOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.FireBullet();
        }
    }
    void FireBullet()
    {
        if (dan)
        {
            GameObject newDan = Instantiate(dan, transform.position, Quaternion.identity);
            newDan.transform.parent = transform;
            string nameGameObj = "Dan" + objectCount.ToString();
            newDan.name = nameGameObj;
            objectCount++;
        }
    }
}  
