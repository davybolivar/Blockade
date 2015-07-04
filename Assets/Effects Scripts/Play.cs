using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {

	public int cameraScale;
	public static Camera cam;
	// Use this for initialization
	void Start () {
		float s_baseOrthographicSize = Screen.height / 16.0f / (2.0f * cameraScale);

		cam = Camera.main;
		cam.orthographicSize = s_baseOrthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void CamShake(){
		cam.GetComponent<CameraShake> ().Shake ();
	}
}
