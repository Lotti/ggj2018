﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameTimer : MonoBehaviour {

    Text _timer;

    void Awake () {
        _timer = GetComponent<Text>();
    }

    void Start () {
		
	}
	
	void Update () {
        if ( GameManager.IsInstanced ) {
            _timer.text = GameManager.Instance.GameTimerInt.ToString();
        }
	}
}
