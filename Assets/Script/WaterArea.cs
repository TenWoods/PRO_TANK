using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterArea : MonoBehaviour
{
    private List<GameObject> tanks;

    private void Start()
    {
        tanks = new List<GameObject>();
    }

    //坦克减速
    private void SpeedDown(GameObject tank)
    {
        tank.GetComponent<TankData>().Speed /= 2;
    }

    //坦克速度恢复
    private void SpeedReturn(GameObject tank)
    {
        tank.GetComponent<TankData>().Speed *= 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tank")
        {
            if (tanks.Contains(collision.gameObject))
                return;
            Debug.Log("坦克减速");
            tanks.Add(collision.gameObject);
            SpeedDown(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tanks.Contains(collision.gameObject))
        {
            if (tanks.Contains(collision.gameObject))
            {
                Debug.Log("坦克速度恢复");
                SpeedReturn(collision.gameObject);
                tanks.Remove(collision.gameObject);
            }
        }
    }
}
