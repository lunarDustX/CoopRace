using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSystem : MonoBehaviour {

    public GameObject[] Trains;
    //public NewTrain train;
    private GameObject train;
    public AudioClip a_train_alert;
    private AudioSource audioSource;

    public int minTrainInterval;
    public int maxTrainInterval;

	// Use this for initialization
	void Start () {
        Trains = GameObject.FindGameObjectsWithTag("Train");
        audioSource = GetComponent<AudioSource>();
        Invoke("TrainCome", 20);
	}
	

    void TrainCome() {
        // select a train
        int index = Random.Range(0, Trains.Length);
        train = Trains[index];

        // play train sound
        audioSource.PlayOneShot(a_train_alert);

        // train start
        Invoke("ActiveTrain", 3);

        // next train
        Invoke("TrainCome", 3 + Random.Range(minTrainInterval, maxTrainInterval));
    }

    void ActiveTrain() {
        train.GetComponent<NewTrain>().moving = true;
    }
}
