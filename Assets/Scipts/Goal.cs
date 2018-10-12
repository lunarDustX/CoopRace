using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public enum TEAM {RED, BLUE}

    public TEAM team;

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.collider.tag == "Tableware") {
            
            int v = collision.collider.GetComponent<Tableware>().value;
            if (team == TEAM.RED) {
                ScoreBoard.Instance.UpdateScore(3, v);
            } else {
                ScoreBoard.Instance.UpdateScore(1, v);
            }
        }
	}
}
