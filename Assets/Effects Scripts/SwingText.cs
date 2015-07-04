using UnityEngine;
using System.Collections;

public class SwingText : MonoBehaviour {
	
	public float maxAngle; 
	public float minAngle;
	public float dur;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Swing ();
	}
	
	void Swing(){
		transform.rotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y,
		                                      Mathf.PingPong(Time.time * dur, maxAngle-minAngle)+minAngle);
	}
}