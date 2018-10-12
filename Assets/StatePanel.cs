using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePanel : MonoBehaviour {

    public Player player;
    Text stateText;
	// Use this for initialization
	void Start () {
        stateText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.outside) {
            stateText.text = "OUT";
        } else {
            stateText.text = "BED";
        }
	}
}
