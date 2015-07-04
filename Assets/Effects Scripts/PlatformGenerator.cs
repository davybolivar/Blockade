using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {
	
	public float tileWidth;
	public float tileHeight;
	public float posX = 45.092f;
	public float posY = 3.26f;

	public GameObject[] tiles;

	public int[,] mapTile;
	

	private static PlatformGenerator _instance;
	
	public static PlatformGenerator instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<PlatformGenerator>();
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

	void Awake(){

		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
		
		mapTile = new int[,]{ 
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
			{ 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0},  
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0}, 
			{ 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 0, 0, 0}, 
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0}, 
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0}, 
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0},  
			{ 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0},  
			{ 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0}, 
			{ 0, 0, 0, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0},
			{ 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
			{ 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},  
			{ 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},  
			{ 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0}, 
			{ 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0}, 
			{ 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0},  
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0}, 
			{ 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0}, 
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0},  
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0},  
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},  
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, 
		};
	
		tileWidth = tiles[0].transform.GetComponent<Renderer> ().bounds.size.x;
		tileHeight = tiles[0].transform.GetComponent<Renderer> ().bounds.size.y;
		GeneratePlat ();
	}


	// Use this for initialization
	void Start () {

//		int rowSize = mapTile.GetLength (1);
//		int colSize = mapTile.GetLength (0);
//		rowSize--;
//		colSize--;
//
//		float origPosY = posY;
//
//		for (int row = 0; row <= rowSize; row++) {
//
//			for(int col = 0; col <= colSize; col++){
//				SetTile (mapTile[col, row],posX,posY);
//				posY -= tileHeight;
//			}
//
//			posY = origPosY;
//			posX += tileWidth;
//		}

	}

	void GeneratePlat(){
		int rowSize = mapTile.GetLength (1);
		int colSize = mapTile.GetLength (0);
		rowSize--;
		colSize--;
		
		float origPosY = posY;
		
		for (int row = 0; row <= rowSize; row++) {
			
			for(int col = 0; col <= colSize; col++){
				SetTile (mapTile[col, row],posX,posY);
				posY -= tileHeight;
			}
			
			posY = origPosY;
			posX += tileWidth;
		}
	}

	void SetTile(int tileType, float xpos, float ypos){
		if (tileType == 1) {
			int tileNum = Random.Range (0, 10);
//			GameObject tileClone = (GameObject)Instantiate (tiles [tileType], new Vector3 (xpos, ypos, 1f), Quaternion.identity);
			GameObject tileClone = (GameObject)Instantiate (tiles [tileNum], new Vector3 (xpos, ypos, 1f), Quaternion.identity);
			tileClone.transform.parent = GameObject.Find ("Map").transform;
			tileClone.tag = "Platform";
		} else if (tileType == 2) {
			GameObject tileClone = (GameObject)Instantiate (tiles [10], new Vector3 (xpos, ypos, 1f), Quaternion.identity);
			tileClone.transform.parent = GameObject.Find ("Map").transform;
			tileClone.tag = "BottomLadder";
		} else if (tileType == 4) {
			GameObject tileClone = (GameObject)Instantiate (tiles [11], new Vector3 (xpos, ypos, 1f), Quaternion.identity);
			tileClone.transform.parent = GameObject.Find ("Map").transform;
			tileClone.tag = "Ladder";
		} else if (tileType == 3) {
			GameObject tileClone = (GameObject)Instantiate (tiles [12], new Vector3 (xpos, ypos, 1f), Quaternion.identity);
			tileClone.transform.parent = GameObject.Find ("Map").transform;
			tileClone.tag = "TopLadder";
		} else if (tileType == 5) {
			GameObject tileClone = (GameObject)Instantiate (tiles [13], new Vector3 (xpos, ypos, 1f), Quaternion.identity);
			tileClone.transform.parent = GameObject.Find ("Map").transform;
			tileClone.tag = "SpawnPoints";
		}

	}

}
