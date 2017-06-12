using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
	void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "Character"){
			Application.LoadLevel("Temp_menu");
		}
	}
}
