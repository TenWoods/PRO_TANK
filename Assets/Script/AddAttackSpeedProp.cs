using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AddAttackSpeedProp : MonoBehaviour {

    private Vector2 propPos;
    private Vector2 propPos1;
    private Vector2 propPos2;

    [SerializeField][Header("【增加攻速的系数】")][Range(1,2)]
    private float addAttackSpeed;

    [SerializeField]
    [Header("【buff的持续时间】")]
    private float buffTime;

    //道具悬浮效果
    public void Fall()
    {
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => propPos, x => propPos = x, propPos2, 1f));
        s.Insert(0.6f, DOTween.To(() => propPos, x => propPos = x, propPos1, 1f));
        s.SetLoops(-1);
    }

    //道具的使用
    public void Use(TankData g)
    {
        float speed = g.FireRate;
        g.FireRate = g.FireRate - (addAttackSpeed-1)* g.FireRate;
        StartCoroutine(ReturnSpeed(g, speed));
    }
    IEnumerator ReturnSpeed(TankData g, float earlySpeed)
    {
        yield return new WaitForSeconds(buffTime);
        g.FireRate = earlySpeed;
        yield return 0;
    }
    


    //碰撞检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tank")
        {
            Use( collision.gameObject.GetComponent<TankData>());
            GetComponent<SpriteRenderer>().sprite = null;
            GetComponent<Collider2D>().enabled = false;
            Destroy(this.gameObject,buffTime+0.5f);
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
