using UnityEngine;
using System.Collections;

public class PlayerController : PlatformerPlayer {

	bool shooting = false;
	bool isStoppedOnShoot = false;
	public Animator anim;
	// hit stuff
	int hp;
	const float invulnerableTime = 3f;
	SpriteRenderer sprite;
	public bool recoveringFromHit;
	public int score;
	// coins
	int coins;
	public bool gameOver;

//	public void Awake(){
//		Vector3 spawnPoint = GameObject.Find ("SpawnPoint").transform.localPosition;
//		transform.localPosition = spawnPoint;
//	}

	// Use this for initialization
	public override void Start () {
		base.Start ();
		gameOver = false;
		hp = 2;
		score = 0;
		sprite = transform.FindChild("playerBody").GetComponent<SpriteRenderer> ();
		recoveringFromHit = false;

//		Vector3 spawnPoint = GameObject.Find ("SpawnPoint").transform.localPosition;
//		transform.localPosition = spawnPoint;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
		ShieldControls ();
	}

	public override void MovementControls(){
		if (!shooting) {
			base.MovementControls();
		}
	}

	void ShieldControls(){
		// pressing shied button
		if (Input.GetAxisRaw ("Shield") != 0) {
			Shooting();

			if(!isStoppedOnShoot){
				isStoppedOnShoot = true;
			}
		}
		// released
		else {
			if(isStoppedOnShoot){
				isStoppedOnShoot = false;
			}
			shooting = false;
		}
	}

//	void SpecialAttack(){
//		base.SpecialAttack();
//		Debug.Log ("WEE");
//	}

	void Shooting(){
		shooting = true;

		LerpXSpeedToZero ();

//		float xaxis = Input.GetAxis ("Horizontal");
//		float yaxis = Input.GetAxis ("Vertical");

//		float ypos;
//		float rotation = 0;
	}

	public void Hit(){
		hp--;

		if (hp <= 0){
			Die ();
		}
		else{
			InvokeRepeating("Blinking", 0, 0.05f);
			Invoke ("StopBlink", invulnerableTime);
			recoveringFromHit = true;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.name == "Platform" || col.gameObject.name == "Floor") {
			rb.velocity = new Vector2(rb.velocity.x,0f);
		}

		if (col.gameObject.tag == "Enemy") {
			Die();
		}
	}

	public void Die(){
//		Application.LoadLevel (Application.loadedLevelName);
		gameOver = true;
//		GameObject.Find ("playerBody").SetActive(false);
		GameObject.Find ("playerBody").GetComponent<SpriteRenderer> ().enabled = false;
	}		

	void Blinking(){
		sprite.enabled = !sprite.enabled;	
	}

	void StopBlink(){
		CancelInvoke ("Blinking");
		sprite.enabled = true;
		recoveringFromHit = false;
	}

	public void PickCoin(){
		coins++;
	}
}
