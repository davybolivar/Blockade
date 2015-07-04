using UnityEngine;
using System.Collections;

public class PlayerBody : MonoBehaviour {

	PlayerController pc;

	void Start(){
		pc = GetComponentInParent<PlayerController> ();
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Bullet" && !pc.recoveringFromHit) {
			pc.Hit();
//			col.gameObject.GetComponent<Bullet>().Die();
		}
	}
}
