using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public void newGame() {
		Debug.Log ("New Game");
		Application.LoadLevel ("Main");
	}
	
}
