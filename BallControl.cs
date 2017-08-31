using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {
	private PaddleManage paddle;
	private Vector3 touchVector;
	private bool hasStarted = false;
	private float maxVely = 10;

	// Use gameObject for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<PaddleManage>();
		touchVector = gameObject.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!hasStarted)
		{
			gameObject.transform.position = paddle.transform.position + touchVector;
			if(Input.GetMouseButtonUp(0))
			{
				LaunchBall();
			}
		}
		if(Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) < maxVely)
		{
			SlowCheck();
		}
		
	}
	
	public void ResetBall() {
		hasStarted = false;
		gameObject.transform.position = paddle.transform.position + touchVector;
	}
	
	void OnCollisionExit2D (Collision2D boing) {
		if (!PaddleManage.autoPlay) {
			Vector2 tweak = new Vector2 (Random.Range (-0.5f, 0.5f), Random.Range (0f, 0.5f));
			if (hasStarted) {
				gameObject.GetComponent<Rigidbody2D> ().velocity += tweak;
				FastCheck ();
			}
		}
	}
	
	public void LaunchBall() {
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (2f, maxVely);
		hasStarted = true;
	}
	
	void FastCheck() {
		if(gameObject.GetComponent<Rigidbody2D>().velocity.y >= 15)
		{
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (gameObject.GetComponent<Rigidbody2D>().velocity.x, 15);
		}
	}
	
	void SlowCheck() {
		if(gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
		{
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (gameObject.GetComponent<Rigidbody2D>().velocity.x, maxVely);
		}
		else
		{
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (gameObject.GetComponent<Rigidbody2D>().velocity.x, -maxVely);
		}
	}
}