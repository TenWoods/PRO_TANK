using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject flag_Prefab;
    public Transform[] flag_Spawn_Point;
    public float flag_Set_Time;
    private float flag_timer;

    private void Start()
    {
        flag_timer = 0;
    }

    private void Update()
    {
        FlagInit();
    }

    private void FlagInit()
    {
        if (flag_timer <= flag_Set_Time)
        {
            flag_timer += Time.deltaTime;
            return;
        }
        flag_timer = 0;
        int spawnIndex = Random.Range(0, 3);
        GameObject.Instantiate(flag_Prefab, flag_Spawn_Point[spawnIndex].position, flag_Spawn_Point[spawnIndex].rotation);
    }

}
