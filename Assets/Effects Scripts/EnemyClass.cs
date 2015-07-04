using UnityEngine;
using System.Collections;

public class EnemyClass: MonoBehaviour {
	
	protected enum Direction{LEFT,RIGHT,UP,DOWN};
	protected Direction dir, prevDir;
	
	protected bool isClimbing;
	
	protected float ladderX;
	
	protected float movX;
	protected float movY;

	public bool deathRecorded;
	
	public bool isDead;

	protected bool climbCooldown;
	
	// Use this for initialization
	public virtual void Start () {
		//		Physics2D.IgnoreCollision(GameObject.FindWithTag("Platform").GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
		isClimbing = false;
		dir = Direction.RIGHT;
		climbCooldown = false;
		deathRecorded = false;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
	}
	
	public virtual void Movement(float speed, Rigidbody2D rb){					 	//HORIZONTAL MOVEMENT
		if (!isClimbing && dir != Direction.UP) {
			if (dir == Direction.LEFT)
				speed *= -1;
			else
				speed *= 1;
			rb.velocity = new Vector2 (speed, rb.velocity.y);
		} else {
			
			if (dir == Direction.DOWN)
				speed *= -1;
			else if(dir == Direction.UP)
				speed *= 1;
			rb.velocity = new Vector2 (rb.velocity.x, speed);
		}
		
	}
	
	void OnCollisionEnter2D(Collision2D col){										//IF HIT BLOCK CALL CHANGE DIRECTION
		if (col.gameObject.tag == "Block") {
			ChangeDirection();
			
		}
		
		if(col.gameObject.tag == "Enemy"){											//IGNORE SPECIFIC COLLIDERS
			Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){											//IF TOUCH LADDER

		if (col.gameObject.tag == "BottomLadder" && !isClimbing) {
			ladderX = col.transform.localPosition.x;
			prevDir = dir; //go back to this if reach top
			dir = Direction.UP;
			
			Invoke("DelayClimb",.4f);
		}
		
		if (col.gameObject.tag == "TopLadder" && isClimbing) {
			climbCooldown = true;
			Invoke("ClimbCd",4f);
			ChangeDirectionAfterClimb();
			CancelInvoke("KeepCenter");
			Invoke("DelayStopClimb", 2f);
		}

		if (col.gameObject.tag == "TopLadder" && !isClimbing && !climbCooldown) {
			isClimbing = true;
			ladderX = col.transform.localPosition.x;
			prevDir = dir; //go back to this if reach top
			dir = Direction.DOWN;
			
			Invoke("DelayClimb",.4f);
		}

		if (col.gameObject.tag == "BottomLadder" && isClimbing) {
//			climbCooldown = true;
//			Invoke("ClimbCd",4f);
			ChangeDirectionAfterClimb();
			CancelInvoke("KeepCenter");
//			Invoke("DelayStopClimb", 2f);
			isClimbing = false;
		}

	}

	public void ClimbCd(){
		climbCooldown = false;
	}
	
	void ChangeDirectionAfterClimb(){
		if (prevDir == Direction.LEFT) {
			dir = Direction.RIGHT;
			transform.localScale = new Vector3 (1, transform.localScale.y, transform.localScale.z);
		} else if(prevDir == Direction.RIGHT){
			dir = Direction.LEFT;
			transform.localScale = new Vector3 (-1, transform.localScale.y, transform.localScale.z);
		}
	}
	
	void DelayStopClimb(){
		isClimbing = false;
	}
	
	void DelayClimb(){
		isClimbing = true;
		if(isClimbing){
			InvokeRepeating("KeepCenter",.2f,.05f);
		}
	}
	
	void KeepCenter(){
		transform.localPosition = new Vector2(ladderX,transform.localPosition.y);
	}
	
	public void ChangeDirection(){                     								//CHANGE DIRECTION
		if (dir == Direction.LEFT) {
			transform.localScale = new Vector3 (1, transform.localScale.y, transform.localScale.z);
			dir = Direction.RIGHT;
		} else if (dir == Direction.RIGHT) {
			transform.localScale = new Vector3 (-1, transform.localScale.y, transform.localScale.z);
			dir = Direction.LEFT;
		}
	}
	
	public virtual void Die(Rigidbody2D rb){
		rb.fixedAngle = false;
		//		rb.AddForce (new Vector2(Random.Range(3,4),Random.Range(40,80)));
//		rb.AddForce (new Vector2(4,50f));
		rb.gravityScale = 7;
		rb.AddTorque(30f);
		gameObject.GetComponent<CircleCollider2D> ().isTrigger = true;
//		Invoke ("DestroyObject",5f);
	}
	
	public virtual void DestroyObject(GameObject go){
		Debug.Log ("DESTROYED");
		Destroy (go);
	}
}
