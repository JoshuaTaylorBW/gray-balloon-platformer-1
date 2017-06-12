using UnityEngine;
using System.Collections;

public class Fight : MonoBehaviour {

	public GameObject protagonist;
	private BoxCollider bottom;
	private SphereCollider top;

	// Use this for initialization
	void Start () {
		bottom = GetComponent<BoxCollider>();
		top = GetComponent<SphereCollider>();
		protagonist = GameObject.Find("Character");
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.name == "Character"){
			protagonist.SendMessage("fall");
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "Character"){
			bottom.enabled = false;
			protagonist.SendMessage("bounce", -1f);
		}
	}

}