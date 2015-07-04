using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int hp;

	public virtual void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Bullet") {
			Hit (1);
		}
	}

	public virtual void Hit(int damage){
		hp -= damage;

		if (hp <= 0) {
			Die ();	
		}
	}

	public virtual void Die(){
		Destroy (this.gameObject);
	}
}
