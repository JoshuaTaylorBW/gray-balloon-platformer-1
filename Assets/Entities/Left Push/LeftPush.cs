using UnityEngine;
using System.Collections;

public class LeftPush : MonoBehaviour {
	public GameObject protagonist;
	private BoxCollider bottom;
	public float pushForce;

	// Use this for initialization
	void Start () {
		bottom = GetComponent<BoxCollider>();
		protagonist = GameObject.Find("Character");
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "Character"){
			protagonist.SendMessage("leftPush", pushForce);
		}
	}

}
