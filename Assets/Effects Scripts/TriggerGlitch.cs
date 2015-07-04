using UnityEngine;
using System.Collections;

public class TriggerGlitch : MonoBehaviour {
	
	public Material glitchMat;
	public Material normalMat;
	public bool glitch;
	public Renderer rend;

	// Use this for initialization
	void Start () {

		glitch = false;
		rend = GetComponent<Renderer>();
		rend.material.shader = Shader.Find("Glitch");
	}
	
	// Update is called once per frame
	void Update () {

		if (glitch) {
			ChangeMaterial (this.gameObject, glitchMat);
			GlitchProbability(1f, .2f);
		} else {
			ChangeMaterial (this.gameObject, normalMat);
			GlitchProbability(1f, 1f);
		}

	}

	void ChangeMaterial(GameObject go, Material mat)
	{
		rend.material = mat;
	}

	void GlitchProbability(float valueProb, float valueIntense){
//		rend.material.SetFloat ("_DispProbability", valueProb);
//		rend.material.SetFloat ("_ColorProbability", valueProb);
//		rend.material.SetFloat ("_ColorIntensity", valueIntense);
//
//		int mult = 0;
//
//		if (gameObject.tag == "Block") {
//			mult = 10;
//			Debug.Log(gameObject.tag+"-"+mult);
//		}

		glitchMat.SetFloat ("_DispProbability", valueProb);
		glitchMat.SetFloat ("_ColorProbability", valueProb);
		glitchMat.SetFloat ("_ColorIntensity", valueIntense);
	}
}
