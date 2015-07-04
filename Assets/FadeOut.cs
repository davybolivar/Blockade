using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

	public float time;
	public Color normal;
	public Color transparentCol;
	public float fadeInDelay;
	public float fadeOutDelay;

	// Use this for initialization
	void Start () {
//		Invoke ("Fade",10);
	}
	
	// Update is called once per frame
	void Update () {
		Invoke ("FadeI",fadeInDelay);
		Invoke ("FadeO",fadeOutDelay);
	}
	
	void FadeO(){
		CancelInvoke ("FadeI");
		GetComponent<Text>().color = Color.Lerp(GetComponent<Text>().color,transparentCol,time);
	}

	void FadeI(){
		GetComponent<Text>().color = Color.Lerp(GetComponent<Text>().color,normal,time);
	}
}
