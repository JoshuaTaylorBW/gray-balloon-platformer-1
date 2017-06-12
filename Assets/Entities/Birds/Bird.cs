using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	private CharacterController controller;
	private Animator animator;
	private bool flying;
	public bool flyRight;
	public float horizontalSpeed;
	public float verticalSpeed;
	private Vector3 velocity;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
		changeValuesBasedOnDirection();
	}
	
	// Update is called once per frame
	void Update () {
		if(flying){
			controller.Move(velocity * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "Character"){
			flying = true;
			animator.SetBool("Flying", true);
			if(flyRight){
				velocity = new Vector3(horizontalSpeed, verticalSpeed, 0);
			}else{
				velocity = new Vector3(-horizontalSpeed, verticalSpeed, 0);
			}
		}
	}


	private void changeValuesBasedOnDirection(){
		if(flyRight){
			GetComponent<SpriteRenderer>().flipX = false;
			velocity = new Vector3(horizontalSpeed, verticalSpeed, 0);
		}else{
			velocity = new Vector3(-horizontalSpeed, verticalSpeed, 0);
		}
	}

}
