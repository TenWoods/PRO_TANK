using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaArea : MonoBehaviour
{
    private List<GameObject> tanks;

    private void Start()
    {
        tanks = new List<GameObject>();
    }

    private void Update() 
	{
        DebuffEffect(tanks);
	}

    private void DebuffEffect(List<GameObject> tanks)
	{
        foreach(GameObject tank in tanks)
        {
            //TODO:坦克掉血
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tank")
        {
            if (tanks.Contains(collision.gameObject))
                return;
            tanks.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tanks.Contains(collision.gameObject))
        {
            tanks.Remove(collision.gameObject);
        }
    }
}
