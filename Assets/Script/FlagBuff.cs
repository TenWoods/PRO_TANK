using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBuff : MonoBehaviour 
{
	private GameObject tank;
	[SerializeField]
	private float get_buff_time;
	private float timer;

	private void Start() 
	{
		tank = null;
        timer = 0;
	}

	private void Update()  
	{
		if (tank == null)
		{
			return;
		}
		timer += Time.deltaTime;
        if (timer > get_buff_time)
        {
            Tank data = tank.GetComponent<Tank>();
            data.Upgrade();
            Destroy(this.gameObject);
        }
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Tank")
		{
			tank = other.gameObject;
		}
	}
}
