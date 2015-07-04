using UnityEngine;
using System.Collections;

public class PlatformerPlayer : MonoBehaviour {

	public GameObject jumpPrefab;

	public AudioClip jumpSfx;
	public AudioClip landSfx;
	public AudioClip walkSfx;
	public AudioSource source;

	//Animation
	public Animator animator;
	
	public bool killedEnemy;

	const int STATE_IDLE = 0;
	const int STATE_RUN = 1;
	const int STATE_JUMP = 2;
	
	string _currentDirection = "left";
	int _currentAnimationState = STATE_IDLE;

	// physics stuff
	public float speed;
	public float jumpAmount;
	public Vector2 jumpForce;

	protected bool facingRight = true;
	protected Rigidbody2D rb;

	// jump stuff
	public Transform groundChecker;
	public LayerMask whatIsGround;
	GameObject groundCheck;
	float groundRadius = 0.3f;
	bool grounded = false;
	bool jumpAxisInUse = false;

	//special attack
	public float groundSmashForce = -1200f;

	// Use this for initialization
	public virtual void Start () {
		animator = this.gameObject.GetComponentInChildren <PlayerBody>().GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		jumpForce = new Vector2 (0, jumpAmount);
	}

	// Update is called once per frame
	public virtual void FixedUpdate () {
		UpdateOnGround ();
	}

	public virtual void Update(){
		JumpControls ();
		MovementControls ();
		SpecialAttack ();
	}

	public virtual void MovementControls(){
		// left
		if (Input.GetAxis ("Horizontal") < 0) {
			rb.velocity = new Vector2 (-speed, rb.velocity.y);
			facingRight = false;
			changeState(STATE_RUN);
			changeDirection ("right");

//			if (grounded) {
//				source.PlayOneShot(walkSfx,1f);										//PLAY WALKING SOUND
//			}

		}
		//right 
		else if (Input.GetAxis ("Horizontal") > 0) {
			rb.velocity = new Vector2 (speed, rb.velocity.y);
			facingRight = true;
			changeState(STATE_RUN);
			changeDirection ("left");

//			if (grounded) {
//				source.PlayOneShot(walkSfx,1f);										//PLAY WALKING SOUND
//			}		

		}
		else if (Input.GetAxis ("Horizontal") == 0) {
			rb.velocity = new Vector2(0, rb.velocity.y);
			changeState(STATE_IDLE);
//			LerpXSpeedToZero();
		}
	}

	public virtual void JumpControls(){
		// jump
		if (Input.GetAxisRaw("Jump") != 0 && grounded) {
			if(!jumpAxisInUse){
				Jump ();
				jumpAxisInUse = true;
			}
		}
		else if(Input.GetAxisRaw("Jump") == 0){
			jumpAxisInUse = false;
		}
	}

	public virtual void Jump(){
//		AudioListener.
		source.PlayOneShot(jumpSfx,1f);
		GameObject jump = (GameObject)Instantiate (jumpPrefab, new Vector3 (transform.position.x, transform.position.y, 1f), Quaternion.identity);
		jump.transform.parent = GameObject.Find("Effects").transform;
		
		Debug.Log ("IMHERE");
		changeState(STATE_JUMP);
		rb.velocity = new Vector2 (rb.velocity.x, 0);
		rb.AddForce (new Vector2(0, jumpAmount));
		grounded = false;
	}

	public virtual void UpdateOnGround(){
		grounded = Physics2D.OverlapCircle (groundChecker.position, groundRadius, whatIsGround);
		// anim.SetBool("Grounded", grounded);
	}

	public virtual void LerpXSpeedToZero(){
		rb.velocity = Vector2.Lerp (rb.velocity, new Vector2 (0, rb.velocity.y), 12 * Time.deltaTime);
	}

	public virtual void SpecialAttack(){ //GROUND SMASH
		if (Input.GetKeyDown (KeyCode.X) && !grounded) {
			rb.velocity = new Vector2(0,0);
			rb.AddForce(new Vector2(0,groundSmashForce));
			GameObject.Find("Drill").transform.localPosition = new Vector3(GameObject.Find("Drill").transform.localPosition.x,-.3f,0);
			Invoke("ResetVal",.2f);
		}
	}

	void ResetVal(){
		GameObject.Find("Drill").transform.localPosition = new Vector3(GameObject.Find("Drill").transform.localPosition.x,50f,0);
	}

	void changeState(int state){
		
		if (_currentAnimationState == state)
			return;
		
		switch (state) {

		case STATE_IDLE:
			animator.SetInteger ("state", STATE_IDLE);
			break;

		case STATE_RUN:
			animator.SetInteger ("state", STATE_RUN);
			break;
			
		}
		
		_currentAnimationState = state;
	}

	void changeDirection(string direction)
	{
		
		if (_currentDirection != direction)
		{
			if (direction == "right")
			{
				transform.localScale = new Vector3(-1,transform.localScale.y,transform.localScale.z);
				_currentDirection = "right";
			}
			else if (direction == "left")
			{
//				transform.Rotate (0, -180, 0);
				transform.localScale = new Vector3(1,transform.localScale.y,transform.localScale.z);
				_currentDirection = "left";
			}
		}
		
	}
}
