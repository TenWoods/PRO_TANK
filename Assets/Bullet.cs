using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    [Range(5,20)]
    [Header("速度")]
    private float speed;

    [SerializeField]
    [Range(50, 500)]
    [Header("伤害")]
    private int hurt;

    private bool isMax;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
