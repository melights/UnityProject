﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDirection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Physics.gravity = transform.up;
	}
}
