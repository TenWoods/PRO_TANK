using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//复活见Destory()
//true为可复活
//
public class Tank : MonoBehaviour
{

    private TankData tankData;

    [Header("炮管旋转点")]
    public Transform rotPos;
    [Header("炮管位置")]
    public Transform tankPos;
    [Header("开火点")]
    public Transform firePos;

    public int CurrentHP { get; set; }
    public int CurrentLife { get; set; }

    void Start()
    {
        tankData = gameObject.GetComponent<TankData>();
        CurrentHP = tankData.HP;
        CurrentLife = tankData.Life;
	}

	void Update()
    {
        TankMove();
        FireRot(rotPos,tankPos);
        Fire();
        if (tankData.HP <= 0)
        {
            Destroy();
        }
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
            GameObject bullet = Resources.Load<GameObject>(gameObject.name + "Bullet");
            bullet.GetComponent<Bullet>().PlayerNO = tankData.PlayerNO;
            Instantiate(bullet, firePos.position, firePos.rotation);
            //GameObject fire = Resources.Load<GameObject>("Fire");
            //Instantiate(fire, firePos.position, firePos.rotation);
            //Destroy(fire, 2f);
            timer = 0;
        }
    }

    public bool Destroy()
    {
        //GameObject boom = Resources.Load<GameObject>("TankBoom");
        //Instantiate(boom,transform.position,transform.rotation);
        //Destroy(boom, 2);
        tankData.Life -= 1;
        Destroy(gameObject);
        if (tankData.Life > 0)
        {
            return true;
        }
        else
            return false;
    }

}
