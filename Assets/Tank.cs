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
    public int CurrentBigBullet { get; set; }
    bool isAlive = true;

    void Start()
    {
        tankData = gameObject.GetComponent<TankData>();
        CurrentHP = tankData.HP;
        CurrentLife = tankData.Life;
        CurrentBigBullet = 1;
	}

    //复活计时器
    float reviveTimer = 0;

    //
    float bigBulletTimer = 0;
    //TODO复活点
	void Update()
    {
        TankMove();
        FireRot(rotPos,tankPos);
        Fire();
        if(CurrentBigBullet < tankData.BigBullet)
        {
            bigBulletTimer += Time.deltaTime;
            if(bigBulletTimer >= 10)
            {
                CurrentBigBullet++;
                bigBulletTimer = 0;
            }
        }
        if (CurrentHP <= 0)
        {
            Destroy();
            isAlive = false;
        }
        if(!isAlive)
        {
            reviveTimer += Time.deltaTime;
            if(reviveTimer >= 5f)
            {
                isAlive = true;
                transform.position = new Vector3(0,0,0);//复活点
                CurrentHP = tankData.HP;
                gameObject.SetActive(true);
            }
        }
        if (Input.GetKey("u")) Upgrade();
        if (Input.GetKey("i")) Downgrade();
	}

    void TankMove()
    {
        float rot, speed;
        if (tankData.PlayerNO == Player.Player1)
        {
            rot = Input.GetAxis("Horizontal1") * tankData.RotSpeed * Time.deltaTime;
            speed = Input.GetAxis("Vertical1") * tankData.Speed * Time.deltaTime;
        }
        else
        {
            rot = Input.GetAxis("Horizontal2") * tankData.RotSpeed * Time.deltaTime;
            speed = Input.GetAxis("Vertical2") * tankData.Speed * Time.deltaTime;
        }
        transform.Translate(Vector3.up * speed);
        transform.RotateAround(transform.position, new Vector3(0, 0, -1), rot);
    }

    //炮管的旋转
    //rotposition  旋转点
    //rotPos  炮管
    void FireRot(Transform rotPosition, Transform rotPos)
    {
        if (tankData.PlayerNO == Player.Player1)
        {
            rotPos.RotateAround(rotPosition.position, new Vector3(0, 0, -1), Input.GetAxis("Rotation1") * tankData.RotSpeed * Time.deltaTime);
        }
        else
        {
            rotPos.RotateAround(rotPosition.position, new Vector3(0, 0, -1), Input.GetAxis("Rotation2") * tankData.RotSpeed * Time.deltaTime);
            
        }
            
    }

    //开火计时器
    float fireTimer = 0;
    void Fire()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= tankData.FireRate)
        {
            if (tankData.PlayerNO == Player.Player1)
            {
                if (Input.GetAxis("Fire11") > 0 || (Input.GetAxis("Fire12") > 0 && CurrentBigBullet > 0))
                {
                    fireTimer = 0;
                    GameObject bullet;
                    if (tankData.Type == TankType.JuniorTank)
                    {
                        bullet = Resources.Load<GameObject>("Player1Bullet");
                    }
                    else
                    {
                        bullet = Resources.Load<GameObject>("Player1MaxBullet");
                    }
                    bullet.GetComponent<Bullet>().PlayerNO = tankData.PlayerNO;
                    bullet = Instantiate(bullet, firePos.position, firePos.rotation);
                    if (Input.GetAxis("Fire12") > 0)
                    {
                        bullet.transform.localScale = new Vector3(2, 2, 2);
                        bullet.GetComponent<Bullet>().IsMax = true;
                        CurrentBigBullet--;
                    }
                    //GameObject fire = Resources.Load<GameObject>("Fire");
                    //Instantiate(fire, firePos.position, firePos.rotation);
                    //Destroy(fire, 2f); 
                }
            }
            else
            {
                if (Input.GetAxis("Fire21") > 0 || (Input.GetAxis("Fire22") > 0 && CurrentBigBullet > 0))
                {
                    fireTimer = 0;
                    GameObject bullet;
                    if (tankData.Type == TankType.JuniorTank)
                    {
                        bullet = Resources.Load<GameObject>("Player2Bullet");
                    }
                    else
                    {
                        bullet = Resources.Load<GameObject>("Player2MaxBullet");
                    }
                    bullet.GetComponent<Bullet>().PlayerNO = tankData.PlayerNO;
                    bullet = Instantiate(bullet, firePos.position, firePos.rotation);
                    if (Input.GetAxis("Fire22") > 0)
                    {
                        bullet.transform.localScale = new Vector3(2, 2, 2);
                        bullet.GetComponent<Bullet>().IsMax = true;
                        CurrentBigBullet--;
                    }
                    //GameObject fire = Resources.Load<GameObject>("Fire");
                    //Instantiate(fire, firePos.position, firePos.rotation);
                    //Destroy(fire, 2f); 
                }
            }                    
        }
    }

    void Destroy()
    {
        //GameObject boom = Resources.Load<GameObject>("TankBoom");
        //Instantiate(boom,transform.position,transform.rotation);
        //Destroy(boom, 2);
        tankData.Life -= 1;
        gameObject.SetActive(false);
        isAlive = false;
        if (tankData.Life <= 0)
        {
            GameManager.instance.GameOver();
        }        
    }

    //升级
    public void Upgrade()
    {
        if(tankData.PlayerNO == Player.Player1)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player1Max");
            tankPos.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player1MaxTank");
        }
        else if(tankData.PlayerNO == Player.Player2)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player2Max");
            tankPos.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player2Max");
        }
        tankData.Type = TankType.SeniorTank;
        CurrentHP = tankData.HP;
        tankData.SeniorTankData();       
    }

    //降级
    public void Downgrade()
    {
        if (tankData.PlayerNO == Player.Player1)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player1");
            tankPos.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player1Tank");

        }
        else if (tankData.PlayerNO == Player.Player2)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player2");
            tankPos.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Player2Tank");
        }
        tankData.Type = TankType.JuniorTank;
        if (CurrentHP > tankData.HP)
        {
            CurrentHP = tankData.HP;
        }
        tankData.JuniorTankData();
    }

}
