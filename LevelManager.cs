using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {
		PaddleManage.autoPlay = false;
		Application.LoadLevel(name);
	}
	
	public void LoadGodMode (string name) {
		PaddleManage.autoPlay = true;
		Application.LoadLevel(name);
	}
	
	public void QuitReq(string name){
		Application.Quit();
	}
	
	public void LoadNextLevel() {
		Application.LoadLevel((Application.loadedLevel) + 1);
		LoseCollider.lives++;
	}
	
	public void WinCheck() {
		if(Brick.breakableCount < 1)
		{
			Brick.breakableCount = 0;
			LoadNextLevel();
		}
	}
}