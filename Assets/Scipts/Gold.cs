using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Gold : MonoBehaviour {

    public GameObject fireworksPrefab;

    public int value;

    public GameObject IslandPrefab;

    private GameObject raft;

    private GameObject island; 
    // Use this for initialization
	void Start () {

        raft = GameObject.FindWithTag("Ground"); 

        switch (LevelSettings.Instance.level) {
            case LevelSettings.LEVEL.RAFT:
                Vector3 pos = new Vector3(transform.position.x, raft.transform.position.y, transform.position.z);
                island = Instantiate(IslandPrefab, pos, Quaternion.identity);
                ArenaMovement.Instance.Islands.Add(island.transform);
                break;
        }
	}
	
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * 50f);
	}

    private void OnTriggerEnter(Collider other)
    {
        int player = other.GetComponentInParent<Player>().playerNumber;
        ScoreBoard.Instance.UpdateScore(player, value);
        Crowd.Instance.Cheer();

        Freezer.Instance.Freeze(0.02f);
        CameraShaker.Instance.ShakeOnce(2f, 2f, 0.2f, 0.4f);
        Instantiate(fireworksPrefab, transform.position, Quaternion.identity);

        ArenaMovement.Instance.Islands.Remove(island.transform);
        Destroy(island);

        Destroy(gameObject);
    }

}
