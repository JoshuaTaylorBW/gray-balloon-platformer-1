using UnityEngine;
using System.Collections;

public class temp_menu_pointer : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		Touch[] myTouches = Input.touches;
		if(myTouches.Length > 0){
			Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(myTouches[0].position.x, myTouches[0].position.y, -4));
			transform.position = new Vector3(p.x, p.y, -4);
		}
	}
}
