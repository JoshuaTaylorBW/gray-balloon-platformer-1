﻿using UnityEngine;
using System.Collections;
using UnityEngine.WSA;

public class Temp_Menu_Button : MonoBehaviour {

	public string newScene;

	void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "Selector"){
			Application.LoadLevel(newScene);
		}
	}
}
