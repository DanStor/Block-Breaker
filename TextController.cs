using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {

	private Text lifeText;

	// Use this for initialization
	void Start () {
		lifeText = GameObject.FindObjectOfType<Text>();
		UpdateLives();
	}
	
	public void UpdateLives() {
		lifeText.text = "Lives: " + LoseCollider.lives;
	}
}
