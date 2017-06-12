using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	public GameObject protagonist;	
	public string newScene;
	private bool isTouched;
	void Start () {
		isTouched = false;
		protagonist = GameObject.Find("Character");
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject == protagonist){
			Application.LoadLevel(newScene);
		}
	}

}

