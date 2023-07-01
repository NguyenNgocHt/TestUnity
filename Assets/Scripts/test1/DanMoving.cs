using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DanMoving : MonoBehaviour
{
    public GameObject spaceShip;
    float moveSpeed = 15f;
    private GameObject collisionGameObiject;
    private GameObject parentCollisionGameObiject;
    private setGameObjMoving collisionGameObiject_cs;
    private bool hasPassedB = false;
    private Vector3 posGameObjStartMoving;
    private Vector3 posLimitTop = new Vector3(0, 1560, 0);
    private Vector3 posLimitBottom = new Vector3(0, 0, 0);
    private Vector3 posLimitLeft = new Vector3(0, 0, 0);
    private Vector3 posLimitRight = new Vector3(720, 0, 0);


    // Start is called before the first frame update
    void Start()
    { 
        posGameObjStartMoving = transform.position;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ThienThach"))
        {
            Color greenColor = Color.green;
            collisionGameObiject = collision.gameObject;
            collisionGameObiject_cs = collisionGameObiject.transform.GetComponent<setGameObjMoving>();
            SpriteRenderer spriteGameObiject = collisionGameObiject.transform.GetComponent<SpriteRenderer>();
            spriteGameObiject.color = greenColor;
            collisionGameObiject_cs.healthUpdate -=  500;
            collisionGameObiject_cs.healthBarBg.enabled = true;
            collisionGameObiject_cs.healthBar.enabled = true;
            collisionGameObiject_cs.valueText.enabled = true;
            collisionGameObiject_cs.UpdateBar(collisionGameObiject_cs.healthUpdate, collisionGameObiject_cs.realityHP_Meteorite);
            if(collisionGameObiject_cs.healthUpdate <= 0)
            {
                collisionGameObiject.transform.GetComponent<CircleCollider2D>().enabled = false;
                collisionGameObiject_cs.healthBarBg.enabled = false;
                collisionGameObiject_cs.healthBar.enabled = false;
                collisionGameObiject_cs.valueText.enabled = false;
                collisionGameObiject_cs.Boom = true;
                Invoke("SetPosGameObject", 1f);
               
            }  
        }
    }
private void SetPosGameObject()
    { 
        collisionGameObiject_cs.setPositionGameObject(collisionGameObiject);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ThienThach")) 
        {
            collisionGameObiject = collision.gameObject;
            setGameObjMoving collisionGameObiject_cs = collisionGameObiject.transform.GetComponent<setGameObjMoving>();
            collisionGameObiject_cs.healthBarBg.enabled = false;
            collisionGameObiject_cs.healthBar.enabled = false;
            collisionGameObiject_cs.valueText.enabled = false;
            transform.position = posGameObjStartMoving;
            transform.gameObject.SetActive(false);
            this.SawpToRedColor();
        }  
    }
    private void SawpToRedColor()
    {
        Debug.Log("doi mau game obiject");
        Color redColor = Color.red;
        SpriteRenderer spriteGameObiject = collisionGameObiject.transform.GetComponent<SpriteRenderer>();
        spriteGameObiject.color = redColor;
    }
    // Update is called once per frame
    void Update()
    {
        CheckLimitPosGameObj();
    }
   private void CheckLimitPosGameObj()
   {
        Vector3 ScreenPosGameObj = Camera.main.WorldToScreenPoint(transform.position);
        if(ScreenPosGameObj.x < 0)
        {
            transform.position = posGameObjStartMoving;
            transform.gameObject.SetActive(false);
        }
        else if(ScreenPosGameObj.x > 720)
        {
            transform.position = posGameObjStartMoving;
            transform.gameObject.SetActive(false);
        }
        else if(ScreenPosGameObj.y < 0)
        {
            transform.position = posGameObjStartMoving;
            transform.gameObject.SetActive(false);
        }
        else if(ScreenPosGameObj.y > 1560)
        {
            transform.position = posGameObjStartMoving;
            transform.gameObject.SetActive(false);
        }
   }
  
}   
