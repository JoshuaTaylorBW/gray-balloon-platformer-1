using UnityEngine;
using System.Collections;
using UnityEngine.WSA;

public class CharacterOnRightCameraController : MonoBehaviour {

	public GameObject protagonist;
	public float endPosition;

	// Use this for initialization
	void Start () {
		protagonist = GameObject.Find("Character");
	}

	// Update is called once per frame
	void LateUpdate () {
		if (protagonist.transform.position.x > transform.position.x - 5.8f && protagonist.transform.position.x < endPosition){
			transform.position = new Vector3(protagonist.transform.position.x - 5.8f, 0, -10);
		}

		if (protagonist.transform.position.x > endPosition + 5f){
			transform.position = new Vector3(endPosition, 0, -10);
		}
	}
}
