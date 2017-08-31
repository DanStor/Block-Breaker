using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public Sprite[] hitSprites;
	public static int breakableCount;
	public AudioClip smash;
	public float brickVelocity;
	public BallControl ballControl;
	public GameObject smoke;
	
	private int hitCount;
	private LevelManager levelManager;
	private bool isBreakable;

	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		ballControl = GameObject.FindObjectOfType<BallControl>();
		isBreakable = (this.tag == "Destructable");
		if(isBreakable)
		{
			breakableCount++;
		}
		hitCount = 0;
	}
	
	void OnCollisionExit2D (Collision2D hit) {
		if(isBreakable) {
			DamageCheck();
			VelocityChange();	
		}	
	}
	
	void DamageCheck() {
		hitCount++;
		int maxHits = hitSprites.Length + 1;
		
		if(hitCount >= maxHits)
		{
			PuffSmoke();
			AudioSource.PlayClipAtPoint(smash, gameObject.transform.position);
			Destroy(gameObject);
			breakableCount--;
		}
		else
		{
			LoadSprite();
		}
		levelManager.WinCheck();
	}
	
	// Changes ball Y velocity by float number set in inspector. Green = 10, yellow = 12, red = 14
	void VelocityChange() {
		if(ballControl.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
		{
			ballControl.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (ballControl.gameObject.GetComponent<Rigidbody2D>().velocity.x, brickVelocity);
		}
		else
		{
			ballControl.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (ballControl.gameObject.GetComponent<Rigidbody2D>().velocity.x, -brickVelocity);
		}
	}
	
	void PuffSmoke() {
		GameObject smokePuff = Instantiate(smoke, gameObject.transform.position, Quaternion.identity) as GameObject;
		smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	void LoadSprite() {
		int spriteIndex = hitCount - 1;
		if(hitSprites[spriteIndex])
		{
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
		else
		{
			Debug.LogError("Missing Sprite");
		}
	}
}