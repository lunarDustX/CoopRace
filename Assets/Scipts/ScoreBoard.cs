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
            Destroy(this);
        }
	}
#endregion

    /*
    public Text redText;
    public Text blueText;

    private int redScore;
    private int blueScore;
    */
    
    [SerializeField]
    private int[] playerScore;
    //private AudioSource audioSource;

	private void Start()
	{
        //audioSource = GetComponent<AudioSource>();
	}

    public void UpdateScore(int playerNumber, int score) {

        playerScore[playerNumber - 1]+= score;
        int s = playerScore[playerNumber - 1];

        transform.GetChild(playerNumber - 1).GetComponentInChildren<Text>().text = s.ToString();
        transform.GetChild(playerNumber - 1).GetComponentInChildren<ScoreEffect>().scoreUpdated = true;
                 
        /*
        if (playerNumber < 3)
        {
            redScore += score;
            redText.text = redScore.ToString();
        }
        else
        {
            blueScore += score;
            blueText.text = blueScore.ToString();
        }
        */
    }
}
