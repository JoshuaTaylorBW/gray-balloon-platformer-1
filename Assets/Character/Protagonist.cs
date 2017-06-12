using UnityEngine;
using System.Collections;

public class Protagonist : MonoBehaviour {

	public bool mobile;

	private CharacterController controller;

	//movement
	public float JumpHeight;
	public float JumpStopSpeed;
	public float fallSpeed;
	public GUIText consoleText;

	private Animator animator;

	public float walkAcceleration;
	public float currSpeed;
	public float walkSpeed;
	public float friction;
	public bool running = false;
	public bool beingPushed = false;
	private float airSpeed;
	public float gravity;

	public Vector3 velocity;
	public bool Jumping = false;
	public bool Falling = true;
	public bool stopJumping = false;
	public bool bouncing = false;

	//touch controls
	private Vector2 touchOrigin = -Vector2.one;

	void Start()
	{
		airSpeed = walkSpeed / 2;
		animator = gameObject.GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		controller.Move(velocity * Time.deltaTime);

		VerticalMovement();

		mobileInput();

		if(velocity.y < 0 && !controller.isGrounded)
		{
			Falling = true;
			stopJumping = false;
		}
		else
		{
			Falling = false;
		}

		if(!controller.isGrounded)
		{
			velocity.y -= (gravity + fallSpeed) * Time.deltaTime;
		}

		if(!running && controller.isGrounded && !beingPushed){
			if(velocity.x > 0){
				velocity.x -= 0.45f;
			}else{
				velocity.x = 0;
			}

		}

		if(stopJumping)
		{
			velocity.y -= velocity.y * JumpStopSpeed;
		}

		if(beingPushed)
		{
			velocity.x = -walkSpeed;
		}

	} 

	void nonMobileInput(){
		if(Input.GetButtonDown("Run"))
		{
			animator.SetBool("Running", true);
			running = true;
		}

		if(Input.GetButtonUp("Run"))
		{
			animator.SetBool("Running", false);
			running = false;
		}

		HorizontalNonMobileMovement();
	}

	void mobileInput(){
		currentSpeed();
		HorizontalMobileMovement();	
		if (mobile)
		{

			if(Input.touchCount > 0)
			{
				Touch[] myTouches = Input.touches;
				if(myTouches.Length == 1) {
					checkForOneTouch(myTouches);
				}else if(myTouches.Length >= 2) {
					checkForTwoTouches(myTouches);
				} 
			}else{
				animator.SetBool("Running", false);
				if(!controller.isGrounded && !Falling && !bouncing)
				{
					stopJumping = true;
				}
				running = false;
			}
		}
		else
		{
			if(Input.GetButtonDown("Run"))
			{
				animator.SetBool("Running", true);
				running = true;
			}

			if(Input.GetButtonUp("Run"))
			{
				animator.SetBool("Running", false);
				running = false;
			}



			if(controller.isGrounded)
			{
				animator.SetBool("Jumping", false);
				Jumping = false;
				stopJumping = false;
				Falling = false;
				if(Input.GetButtonDown("Jump"))
				{
					Jumping = true;
					velocity = new Vector3(
						velocity.x * 1.7f,
						JumpHeight,
						velocity.z
					);
				}
			}
		}
	}

	void checkForOneTouch(Touch[] myTouches) {
		if(myTouches[0].position.x < Screen.width / 2)
		{
			animator.SetBool("Running", true);
			running = true;
			if(!controller.isGrounded && !Falling && !bouncing)
			{
				stopJumping = true;
			}
		}

		if(controller.isGrounded)
		{
			animator.SetBool("Jumping", false);
			Jumping = false;
			stopJumping = false;
			Falling = false;
			if((myTouches[0].position.x > Screen.width / 2))
			{
				Jumping = true;
				running = false;
				velocity = new Vector3(
					velocity.x * 1.7f,
					JumpHeight,
					velocity.z
				);
			}
		}
	}

	void checkForTwoTouches(Touch[] myTouches)
	{
		if(myTouches[0].position.x < Screen.width / 2 || myTouches[1].position.x < Screen.width / 2)
		{
			//consoleText.text = "touchLeft: true \n" + "screenWidth: " + Screen.width + "\n Half Screen Width: " + ("" + Screen.width / 2) +   "\n lastTouch: " + myTouches[0].position.x;
			animator.SetBool("Running", true);
			running = true;
		}

		if(controller.isGrounded)
		{
			animator.SetBool("Jumping", false);
			Jumping = false;
			stopJumping = false;
			Falling = false;
			if((myTouches[0].position.x > Screen.width / 2) || (myTouches[1].position.x > Screen.width / 2))
			{
				//consoleText.text = "touchRight: true \n" + "screenWidth: " + Screen.width + "\n Half Screen Width: " + ("" + Screen.width / 2) +   "\n lastTouch: " + myTouches[0].position.x;
				Jumping = true;
				velocity = new Vector3(
					velocity.x * 1.7f,
					JumpHeight,
					velocity.z
				);
			}
		}
	}

	void currentSpeed()
	{
		if(running)
		{
			if(currSpeed < walkSpeed)
			{
				currSpeed += (walkAcceleration) * Time.deltaTime;
			}
			if(currSpeed > walkSpeed)
			{
				currSpeed = walkSpeed;
			}
		}
		if(!running || beingPushed){
			if(currSpeed > 0)
			{
				currSpeed -= (friction) * Time.deltaTime;
			}

			if(currSpeed < 0 && !beingPushed)
			{
				currSpeed = 0;
			}
		}
	}	

	void HorizontalMobileMovement ()
	{
		velocity.x = currSpeed;
		if(beingPushed)
		{
			Debug.Log("bieng pushed");
			velocity = new Vector3(
				0f,
				velocity.y,
				velocity.z
			);
			velocity.x -= velocity.x * JumpStopSpeed / 1000;
		}else{
			if(controller.isGrounded)
			{
				velocity = new Vector3(
					currSpeed,
					velocity.y,
					velocity.z
				);
			}
		}
	}

	void HorizontalNonMobileMovement ()
	{
		if(!running && controller.isGrounded)
		{
			velocity = new Vector3(
				Input.GetAxis("Horizontal") * 3,
				velocity.y,
				velocity.z
			);
		}
	}

	void bounce(float bounceHeight)
	{
		beingPushed = false;
		Jumping = true;
		if(bounceHeight == -1){
			velocity = new Vector3(
				(currSpeed) * 1.7f,
				JumpHeight,
				velocity.z
			);
		}else{
			velocity = new Vector3(
				(currSpeed) * 1.7f,
				bounceHeight,
				velocity.z
			);
		}

		bouncing = true;
	}

	void leftPush(float pushTime)
	{
		beingPushed = true;

		if(pushTime == -1)
		{

			StartCoroutine(pushForX(1.5f));
		}
		else
		{
			StartCoroutine(pushForX(pushTime));
		}
			

	}

	IEnumerator pushForX(float pushTime)
	{
		while (true)
		{
			yield return new WaitForSeconds(pushTime);
			beingPushed = false;
		}
	}

	void fall()
	{
		velocity.y = -1;
	}

	void VerticalMovement ()
	{
		if(controller.isGrounded){
			animator.SetBool("Jumping", false);
			bouncing = false;
			Jumping = false;
			stopJumping = false;
			Falling = false; 
			if(Jumping || Input.GetButtonDown("Jump")){
				velocity = new Vector3(
					-(Input.GetAxis("Horizontal")  * 1.7f),
					JumpHeight,
					velocity.z
				);

			}
		}
		if(!controller.isGrounded){

			if(Input.GetButtonUp("Jump")){
				stopJumping = true;
			}

			animator.SetBool("Jumping", true);
			if(Jumping && !Falling){
				float yRate = velocity.y;
				if(running){
					velocity = new Vector3(
						-((Input.GetAxis("Horizontal") * 3) * 1.7f),
						velocity.y,
						velocity.z
					);
				}
			}
		}
	}

	public bool getRunning(){
		return running;
	}

	public bool getJumping(){
		return Jumping;
	}

}
