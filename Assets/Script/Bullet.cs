using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    [Range(0,20)]
    [Header("速度")]
    private float speed;

    [SerializeField]
    [Range(50, 500)]
    [Header("伤害")]
    private int hurt;

    [SerializeField]
    [Header("玩家")]
    private Player playerNO;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, 10);
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Tank" && collision.gameObject.GetComponent<TankData>().PlayerNO != PlayerNO)
    //    {
    //        if (collision.gameObject.GetComponent<Tank>().isInvincible == false)
    //        {
    //            collision.gameObject.GetComponent<Tank>().CurrentHP -= hurt;
    //            if (IsMax)
    //            {
    //                collision.gameObject.GetComponent<Tank>().CurrentHP -= hurt;
    //            }
    //            GameObject boom = Resources.Load<GameObject>("Boom");
    //            boom = Instantiate(boom, transform.position, transform.rotation);
    //            Destroy(boom, 2);
    //            Destroy(gameObject);
    //        }

    //    }
    //    if (collision.gameObject.tag == "障碍物")
    //    {
    //        GameObject boom = Resources.Load<GameObject>("Boom");
    //        boom = Instantiate(boom, transform.position, transform.rotation);
    //        Destroy(boom, 2);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tank" && collision.gameObject.GetComponent<TankData>().PlayerNO != PlayerNO)
        {
            if (collision.gameObject.GetComponent<Tank>().isInvincible == false)
            {
                collision.gameObject.GetComponent<Tank>().CurrentHP -= hurt;
                if (IsMax)
                {
                    collision.gameObject.GetComponent<Tank>().CurrentHP -= hurt;
                }
                GameObject boom = Resources.Load<GameObject>("Boom");
                boom = Instantiate(boom, transform.position, transform.rotation);
                Destroy(boom, 2);
                Destroy(gameObject);
            }

        }
        if (collision.gameObject.tag == "Block")
        {
            GameObject boom = Resources.Load<GameObject>("Boom");
            boom = Instantiate(boom, transform.position, transform.rotation);
            Destroy(boom, 2);
            Destroy(gameObject);
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

    public bool IsMax { get; set; }

}
