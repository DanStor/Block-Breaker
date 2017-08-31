using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

	private LevelManager lvlmngr;
	private TextController textControl;
	private BallControl ballControl;
	
	public static int lives = 3;
	
	void Start ()
	{
		lvlmngr = GameObject.FindObjectOfType<LevelManager>();
		textControl = GameObject.FindObjectOfType<TextController>();
		ballControl = GameObject.FindObjectOfType<BallControl>();
	}
	
	void OnTriggerEnter2D (Collider2D trig)
	{
		print ("Trigger");
		lives--;
		textControl.UpdateLives();
		if(lives < 1)
		{
			Brick.breakableCount = 0;
			lives = 3;
			lvlmngr.LoadLevel("Lose");
		}
		else
		{
			ballControl.ResetBall();
		}
	}
}