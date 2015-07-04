using UnityEngine;
using System.Collections;

public class Blocks : MonoBehaviour {

	public GameObject smashPrefab;
	public float offsetY;

	public Vector3 origPos;
	public Quaternion origRotation;
	public Sprite origSprite;

	public AudioClip destroyBlockSfx;
	public AudioClip hitFloorSfx;

	public AudioSource source;

	public bool dropState;

	public float resetTime;
	public float dropDelay;
	public float blockMass;

	void Awake(){
		origPos = transform.localPosition;
		origRotation = new Quaternion (0f, 0f, 0f, 0f);
		origSprite = transform.GetComponent<SpriteRenderer> ().sprite;
	}

	// Use this for initialization
	void Start () {
		offsetY = GameObject.Find ("Map").GetComponent<TileGenerator>().tileHeight;

		dropState = false;

		source = GameObject.Find ("Player").GetComponent<AudioSource> ();

		resetTime = 5f;
		dropDelay = .3f;
		blockMass = 5f;

		Physics2D.IgnoreCollision(GameObject.FindWithTag("Platform").GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col){

	}

	void OnTriggerEnter2D(Collider2D col){
		if (gameObject.transform.tag == "Platform" && !dropState) {
			if (col.transform.tag == "Drill") { //smashed by player

				GameObject smash = (GameObject)Instantiate (smashPrefab, new Vector3 (transform.position.x, transform.position.y+offsetY, 1f), Quaternion.identity);
				smash.transform.parent = GameObject.Find("Effects").transform;
				
				source.PlayOneShot(destroyBlockSfx,4f);

				GameObject.Find("Main Camera").GetComponent<CameraShake>().Shake();
				dropState = true;
				DropEffect();
				Invoke ("DropBlock", dropDelay); 
			}
		}

		if (col.transform.tag == "Enemy") { //HIT ENEMY
			col.gameObject.GetComponent<NormalEnemy>().isDead = true;

			source.PlayOneShot(hitFloorSfx,2f);

			if(!col.gameObject.GetComponent<NormalEnemy>().deathRecorded){
				if(!GameObject.Find("Player").GetComponent<PlayerController>().gameOver){
					GameObject.Find("Player").GetComponent<PlayerController>().score++;
					col.gameObject.GetComponent<NormalEnemy>().deathRecorded = true;
				}
			}
		}	
	}

	void Reset(){

		do
		{
			gameObject.transform.localPosition = origPos;
			gameObject.transform.localRotation = origRotation;
			gameObject.GetComponent<SpriteRenderer> ().sprite = origSprite;
			
		}while(transform.localPosition != origPos);

		Invoke ("DelayCollider",.2f);
	}

	void DelayCollider(){
		if (transform.localPosition == origPos) {
			gameObject.GetComponent<TriggerGlitch> ().glitch = false;
			gameObject.GetComponent<Collider2D> ().isTrigger = false;
		} else {
			Reset();
		}
	}

	void DropBlock(){

		gameObject.transform.tag = "FallingPlatform";

		if (gameObject.GetComponent<Rigidbody2D> () == null) {
			Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D> ();
			rb.mass = blockMass;
			rb.AddTorque(Random.Range(-20f, 20f));

			dropState = true;

			gameObject.GetComponent<Collider2D> ().isTrigger = true;

			Invoke("Reset", resetTime);
			Invoke("DestroyBlock", resetTime);
		}
	}

	void DropEffect(){
		gameObject.GetComponent<TriggerGlitch> ().glitch = true;
	}

	void DestroyBlock(){
		dropState = false;
		gameObject.GetComponent<SpriteRenderer> ().sprite = null;
		gameObject.transform.tag = "Platform";
		Destroy (gameObject.GetComponent<Rigidbody2D> ());
//		Invoke ("Reset", resetTime);
		Reset ();
	}

}
