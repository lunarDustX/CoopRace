using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLogic : MonoBehaviour {
    // Update is called once per frame
    #region Singleton
    public static GameLogic Instance;
	private void Awake()
	{
        if (Instance == null) {
            Instance = this;
        }
        return;
	}
    #endregion

    public bool gameOn = false;
    public bool gameOver = false;

    public Player[] playerComponents;
    public GameObject resultText;

    public GameObject winEffectPrefab;

	void Update () {

        // INPUT
        if (Input.GetButtonDown("Restart")) {
            SceneManager.LoadScene(0);
        }	

        // GAME OVER SITUATIONS
        if (LevelSettings.Instance.gameMode == LevelSettings.GAMEMODE.TWO && !gameOver) {
            if (playerComponents[0].outside && playerComponents[1].outside) {
                Instantiate(winEffectPrefab, new Vector3(0, 12, 0), Quaternion.identity);
                gameOver = true;
                resultText.SetActive(true);
                resultText.GetComponent<TextMeshProUGUI>().text = "BLUE WIN!";
                resultText.GetComponent<AnimatedEntry>().Appear();
            }

            if (playerComponents[2].outside && playerComponents[3].outside)
            {
                Instantiate(winEffectPrefab, new Vector3(0, 12, 0), Quaternion.identity);
                gameOver = true;
                resultText.SetActive(true);
                resultText.GetComponent<TextMeshProUGUI>().text = "RED WIN!";
                resultText.GetComponent<AnimatedEntry>().Appear();
            }
        }
	}

    public void GameStart() {
        gameOn = true;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++) {
            players[i].GetComponent<Player>().enabled = true;
        }
    }
}
