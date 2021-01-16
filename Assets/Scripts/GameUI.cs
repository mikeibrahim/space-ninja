using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour {
	[SerializeField] private TMP_Text scoreText;
	[SerializeField] private TMP_Text comboText;
	[SerializeField] private TMP_Text timerText;
	[SerializeField] private TMP_Text lives;
	[SerializeField] private GameObject endPanel;
	[SerializeField] private TMP_Text endScore;
	int score;
	int time = 75;
	float currentTime;
	Player player;

	private void Start() {
		scoreText.text = "0";
		comboText.gameObject.SetActive(false);
		endPanel.SetActive(false);
		currentTime = time;
		player = GameObject.FindObjectOfType<Player>();
	}

	private void FixedUpdate() {
		if (!endPanel.activeInHierarchy) {
			if (currentTime >= 1) { 
				currentTime -= Time.deltaTime; 
			} else {
				player.Die();
			}
		}
		float minutes = Mathf.FloorToInt(currentTime / 60);  
		float seconds = Mathf.FloorToInt(currentTime % 60);
		string space = seconds < 10 ? "0" : "";
		
		timerText.text = minutes.ToString() + ":" + space + seconds.ToString();

	}

	private void Update() {
		if (endPanel.activeInHierarchy) {
			if (Input.GetKeyDown(KeyCode.H)) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
			} else if (Input.GetKeyDown(KeyCode.R)) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

    public void AddScore(int value) {
		score += value;
		scoreText.text = score.ToString();
	}

	public IEnumerator SetCombo(int value, Vector3 pos) {
		comboText.gameObject.transform.position = pos;
		ClampCombo();
		comboText.text = value.ToString();
		comboText.gameObject.SetActive(true);
		player.AddScore((int) ((value * value) / 2));
		print("Got: "+value);
		print("Added (no casting): "+ (value * value));
		print("Added: "+ (int) (value * value));
		print("AddedDivide: "+ (int) ((value * value) / 2));
		yield return new WaitForSeconds(1f);
		comboText.gameObject.SetActive(false);
	}

	private void ClampCombo() {
		var pos = comboText.transform.position;
		pos.x = Mathf.Clamp(pos.x, -9f, 9f);
		pos.y = Mathf.Clamp(pos.y, -3f, 4f);
		comboText.transform.position = pos;
	}

	public void SetLives(int value) { lives.text = value.ToString(); }
 
	public void ShowScore() {
		endPanel.SetActive(true);
		endScore.text = score.ToString();
		if (PlayerPrefs.GetInt("HighScore") < score) {
			PlayerPrefs.SetInt("HighScore", score);
		}
	}

	public GameObject GetScorePanel() { return endPanel; }
}
