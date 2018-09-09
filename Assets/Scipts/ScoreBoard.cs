using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
    
#region Singleton
    public static ScoreBoard Instance;
	private void Awake()
	{
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("ERROR!");
            return;
        }
	}
#endregion

    public Text redText;
    public Text blueText;

    private int redScore;
    private int blueScore;

    //private AudioSource audioSource;

	private void Start()
	{
        //audioSource = GetComponent<AudioSource>();
	}

	public void UpdateScore(int playerNumber) {

        if (playerNumber < 3)
        {
            redScore++;
            redText.text = redScore.ToString();
        }
        else
        {
            blueScore++;
            blueText.text = blueScore.ToString();
        }

        Crowd.Instance.Cheer();
    }
}
