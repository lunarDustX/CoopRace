using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEffect : MonoBehaviour {

    Text scoreText;
    public bool scoreUpdated;

    private int normalSize;
    public int maxSize;
    public int deltaSpeed;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        normalSize = scoreText.fontSize;
	}
	
	// Update is called once per frame
	void Update () {
        if (scoreUpdated) {
            if (scoreText.fontSize < maxSize)
            {
                int deltaSize = (int)Mathf.Lerp(scoreText.fontSize, maxSize, deltaSpeed*Time.deltaTime) - scoreText.fontSize;
                if (deltaSize < 1) deltaSize = 1;


                scoreText.fontSize += deltaSize;
                if (scoreText.fontSize == maxSize) {

                    scoreUpdated = false;
                }

            }
        } else if (scoreText.fontSize != normalSize) {
            int deltaSize = scoreText.fontSize - (int)Mathf.Lerp(scoreText.fontSize, normalSize, deltaSpeed*Time.deltaTime);
            if (deltaSize < 1) deltaSize = 1;

            scoreText.fontSize -= deltaSize;
        }
	}
}
