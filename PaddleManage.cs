using UnityEngine;
using System.Collections;

public class PaddleManage : MonoBehaviour {

	public Vector2 offset;
	public static bool autoPlay;
	
	private Vector3 paddlePos;
	private Vector3 ballPos;
	private BallControl ballControl;
	private float paddleWidth;
	
	void Start () {
		ballControl = GameObject.FindObjectOfType<BallControl>();
		paddleWidth = gameObject.GetComponent<Renderer>().bounds.size.x;
		paddlePos = this.transform.position;
		if (autoPlay) {
			ballControl.LaunchBall ();
		}
	}
	
	void Update () {
		ballPos = ballControl.gameObject.transform.position;
		if(!autoPlay)
		{
			MoveWithMouse();
		}
		else
		{
			AutoPlay();
		}
		this.transform.position = paddlePos;
	}
	
	void MoveWithMouse() {
		float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
		paddlePos.x = Mathf.Clamp(mousePosInBlocks, 0.75f, 15.25f);
	}
	
	void AutoPlay() {
		paddlePos.x = Mathf.Clamp(ballPos.x, 0.75f, 15.25f);
	}
	
	void OnCollisionEnter2D (Collision2D incidence) {
		float xIncidence;
		if(autoPlay) {
			FindTargetAndRedirect ();
//			xIncidence = ((ballPos.x - paddlePos.x) + Random.Range(-0.5f, 0.5f));
			return;

		} else {
			xIncidence = (ballPos.x - paddlePos.x);
		}

		if(xIncidence > paddleWidth || xIncidence < -paddleWidth) {
			xIncidence = 0;
		}

		offset = new Vector2 (10f * xIncidence, ballControl.gameObject.GetComponent<Rigidbody2D>().velocity.y);
		ballControl.gameObject.GetComponent<Rigidbody2D>().velocity = offset;
	}

	private void FindTargetAndRedirect() {
		Vector2 target = GameObject.FindGameObjectWithTag ("Destructable").transform.position;
		Vector2 targetVector = new Vector2 (target.x - ballPos.x, target.y - ballPos.y);
		ballControl.gameObject.GetComponent<Rigidbody2D> ().velocity = targetVector;
	}
		
}
