using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	void Update () {
        if (Input.GetButtonDown("Restart")) {
            SceneManager.LoadScene(0);
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
