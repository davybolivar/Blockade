using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject target;
	public GameObject target2;
	public float offsetY;
	public float speed;

	// Update is called once per frame
	void Update () {
		if (!target.GetComponent<PlayerController> ().gameOver) {
//			transform.position = Vector3.Lerp (transform.position, new Vector3 (target.transform.position.x, target.transform.position.y + offsetY, transform.position.z), speed * Time.deltaTime);

			float camX = Mathf.Clamp(target.transform.position.x, 7.6f, 13.5f);
			float camY = Mathf.Clamp(target.transform.position.y, -15.5f, -4.6f);

			transform.position = Vector3.Lerp (transform.position, new Vector3 (camX, camY+offsetY, transform.position.z), speed * Time.deltaTime);

		} else {
			transform.position = Vector3.Lerp (transform.position, new Vector3 (target2.transform.position.x, target2.transform.position.y + 1, transform.position.z), speed * Time.deltaTime);
//			Math.clampf(pos.x, -100, 100), 
//			Math.clampf(pos.y, -100, 100),
		}
	}
}
