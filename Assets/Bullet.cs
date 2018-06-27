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

    private bool isMax;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 15);
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Tank" && collision.GetComponent<TankData>().PlayerNO != PlayerNO)
        {
            collision.GetComponent<Tank>().CurrentHP -= hurt;
            //GameObject boom = Resources.Load<GameObject>("Boom");
            //Instantiate(boom, transform.position, transform.rotation);
            //Destroy(boom, 2);
            Destroy(gameObject);
        }
        if(collision.tag == "障碍物")
        {
            //GameObject boom = Resources.Load<GameObject>("Boom");
            //Instantiate(boom, transform.position, transform.rotation);
            //Destroy(boom, 2);
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
}
