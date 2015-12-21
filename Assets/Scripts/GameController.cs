using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	public GameObject hazzard;
	public Vector3 spanwValues;
	public int hazzardCount;
	public float spawnWait;
	public float waveWait;
	public float startWait;
	public Text scoreText;
	private int score;

	public Text restartText;
	public Text gameOverText;

	private bool gameOver;
	private bool restart;

	private int waveCounter;
	
	void Start() {
		Debug.Log ("Start GameController");
		StartCoroutine (spanwWaves ());
		score = 0;
		updateScore ();
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		waveCounter = 1;
	}

	IEnumerator spanwWaves(){

		yield return new WaitForSeconds (startWait);

		while (true) {
			waveCounter++;
			for(int i = 0; i < hazzardCount; i++){				
				Vector3 spanwPosition = new Vector3 (Random.Range(-spanwValues.x, spanwValues.x), spanwValues.y, spanwValues.z);
				Quaternion spanwRotation = Quaternion.identity;
				GameObject hazzardClone = Instantiate (hazzard, spanwPosition,spanwRotation) as GameObject;
				Mover mover = hazzardClone.GetComponent<Mover>();
				Debug.Log("Wave counter "+waveCounter);
				mover.speed = mover.speed*waveCounter;
				yield return new WaitForSeconds (spawnWait);	
			}
			
			yield return new WaitForSeconds (waveWait);	
		
			if (gameOver == true) {
				restart = true;
				break;
			}
		}
	}

	void Update(){
		if (restart == true) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
	
	void updateScore() {
		scoreText.text = "Score " + score;
	}

	public void addScore(int newScore) {
		score += newScore;
		updateScore ();
	}

	public void GameOver() {
		gameOverText.text = "Game Over";
		gameOver = true;
	}
}
