using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class spaceShipControler : MonoBehaviour
{
    public GameObject audioSound;
    public GameObject radar;
    public GameObject effectBoom;
    public float spaceShipSpeed;
    private float pixelX = 0f;
    private float pixelY = 0f;
    private float gameWidth = 720f;
    private float gameHeight = 1560f;
    private float spaceShipWidth = 0f;
    private float spaceShipHeight = 0f;
    private Camera mainCamera;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bullet;
    public GameObject boxBullets1;
    public GameObject boxBullets2;
    public GameObject boxBullets3;
    private bool mouseDownStatus;
    public int numberAmmo = 50;
    private List<GameObject> bullets1;
    private List<GameObject> bullets2;
    private List<GameObject> bullets3;
    private int coutBullet = 0;
    private int countRocket = 0;
    private float bulletSpeed = 25f;
    public bool end_Game = false;
    public GameObject rocket1;
    public GameObject rocket2;
    public GameObject rocketPrefab;
    private List<GameObject> rockets1;
    private List<GameObject> rockets2;
    public GameObject boxRocket1;
    public GameObject boxRocket2;
    public float radarRadius = 5f;
    private LineRenderer lineRenderer;
    private PolygonCollider2D spaceShipCollider;
    private List<GameObject> colliderGameObj_radar;
    private GameObject colliderGameObjRadar;
    private bool isRaderCollider = false;
    private float rocketSpeed = 15f;
    private AudioPlay audioPlay_cs;
    // Start is called before the first frame update
    void Start()
    {
        audioPlay_cs = audioSound.transform.GetComponent<AudioPlay>();
        spaceShipCollider = transform.GetComponent<PolygonCollider2D>();
        mainCamera = Camera.main;
        RectTransform rect_transfrom = GetComponent<RectTransform>();
        spaceShipWidth = rect_transfrom.rect.width;
        spaceShipHeight = rect_transfrom.rect.height;
        initBullets();
        initRocket();
        effectBoom.SetActive(false);
        InitRadar();
    }
    private void initBullets()
    {
        bullets1 = new List<GameObject>();
        bullets2 = new List<GameObject>();
        bullets3 = new List<GameObject>();
        for (int i = 0; i < numberAmmo; i++)
        {
            GameObject newBullet1 = Instantiate(bullet, bullet1.transform.position, Quaternion.identity);
            GameObject newBullet2 = Instantiate(bullet, bullet2.transform.position, Quaternion.identity);
            GameObject newBullet3 = Instantiate(bullet, bullet3.transform.position, Quaternion.identity);
            newBullet1.SetActive(false);
            newBullet2.SetActive(false);
            newBullet3.SetActive(false);
            bullets1.Add(newBullet1);
            bullets2.Add(newBullet2);
            bullets3.Add(newBullet3);
        }
        for (int i = 0; i < numberAmmo; i++)
        {
            bullets1[i].transform.parent = boxBullets1.transform;
            bullets2[i].transform.parent = boxBullets2.transform;
            bullets3[i].transform.parent = boxBullets3.transform;
        }
    }
    private void initRocket()
    {
        rockets1 = new List<GameObject>();
        rockets2 = new List<GameObject>();
        for (int i = 0; i < numberAmmo; i++)
        {
            GameObject newRocket1 = Instantiate(rocketPrefab, rocket1.transform.position, Quaternion.identity);
            GameObject newRocket2 = Instantiate(rocketPrefab, rocket2.transform.position, Quaternion.identity);
            newRocket1.SetActive(false);
            newRocket2.SetActive(false);
            rockets1.Add(newRocket1);
            rockets2.Add(newRocket2);
        }
        for (int i = 0; i < numberAmmo; i++)
        {
            rockets1[i].transform.parent = boxRocket1.transform;
            rockets2[i].transform.parent = boxRocket2.transform;
        }
    }
    private void InitRadar()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void DrawRadarBorder()
    {
        int numPoints = 360;
        lineRenderer.positionCount = numPoints;

        // Tính toán các điểm trên đường viền radar
        for (int i = 0; i < numPoints; i++)
        {
            float angle = i * (360f / numPoints);
            float x = transform.position.x + Mathf.Sin(Mathf.Deg2Rad * angle) * radarRadius;
            float y = transform.position.y + Mathf.Cos(Mathf.Deg2Rad * angle) * radarRadius;
            Vector3 point = new Vector3(x, y, transform.position.z);
            lineRenderer.SetPosition(i, point);
        }
    }
    public void GetColliderGameObj_radar(GameObject other)
    {
        colliderGameObjRadar = other;
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRadarBorder();
        Vector3 worldPosition = transform.position;
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        this.Update_spaceShipMoving(screenPosition);
        this.Update_rotateSpaceShip();
        this.Update_bullet();
        this.Update_rocket();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer spriteGameobject = transform.GetComponent<SpriteRenderer>();
        if (collision.CompareTag("ThienThach"))
        {
            Debug.Log("vao collider spaceShip");
            spriteGameobject.sprite = null;
            effectBoom.SetActive(true);
            Invoke("SetEndGame", 1f);
        }
    }
    private void SetEndGame()
    {
        Debug.Log("END GAME");
        end_Game = true;
    }
    private void Update_spaceShipMoving(Vector3 screenPos)
    {
        float horizotalInput = 0f;
        float verticalInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            if (screenPos.x >= 0 + spaceShipWidth / 2)
            {
                horizotalInput = -1f;
            }
            else
            {
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPos);
                transform.position = worldPosition;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (screenPos.x <= gameWidth - spaceShipWidth / 2)
            {
                horizotalInput = 1f;
            }
            else
            {
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPos);
                transform.position = worldPosition;
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (screenPos.y >= 0 + spaceShipHeight)
            {
                verticalInput = -1f;
            }
            else
            {
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPos);
                Debug.Log(" worldPosition" + worldPosition);
                transform.position = worldPosition;
            }
        }
        Vector3 movement = new Vector3(horizotalInput, verticalInput, 0f) * spaceShipSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
    private void Update_rotateSpaceShip()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, worldMousePosition - transform.position);
    }

    private void Update_bullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            coutBullet += 1;
            for (int i = 0; i < bullets1.Count; i++)
            {
                if (i == coutBullet - 1)
                {
                    audioPlay_cs.effect_shotting();
                    bullets1[i].SetActive(true);
                    bullets1[i].transform.position = bullet1.transform.position;
                    bullets1[i].transform.GetComponent<DanMoving>().spaceShip = transform.gameObject;
                    Rigidbody2D rigidbodyBullet1 = bullets1[i].transform.GetComponent<Rigidbody2D>();
                    rigidbodyBullet1.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
                    bullets2[i].SetActive(true);
                    bullets2[i].transform.position = bullet2.transform.position;
                    bullets2[i].transform.GetComponent<DanMoving>().spaceShip = transform.gameObject;
                    Rigidbody2D rigidbodyBullet2 = bullets2[i].transform.GetComponent<Rigidbody2D>();
                    rigidbodyBullet2.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
                    bullets3[i].SetActive(true);
                    bullets3[i].transform.position = bullet3.transform.position;
                    bullets3[i].transform.GetComponent<DanMoving>().spaceShip = transform.gameObject;
                    Rigidbody2D rigidbodyBullet3 = bullets3[i].transform.GetComponent<Rigidbody2D>();
                    rigidbodyBullet3.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
                }
                if (coutBullet == numberAmmo - 1)
                {
                    coutBullet = 0;
                }
            }
        }
    }
    private void Update_rocket()
    {
        RadarManager radarManager_cs = radar.transform.GetComponent<RadarManager>();
        if (radarManager_cs.isRocketCollider)
        {
            countRocket += 1;
            for (int i = 0; i < rockets1.Count; i++)
            {
                if (i == countRocket - 1)
                {
                    audioPlay_cs.effect_rocket();
                    rockets1[i].SetActive(true);
                    rockets1[i].transform.Find("Rocket").position = rocket1.transform.position;
                    rockets1[i].transform.Find("Rocket").GetComponent<RocketMoving>().taget = colliderGameObjRadar;
                    rockets1[i].transform.Find("Rocket").GetComponent<RocketMoving>().isMoving = true;
                    rockets2[i].SetActive(true);
                    rockets2[i].transform.Find("Rocket").position = rocket2.transform.position;
                    rockets2[i].transform.Find("Rocket").GetComponent<RocketMoving>().taget = colliderGameObjRadar;
                    rockets2[i].transform.Find("Rocket").GetComponent<RocketMoving>().isMoving = true;
                    radarManager_cs.isRocketCollider = false;
                }
                if(countRocket == numberAmmo)
                {
                    countRocket = 0;
                }
            }
        }
    }
}