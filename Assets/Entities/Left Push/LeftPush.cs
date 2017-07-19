using UnityEngine;
using System.Collections;
using UnityEngine.WSA;

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
			if(protagonist.transform.position.x < transform.position.x + 0.8f){
				Time.timeScale = 0.0f;
				Debug.Log("protagonist: " + protagonist.transform.position.x);
				Debug.Log("col.gamobejct: " + col.gameObject.transform.localPosition.x);
				Debug.Log("Triggered Push: " + transform.position.x);
				Debug.Log("Triggered Push Local: " + transform.localPosition.x);
				Debug.Log("Parent Position" + transform.parent.position.x);
				Debug.Log(gameObject.transform.name);
				protagonist.SendMessage("leftPush", pushForce);
			}
		}
	}
}
