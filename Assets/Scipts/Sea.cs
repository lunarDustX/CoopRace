using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour {

    public GameObject WavePrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("PlayerPole") || other.CompareTag("PlayerBase")) {
            //Instantiate(other.col)
        }
	}
}
