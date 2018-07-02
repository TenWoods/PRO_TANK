using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//处理UI及BGM
public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject[] revivePlace;
    public GameObject[] tanks;
    public GameObject[] propPlace;

    public Transform flagPos;

    [Range(0,5)][SerializeField][Header("坦克复活时间")]
    private float reviveTime;

    [Range(0, 5)]
    [SerializeField]
    [Header("道具刷新时间")]
    private float refalshTime;

    public bool useFlag = false;

    [Range(0, 20)]
    [SerializeField]
    [Header("旗子刷新时间")]
    private float flagTime;
    private float flagTimer;

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
        GetPanel();
        TankUI();
        foreach (GameObject tank in tanks)
        {
            if(!tank.GetComponent<Tank>().IsAlive)
            {
                Vector3 revive = revivePlace[Random.Range(0, revivePlace.Length)].transform.position;
                tank.transform.position = revive;
                StartCoroutine(Revive(tank, revive));
               
            }
        }
        if(useFlag)
        {
            FlagInit();
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

    public void FlagInit()
    {
        if (flagTimer <= flagTime)
        {
            flagTimer += Time.deltaTime;
            return;
        }
        flagTimer = 0;
        GameObject flagPrefab = Resources.Load<GameObject>("Flag");
        flagPrefab = Instantiate(flagPrefab, flagPos.position, flagPos.rotation);
        flagPrefab.transform.SetParent(GameObject.Find("Map").transform);
    }

    //中途暂停的操作
    private void GetPanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void TankUI()
    {
        string tank1Hp = tanks[0].GetComponent<Tank>().CurrentHP.ToString();
        string tank1BigBullet = tanks[0].GetComponent<Tank>().CurrentBigBullet.ToString();
        string tank2Hp = tanks[1].GetComponent<Tank>().CurrentHP.ToString();
        string tank2BigBullet = tanks[1].GetComponent<Tank>().CurrentBigBullet.ToString();
        GameObject.Find("Canvas").transform.Find("Text1").GetComponent<Text>().text = "tank1生命值"+   tank1Hp+"/1000";
        GameObject.Find("Canvas").transform.Find("Text2").GetComponent<Text>().text = "tank1高强子弹数" +  tank1BigBullet+"/4";
        GameObject.Find("Canvas").transform.Find("Text3").GetComponent<Text>().text = "tank2生命值" +   tank2Hp+"/1000";
        GameObject.Find("Canvas").transform.Find("Text4").GetComponent<Text>().text = "tank2高强子弹数" +   tank2BigBullet+"/4";
    }
    


}
