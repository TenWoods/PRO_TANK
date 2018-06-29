using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaArea : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private float damage_time;
    private List<GameObject> tanks;
    private List<float> timers;

    private void Start()
    {
        timers = new List<float>();
        tanks = new List<GameObject>();
    }

    private void Update() 
	{
        for(int i = 0; i < timers.Count; i++)
        {
            if (timers[i] < damage_time)
            {
                timers[i] += Time.deltaTime;
                continue;
            }
            DebuffEffect(tanks[i]);
            timers[i] = 0;
        }
	}

    //坦克掉血
    private void DebuffEffect(GameObject tank)
	{
        tank.GetComponent<Tank>().CurrentHP -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tank")
        {
            if (tanks.Contains(collision.gameObject))
                return;
            tanks.Add(collision.gameObject);
            timers.Add(0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tanks.Contains(collision.gameObject))
        {
            int index = tanks.IndexOf(collision.gameObject);
            tanks.Remove(collision.gameObject);
            timers.RemoveAt(index);
        }
    }
}
