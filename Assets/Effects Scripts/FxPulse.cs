using UnityEngine;
using System.Collections;

public class FxPulse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Pulse", 0f, 0f);
	}

	void Pulse(){
		gameObject.transform.localScale = Vector2.Lerp (new Vector2(gameObject.transform.localScale.x+10f,gameObject.transform.localScale.y+10f),
		                                                new Vector2(gameObject.transform.localScale.x,gameObject.transform.localScale.y), 1f);
		Debug.Log (gameObject.transform.localScale);
	}
}
