using UnityEngine;
using System.Collections;

public class NormalEnemy : EnemyClass {

	public Rigidbody2D rb;
	public float speed;

	// Use this for initialization
	public override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	public override void Update () {
		
		if (!isDead) {
			base.Movement(speed,rb);
		} else {
			base.Die(rb);
		}
	}

}
