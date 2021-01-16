using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour {
	[SerializeField] private Button start;
	[SerializeField] private TMP_Text highScore;
	[SerializeField] private GameObject infoGO;
	[SerializeField] private Button info;

    void Start() {
        start.onClick.AddListener(delegate{StartGame();});
		highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        info.onClick.AddListener(delegate{infoGO.SetActive(!infoGO.activeInHierarchy);});
    }

	private void StartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
