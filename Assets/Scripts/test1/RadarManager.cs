using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarManager : MonoBehaviour
{
    private CircleCollider2D radar;
    public bool isRocketCollider = false;
    private spaceShipControler spaceShipControler_cs;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ThienThach"))
        {
            GameObject parentGameObj = transform.parent.gameObject;
            spaceShipControler_cs = parentGameObj.transform.GetComponent<spaceShipControler>();
            spaceShipControler_cs.GetColliderGameObj_radar(collision.gameObject);
            isRocketCollider = true;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
