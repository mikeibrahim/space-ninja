using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] private Sword sword;
	private float comboTime = 0.5f;
	private int lives = 3;
	private Vector3 post = new Vector3(0,0,0);
	private Vector3 noPost = new Vector3(1.5f,0,0);
	GameUI gameUI;
	float currComboTime;
	int currCombo;
	GameObject volume;
	bool comboBool;

	void Start() {
		gameUI = GameObject.FindObjectOfType<GameUI>();
		gameUI.SetLives(lives);
		volume = GameObject.Find("PlayerVolume");
	}

    void Update() {
		if (Input.GetMouseButtonDown(0)) {
			sword.transform.position = sword.MousePos();
		}
		sword.gameObject.SetActive(Input.GetMouseButton(0) && lives > 0 && !gameUI.GetScorePanel().activeInHierarchy);
		
		currComboTime -= Time.deltaTime;

		if (currCombo >= 3) { StartCoroutine(PostP()); }

		if (currComboTime <= 0) {
			if (currCombo >= 3) {
				StartCoroutine(gameUI.SetCombo(currCombo, sword.MousePos())); // Set GamUI Combo
			}
			currCombo = 0;
		}

		volume.transform.localPosition = Vector3.MoveTowards(
			volume.transform.localPosition, 
			comboBool ? post : noPost, 
			Time.deltaTime * 5);
    }

	private IEnumerator PostP() {
		comboBool = true;
		yield return new WaitForSeconds(0.25f);
		Time.timeScale = 0.5f;
		yield return new WaitForSeconds(0.25f);
		Time.timeScale = 1f;
		comboBool = false;
	}

	public void AddScore(int amount) { // Get Score whenever you slice a ball
		gameUI.AddScore(amount);
	}

	public void ResetCombo() {
		currCombo += 1;
		currComboTime = comboTime;
	}

	public void LoseLife() { // When a ball goes out of bounds
		lives -= 1;
		gameUI.SetLives(lives);
		if (lives <= 0) { Die(); }
	}

	public void Die() {
		EnemyGenerator eGen = GameObject.FindObjectOfType<EnemyGenerator>();
		eGen.gameObject.SetActive(false);
		Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
		foreach (Enemy e in enemies) { e.Particles(); }
		gameUI.ShowScore(); 
	}
}