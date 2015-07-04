using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Beating : MonoBehaviour {
	
	public float lerpSpeed = 0.2f, repeatRate, scaleAmt;
	Vector3 origscale;
	
	void Awake(){
		origscale = transform.localScale;
//		origscale = transform.GetComponent<Text>().fontSize;
		

		if (lerpSpeed == 0.00f)
			lerpSpeed = 0.2f;
	}

	void Start(){
			InvokeRepeating ("Beat", 9, repeatRate);
	}

	void Update(){
		transform.localScale = Vector3.Lerp (transform.localScale, origscale, lerpSpeed);
	}
	
	void Beat(){
		transform.localScale = (scaleAmt * origscale);
//		transform.GetComponent<Text>().fontSize = (scaleAmt * origscale);
	}
}
