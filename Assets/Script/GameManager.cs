using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//处理UI及BGM
public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject[] revivePlace;
    public GameObject[] tanks;

    [Range(0,5)][SerializeField][Header("坦克复活时间")]
    private float reviveTime;

    private void Awake()
    {
        instance = this;
        tanks = GameObject.FindGameObjectsWithTag("Tank");
    }

    private void Update()
    {
        foreach(GameObject tank in tanks)
        {
            if(!tank.GetComponent<Tank>().IsAlive)
            {
                StartCoroutine(Revive(tank));
            }
        }
    }

    public void GameOver()
    {

    }

    IEnumerator Revive(GameObject tank)
    {
        yield return reviveTime;
        tank.transform.position = revivePlace[Random.Range(0, revivePlace.Length)].transform.position;
        tank.SetActive(true);
        tank.GetComponent<Tank>().IsAlive = true;
        tank.GetComponent<Tank>().Downgrade();
        tank.GetComponent<Tank>().CurrentHP = tank.GetComponent<TankData>().HP;
        yield return 0;
    }


}
