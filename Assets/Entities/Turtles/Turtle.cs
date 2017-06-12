using UnityEngine;
using System.Collections;

public class Turtle : MonoBehaviour {

	public GameObject protagonist;
	private BoxCollider bottom;
	private SphereCollider top;
	public bool startMovingRight;
	private float moveSpeed = 1f;
	public bool wandering;
	public bool linearWander;
	public bool moveOnTouch;
	private Vector3 velocity;
	private CharacterController controller;
	private float gravity = 10f;
	private float fallSpeed = 3f;
	public float bounceHeight;


	// Use this for initialization
	void Start () {
		protagonist = GameObject.Find("Character");
		controller = GetComponent<CharacterController>();
		if(!startMovingRight){
			moveSpeed = -1f;
			flipSprite();
		}
		if(wandering){
			startWandering();
		}
	}

	void Update() {
		wander();
		if(!controller.isGrounded)
		{
			velocity.y -= (gravity + fallSpeed) * Time.deltaTime;
		}
	}

	void OnCollisionEnter(Collision col){
		if(wandering){
			if(moveOnTouch){
				switchDirection();
			}
		}else{
			if(col.gameObject.name == "Character"){
				if(moveOnTouch){
					switchDirectionAndBeginMoving();
				}
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.name == "Character"){
			protagonist.SendMessage("bounce", bounceHeight);
		}
	}

	private void switchDirection(){
		velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
		flipSprite();
	}

	private void switchDirectionAndBeginMoving(){
		wandering = true;
		startWandering();
	}


	private void wander(){
		controller.Move(velocity * Time.deltaTime);
	}

	private void startWandering(){
		GetComponent<Animator>().enabled = true;
		velocity = new Vector3(moveSpeed, velocity.y, velocity.z);
	}

	private void flipSprite(){
		GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
	}

}