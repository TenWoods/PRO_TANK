using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//处理UI及BGM
public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject[] revivePlace;
    public GameObject[] tanks;
    public GameObject[] propPlace;

    [Range(0,5)][SerializeField][Header("坦克复活时间")]
    private float reviveTime;

    [Range(0, 5)]
    [SerializeField]
    [Header("道具刷新时间")]
    private float refalshTime;

    [SerializeField ][Header("是否刷新")]
    private bool isRefalsh = false;

    private void Awake()
    {
        instance = this;
        tanks = GameObject.FindGameObjectsWithTag("Tank");
       
    }
    private void Start()
    {
        StartCoroutine(RefalshProp());
    }

    private void Update()
    {
        RefalshProp();
        foreach (GameObject tank in tanks)
        {
            if(!tank.GetComponent<Tank>().IsAlive)
            {
                Vector3 revive = revivePlace[Random.Range(0, revivePlace.Length)].transform.position;
                tank.transform.position = revive;
                StartCoroutine(Revive(tank, revive));
               
            }
        }
    }

    public void GameOver()
    {

    }

    IEnumerator Revive(GameObject tank, Vector3 revive)
    {
        yield return new WaitForSeconds(reviveTime);    
        tank.SetActive(true);
        tank.GetComponent<Tank>().IsAlive = true;
        tank.GetComponent<Tank>().Downgrade();
        tank.GetComponent<Tank>().CurrentHP = tank.GetComponent<TankData>().HP;
        
    }

    IEnumerator  RefalshProp()
    {

        int index = GameObject.Find("PropRefalsh").transform.childCount;
        List<GameObject> propPosList = new List<GameObject>();
        for(int i = 0; i < index; i++)
        {
            propPosList.Add(GameObject.Find("PropRefalsh").transform.GetChild(i).gameObject);
        }
        propPlace = propPosList.ToArray();

        GameObject addHpProp = Resources.Load<GameObject>("AddHpProp");
        GameObject addBigBulletProp = Resources.Load<GameObject>("AddBigBulletProp");
        GameObject addInvincibleProp = Resources.Load<GameObject>("AddInvincibleProp");
        GameObject addAttackSpeedProp = Resources.Load<GameObject>("AddAttackSpeedProp");
        GameObject addSpeedProp = Resources.Load<GameObject>("AddSpeedProp");

        if (GameObject.FindGameObjectsWithTag("Prop").Length == 0)
        {
            foreach (GameObject p in propPlace)
            {
                
                int range = Random.Range(0, propPlace.Length);
                switch (range)
                {
                    
                    case 0: Instantiate(addHpProp, p.transform.position, p.transform.rotation); break;
                    case 1: Instantiate(addBigBulletProp, p.transform.position, p.transform.rotation); break;
                    case 2: Instantiate(addInvincibleProp, p.transform.position, p.transform.rotation); break;
                    case 3: Instantiate(addAttackSpeedProp, p.transform.position, p.transform.rotation); break;
                    case 4: Instantiate(addSpeedProp, p.transform.position, p.transform.rotation); break;
                }

            }
        }
        yield return new WaitForSeconds(refalshTime);
        yield return DestoryProp();


    }
    
    IEnumerator DestoryProp()
    {
        if (GameObject.FindGameObjectsWithTag("Prop").Length != 0)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Prop");
            foreach (GameObject g in gameObjects)
            {
                Destroy(g);
            }
        }
        yield return new WaitForSeconds(refalshTime);
        yield return RefalshProp();
    }




}
