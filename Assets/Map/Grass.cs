using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour {

	public GameObject protagonist;	
	public bool isTouched;
	public Animator grassAnimator;
	// Use this for initialization
	void Start () {
		grassAnimator = GetComponent<Animator>();
		grassAnimator.enabled = false;
		isTouched = false;
		protagonist = GameObject.Find("Character");
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject == protagonist){
			if(!isTouched){
				isTouched = true;	
				grassAnimator.enabled = true;
			}
		}
	}

}

