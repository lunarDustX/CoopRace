using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {

    private int levels;
    private int levelIndex;
    public Text levelText;

	// Use this for initialization
	void Start () {
        levels = transform.childCount;
	}
	
	// Update is called once per frame
	void Update () {

        GamePadState state = GamePad.GetState(PlayerIndex.One);

        if (state.Pressed(CButton.LB)) {
            transform.GetChild(levelIndex).gameObject.SetActive(false);
            levelIndex--;
            if (levelIndex < 0) levelIndex = levels - 1;
            levelText.text = "LEVEL " + (levelIndex + 1);
            transform.GetChild(levelIndex).gameObject.SetActive(true);
        }

        if (state.Pressed(CButton.RB))
        {
            transform.GetChild(levelIndex).gameObject.SetActive(false);
            levelIndex++;
            if (levelIndex > levels-1) levelIndex = 0;
            levelText.text = "LEVEL " + (levelIndex + 1);
            transform.GetChild(levelIndex).gameObject.SetActive(true);
        }

        if (state.Pressed(CButton.A)) {
            SceneManager.LoadScene(levelIndex+1);
        }

        ModelRotate();
	}

    void ModelRotate() {
        transform.Rotate(Vector3.up * Time.deltaTime * 40);
    }
}
