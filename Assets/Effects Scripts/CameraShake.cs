using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour 
{
	
	Vector3 originalCameraPosition;
	
	float shakeAmt = 0;
	
	public Camera mainCamera;

	void Start(){
		mainCamera = GetComponent<Camera> ();
	}

	public void Shake(){
		shakeAmt = 0.12f;
		InvokeRepeating("Shaking", 0, .01f);
		Invoke("StopShaking", 0.1f);
	}
	
	void Shaking()
	{
		if(shakeAmt>0) 
		{
			float quakeAmt = Random.value*shakeAmt*2 - shakeAmt;
			Vector3 pp = mainCamera.transform.position;
			pp.y+= quakeAmt; // can also add to x and/or z
			pp.x+= quakeAmt;
			mainCamera.transform.position = pp;
		}
	}
	
	void StopShaking()
	{
		CancelInvoke("Shaking");
	}
	
}