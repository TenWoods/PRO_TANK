using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

    private TankData tankData;

    [Header("炮管旋转点")]
    public Transform rotPos;
    [Header("炮管位置")]
    public Transform tankPos;
    [Header("开火点")]
    public Transform firePos;

	void Start()
    {
        tankData = gameObject.GetComponent<TankData>();
	}

	void Update()
    {
        TankMove();
        FireRot(rotPos,tankPos);
	}

    void TankMove()
    {
        float rot = Input.GetAxis("Horizontal") * tankData.RotSpeed * Time.deltaTime;
        float speed = Input.GetAxis("Vertical") * tankData.Speed * Time.deltaTime;
        transform.Translate(Vector3.up * speed);
        transform.RotateAround(transform.position, new Vector3(0, 0, -1), rot);
    }

    //炮管的旋转
    //rotposition  旋转点
    //rotPos  炮管
    void FireRot(Transform rotPosition, Transform rotPos)
    {
        
        if (Input.GetKey("e"))
            rotPos.RotateAround(rotPosition.position, new Vector3(0, 0, -1), tankData.RotSpeed * Time.deltaTime);
        else if (Input.GetKey("q"))
            rotPos.RotateAround(rotPosition.position, new Vector3(0, 0, 1), tankData.RotSpeed * Time.deltaTime);
    }

    float timer = 0;
    void Fire()
    {
        timer += Time.deltaTime;
        if (Input.GetKey("r") && timer >= tankData.FireRate)
        {
            //GameObject bullet = Resources.Load<GameObject>(gameObject.name + "Bullet");
            //GameObject fire = Resources.Load<GameObject>("Fire");
            //Destroy(fire, 2f);
            
        }
    }

}
