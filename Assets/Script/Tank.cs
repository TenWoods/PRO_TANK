using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    //本坦克数据
    private TankData tankData;

    [Header("【坦克是否无敌的判断】")]
    public bool isInvincible=false;

    [Header("炮管旋转点")]//炮管的中心点
    public Transform rotPos;
    [Header("炮管位置")]//gameobject的位置
    public Transform tankPos;
    [Header("开火点")]//炮管口
    public Transform firePos;

    //public Transform[] revive;

    public int CurrentHP { get; set; }
    public int CurrentLife { get; set; }
    public int CurrentBigBullet { get; set; }
    public bool IsAlive { get; set; }

    void Start()
    {
        IsAlive = true;
        tankData = gameObject.GetComponent<TankData>();
        Downgrade();
        CurrentHP = tankData.HP;
        CurrentLife = tankData.Life;
        CurrentBigBullet = 1;
	}

    //超强火力计时器
    private float bigBulletTimer = 0;

    //开火计时器
    float fireTimer = 0;

    //降级计时器
    float downTimer = 0;

    //TODO复活点
    void Update()
    {
        if(IsAlive)
        {
            TankMove();
            FireRot(rotPos, tankPos);
            Fire();
            //获得大子弹
            if (CurrentBigBullet < tankData.BigBullet)
            {
                bigBulletTimer += Time.deltaTime;
                if (bigBulletTimer >= 10)
                {
                    CurrentBigBullet++;
                    bigBulletTimer = 0;
                }
            }
            //死亡
            if (CurrentHP <= 0)
            {
                Destroy();
                IsAlive = false;
            }
            if(tankData.Type == TankType.SeniorTank)
            {
                downTimer += Time.deltaTime;
                if (downTimer >= tankData.BigTime)
                {
                    downTimer = 0;
                    Downgrade();
                }

            }
        }     
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
                    GameObject fire = Resources.Load<GameObject>("Fire");
                    fire = Instantiate(fire, firePos.position, firePos.rotation);
                    Destroy(fire, 3);
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
                    GameObject fire = Resources.Load<GameObject>("Fire");
                    fire = Instantiate(fire, firePos.position, firePos.rotation);
                    Destroy(fire, 3);
                }             
            }          
        }
    }

    void Destroy()
    {
        GameObject boom = Resources.Load<GameObject>("TankBoom");
        boom = Instantiate(boom,transform.position,transform.rotation);
        Destroy(boom, 2);
        tankData.Life -= 1;
        gameObject.SetActive(false);
        IsAlive = false;
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
        tankData.JuniorTankData();
        if (CurrentHP > tankData.HP)
        {
            CurrentHP = tankData.HP;
        }
    }

}
