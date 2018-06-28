using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour 
{
	[SerializeField]
	private int hitTimes_max = 3;	
	private int hitTimes_current = 0;
	[SerializeField]
	private bool hitable;

	private void Update() 
	{
		if (hitTimes_current == hitTimes_max)
		{
			Debug.Log("被击碎了");
			Destroy(this.gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Bullet")
		{
			if (hitable)
			{
				hitTimes_current++;
			}
		}
	}
}
