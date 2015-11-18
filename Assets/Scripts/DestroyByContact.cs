using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;


	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");

		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>(); 
		}else{
			Debug.Log("Error no  game controller");
		}

	}


    void OnTriggerEnter(Collider other)
    {
		Debug.Log("OnTriggerEnter Asteroid -> "+other.gameObject.tag);

        if (other.gameObject.tag == "Boundary")
        {
            return;
        }

        Instantiate(explosion, transform.position, transform.rotation);

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Destroy player");
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
		gameController.addScore (scoreValue);
    }




}
