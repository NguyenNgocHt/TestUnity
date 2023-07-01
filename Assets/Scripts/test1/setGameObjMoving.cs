using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
enum MOVINGUPDATE_STATUS
{
    NO_STATUS = 0,
    MOVING_DOWN = 1,
    MOVING_COLLIDED = 2,
}
public class setGameObjMoving : MonoBehaviour
{ 
    public bool Boom;
    public GameObject effectBoom;
    public TextMeshPro valueGameObj;
    public float angleInDegrees;
    public float fallSpeed;
    public Vector2 initialVelocity;
    public float scaleNumber;
    private Camera cameraMain;
    private int StandardHP_Meteorite = 2000;
    public int realityHP_Meteorite;
    public Image healthBarBg;
    public Image healthBar;
    public Canvas canvas_em;
    public TextMeshProUGUI valueText;
    public float fillAmount = 1f;
    public Vector3 fillOffset = new Vector3(1f, 1f, 1f);
    private MOVINGUPDATE_STATUS moving_status = MOVINGUPDATE_STATUS.MOVING_DOWN;
    private Vector3 limitCollider = new Vector3(720, 1660, 0);
    private GameObject m_colliderGameObject;
    public int healthUpdate;
    int percentHasBack = 5;
    Vector3 limitPosX1;
    Vector3 limitPosX2;
    Vector3 limitPosY1;
    Vector3 limitPosY2;
    public setGameObjMoving collidedGameObject_cs;
    private Animator animator;
    private bool isAnimatorPlay = false;
    private float colliderSpeed = 10f;
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
      
        animator = effectBoom.transform.GetComponent<Animator>();
        limitPosX1 = new Vector3(0, 0, 0);
        limitPosX2 = new Vector3(720, 0, 0);
        limitPosY1 = new Vector3(0, 1560, 0);
        limitPosY2 = new Vector3(0, 3960, 0);
        cameraMain = Camera.main;
        angleInDegrees = Random.Range(225f, 315f);
        fallSpeed = Random.Range(2f, 10f);
        scaleNumber = Random.Range(0.1f, 1f);
        float Hp = StandardHP_Meteorite * scaleNumber;
        realityHP_Meteorite = (int)Hp;
        healthUpdate = realityHP_Meteorite;
        transform.localScale = new Vector3(scaleNumber, scaleNumber, scaleNumber);
        initialVelocity = new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad)) * fallSpeed;
        healthBarBg.enabled = false;
        healthBar.enabled = false;
        valueText.enabled = false;
        effectBoom.transform.localScale = new Vector3(0, 0, 0);
        effectBoom.transform.GetComponent<Animation>().Stop();
        DisableAnimation();
        Boom = false;
    }
     public void MyCustomStart()
    {
        gameObject.transform.GetComponent<CircleCollider2D>().enabled = true;
        limitPosX1 = new Vector3(0, 0, 0);
        limitPosX2 = new Vector3(720, 0, 0);
        limitPosY1 = new Vector3(0, 1560, 0);
        limitPosY2 = new Vector3(0, 1660, 0);
        cameraMain = Camera.main;
        angleInDegrees = Random.Range(225f, 315f);
        fallSpeed = Random.Range(2f, 10f);
        scaleNumber = Random.Range(0.1f, 1f);
        float Hp = StandardHP_Meteorite * scaleNumber;
        realityHP_Meteorite = (int)Hp;
        healthUpdate = realityHP_Meteorite;
        transform.localScale = new Vector3(scaleNumber, scaleNumber, scaleNumber);
        initialVelocity = new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad)) * fallSpeed;
        healthBarBg.enabled = false;
        healthBar.enabled = false;
        valueText.enabled = false;
        effectBoom.transform.localScale = new Vector3(0, 0, 0);
        effectBoom.transform.GetComponent<Animation>().Stop();
        DisableAnimation();
        Boom = false;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (cameraMain)
        {
            Vector3 WorldLimitCollided = cameraMain.ScreenToWorldPoint(limitCollider);
            if (transform.position.y <= WorldLimitCollided.y)
            {
                if (other.CompareTag("ThienThach"))
                {
                    m_colliderGameObject = other.gameObject;
                    collidedGameObject_cs = m_colliderGameObject.transform.GetComponent<setGameObjMoving>();
                    if (transform.localScale.magnitude >= m_colliderGameObject.transform.localScale.magnitude)
                    {
                        scaleNumber += collidedGameObject_cs.scaleNumber / percentHasBack;
                        realityHP_Meteorite += collidedGameObject_cs.realityHP_Meteorite / percentHasBack;
                        healthUpdate += collidedGameObject_cs.realityHP_Meteorite / percentHasBack;
                        transform.DOScale(new Vector3(scaleNumber, scaleNumber, scaleNumber), 0.3f)
                            .SetEase(Ease.OutQuad)
                            .OnComplete(SetScaleGameObj);
                    }
                    else
                    {
                        moving_status = MOVINGUPDATE_STATUS.MOVING_COLLIDED;
                        collidedGameObject_cs.scaleNumber += scaleNumber / percentHasBack;
                        collidedGameObject_cs.realityHP_Meteorite += realityHP_Meteorite / percentHasBack;
                        collidedGameObject_cs.healthUpdate += realityHP_Meteorite / percentHasBack;
                        isMoving = true;
                        m_colliderGameObject.transform.DOScale(new Vector3(collidedGameObject_cs.scaleNumber, collidedGameObject_cs.scaleNumber, collidedGameObject_cs.scaleNumber), 0.3f)
                             .SetEase(Ease.OutQuad)
                             .OnComplete(SetScaleCollidedGameObj);
                    }
                }
            }
        }  
    }
    private void SetScaleGameObj()
    {
        transform.localScale = new Vector3(scaleNumber, scaleNumber, scaleNumber);
    }
    private void SetScaleCollidedGameObj()
    {
        m_colliderGameObject.transform.localScale = new Vector3(collidedGameObject_cs.scaleNumber, collidedGameObject_cs.scaleNumber, collidedGameObject_cs.scaleNumber);
    }
    private void destroyCurrentGameObiject()
    {
        moving_status = MOVINGUPDATE_STATUS.MOVING_DOWN;
        setPositionGameObject(gameObject);
    }
    public void UpdateBar(int currentVallue, int maxValue)
    {
        healthBar.fillAmount = (float)currentVallue / (float)maxValue;
        valueText.text = currentVallue.ToString() + "/" + maxValue.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if(moving_status == MOVINGUPDATE_STATUS.MOVING_DOWN)
        {
            Vector2 newPosition = (Vector2)transform.position + initialVelocity * Time.deltaTime;
            transform.position = newPosition;
        }
        else if(moving_status == MOVINGUPDATE_STATUS.MOVING_COLLIDED)
        {
            if (isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, m_colliderGameObject.transform.position, colliderSpeed * Time.deltaTime);
                if(transform.position == m_colliderGameObject.transform.position)
                {
                    isMoving = false;
                    this.destroyCurrentGameObiject();
                }
            }

        }
        effectBoom.transform.position = transform.position;
        if (Boom)
        {
            EnableAnimation();
            effectBoom.transform.localScale = new Vector3(scaleNumber, scaleNumber, scaleNumber);
            transform.localScale = new Vector3(0, 0, 0);
        }

          
    }
    public void setPositionGameObject(GameObject gameObj)
    {
        Color gameObjColor = Color.red;
        SpriteRenderer spriteGameObj = gameObj.transform.GetComponent<SpriteRenderer>();
        spriteGameObj.color = gameObjColor;
        setGameObjMoving GameObjectMoving_cs = gameObj.transform.GetComponent<setGameObjMoving>();
        GameObjectMoving_cs.MyCustomStart();
        Vector3 screenLimitPosX1 = cameraMain.ScreenToWorldPoint(limitPosX1);
        Vector3 screenLimitPosX2 = cameraMain.ScreenToWorldPoint(limitPosX2);
        Vector3 screenLimitPosY1 = cameraMain.ScreenToWorldPoint(limitPosY1);
        Vector3 screenLimitPosY2 = cameraMain.ScreenToWorldPoint(limitPosY2);
        gameObj.transform.position = new Vector3(Random.Range(screenLimitPosX1.x, screenLimitPosX2.x), Random.Range(screenLimitPosY1.y, screenLimitPosY2.y), 0);

    }
    public void DisableAnimation()
    {
        animator.enabled = false;
    }
    public void EnableAnimation()
    {
        animator.enabled = true;
    }
}
