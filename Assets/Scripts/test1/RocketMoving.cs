using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMoving : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject taget;
    public bool isMoving = false;
    public float rocketSpeed = 5f;
    private GameObject colliderGameObj;
    private setGameObjMoving setGameObjMoving_cs;
    
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ThienThach"))
        {
            colliderGameObj = collision.transform.gameObject;
            setGameObjMoving_cs = colliderGameObj.transform.GetComponent<setGameObjMoving>();
            Color greenColer = Color.green;
            SpriteRenderer spriteGameObj = colliderGameObj.transform.GetComponent<SpriteRenderer>();
            spriteGameObj.color = greenColer;
            setGameObjMoving_cs.healthUpdate -= 1000;
            setGameObjMoving_cs.healthBarBg.enabled = true;
            setGameObjMoving_cs.healthBar.enabled = true;
            setGameObjMoving_cs.valueText.enabled = true;
            setGameObjMoving_cs.UpdateBar(setGameObjMoving_cs.healthUpdate, setGameObjMoving_cs.realityHP_Meteorite);
            if(setGameObjMoving_cs.healthUpdate <= 0)
            {
                setGameObjMoving_cs.healthBarBg.enabled = false;
                setGameObjMoving_cs.healthBar.enabled = false;
                setGameObjMoving_cs.valueText.enabled = false;
                setGameObjMoving_cs.Boom = true;
                Invoke("SetPosGameObject", 1f);
            }

        }
    }
    private void SetPosGameObject()
    {
        setGameObjMoving_cs.setPositionGameObject(colliderGameObj);
    }
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, taget.transform.position - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, taget.transform.position, rocketSpeed * Time.deltaTime);
            if(transform.position == taget.transform.position)
            {
                isMoving = false;
                transform.parent.gameObject.SetActive(false);
            }
        }
        
    }
}
