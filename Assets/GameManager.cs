﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//处理UI及BGM
public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {

    }



}
