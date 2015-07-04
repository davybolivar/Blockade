using UnityEngine;
using System.Collections;

public class SpawnEnemyScript : MonoBehaviour {

	public GameObject[] enemyType;
	public Color transparent;
	public Color normal;

	public float spawnChance;

	public float time;

	public int spawnNum;
	public float spawnDelay;
	public float startWait;
	
	public bool spawnNow;

	public int waveWait;
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().color = transparent;
		spawnNow = false;
		StartCoroutine (SpawnEnemy ());
		waveWait = 20;
	}
	
	// Update is called once per frame
	void Update () {
		//IF chances deem fit
		//StartCoroutine(SpawnEnemy ());
//		StartCoroutine (SpawnEnemy ());
	}

	void VisibleColor(bool visible, float time){
		if (visible) {
			GetComponent<SpriteRenderer> ().color = Color.Lerp (GetComponent<SpriteRenderer> ().color, normal, time);
		} else if(!visible){
			GetComponent<SpriteRenderer> ().color = Color.Lerp (GetComponent<SpriteRenderer> ().color, transparent, time);
		}
	}

	IEnumerator SpawnEnemy(){

		VisibleColor (true,time);
		yield return new WaitForSeconds(time);

		int pScore = GameObject.Find ("Player").GetComponent<PlayerController>().score;
		int maxRange = ((pScore / 5) + 4) * 2;

		spawnNum = Random.Range (maxRange,maxRange+4);

		while(true){
			for(int i = 0; i < spawnNum; i++){
				GameObject enemy = (GameObject)Instantiate (enemyType [Random.Range(0,enemyType.Length)], new Vector3 (transform.localPosition.x, transform.localPosition.y, 1f), Quaternion.identity);
				enemy.transform.parent = GameObject.Find ("Mobs").transform;
				enemy.tag = "Enemy";
				yield return new WaitForSeconds(Random.Range(spawnDelay,spawnDelay+2));
			}

//				float skill = pScore / 10;
//				
//				if (skill < 2) {
//					waitTime = 20;
//				}
//				else if(skill > 2 && skill < 4){
//					waitTime = 30;
//				}
//				else if(skill > 4 && skill < 6){
//					waitTime = 30;
//				}

			yield return new WaitForSeconds(waveWait);
		}
	}
}
