using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TankType
{

    JuniorTank,
    SeniorTank,

}

public enum Player
{
    Player1,
    Player2,
}


public class TankData : MonoBehaviour {

    [SerializeField]
    [Header("玩家生命")]
    [Range(1,10)]
    private int life;

    [SerializeField]
    [Header("坦克血量")]
    [Range(0,2000)]
    private int hp;

    [SerializeField]
    [Header("坦克速度")]
    [Range(0,10)]
    private float speed;

    [SerializeField]
    [Header("坦克转速")]
    [Range(0,90)]
    private float rotSpeed;

    [SerializeField]
    [Header("炮管转度")]
    [Range(0,90)]
    private float fireRotSpeed;

    [SerializeField]
    [Header("坦克类型")]
    private TankType type;

    [SerializeField]
    [Header("坦克射速(s)")]
    [Range(0,3)]
    private float fireRate;

    [SerializeField]
    [Header("玩家")]
    private Player playerNO;

    [SerializeField]
    [Header("高强子弹个数")]
    [Range(1, 5)]
    private int bigBullet;

    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
        }
    }

    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    public float RotSpeed
    {
        get
        {
            return rotSpeed;
        }
        set
        {
            rotSpeed = value;
        }
    }

    public float FireRotSpeed
    {
        get
        {
            return fireRotSpeed;
        }
        set
        {
            fireRotSpeed = value;
        }
    }

    public TankType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    public float FireRate
    {
        get
        {
            return fireRate;
        }
        set
        {
            fireRate = value;
        }
    }

    public Player PlayerNO
    {
        get
        {
            return playerNO;
        }
        set
        {
            playerNO = value;
        }
    }

    public int BigBullet
    {
        get
        {
            return bigBullet;
        }
        set
        {
            bigBullet = value;
        }
    }

    public void JuniorTankData()
    {
        HP = 1;
        Speed = 1;
        RotSpeed = 1;
        FireRotSpeed = 1;
        FireRate = 1;
        BigBullet = 1;
    }

    public void SeniorTankData()
    {
        HP = 1;
        Speed = 1;
        RotSpeed = 1;
        FireRotSpeed = 1;
        FireRate = 1;
        BigBullet = 1;
    }

}
