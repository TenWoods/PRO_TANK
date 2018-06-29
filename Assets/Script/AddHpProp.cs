﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 医疗包
/// </summary>
public class AddHpProp : MonoBehaviour {

    private Vector2 propPos;
    private Vector2 propPos1;
    private Vector2 propPos2;
   

    [SerializeField][Header("【道具增加的生命值】")]
    private int addHp;

    //道具悬浮效果
    public void Fall()
    {
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => propPos, x => propPos = x, propPos2, 1f));
        s.Insert(0.6f, DOTween.To(() => propPos, x => propPos = x, propPos1, 1f));
        s.SetLoops(-1);
    }

    //道具的使用
    public void Use(Tank g ,TankData p)
    {
        g.CurrentHP += addHp;
        if (g.CurrentHP >= p.HP)
        {
            g.CurrentHP = p.HP;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tank")
        {
            Use(collision.gameObject.GetComponent<Tank>(),collision.gameObject.GetComponent<TankData>());
            Destroy(this.gameObject);
        }
    }



    // Use this for initialization
    void Start () {
        propPos = transform.position;//初位置
        propPos1 = transform.position;//记录初位置
        propPos2 = propPos + new Vector2(0, 0.2f);//上移的位置
        Fall();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = propPos;
    }
}
