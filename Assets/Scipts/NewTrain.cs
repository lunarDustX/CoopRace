using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTrain : MonoBehaviour {
    
    public Vector3 endPoint;
    public bool moving = false;
    private int pointIndex;
    public Transform[] TrackPoints;

	// Use this for initialization
	void Start () {
        pointIndex = 1;
        endPoint = TrackPoints[1].position;
	}
	
	// Update is called once per frame
	void Update () {

        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, Time.deltaTime * 60f);
            if (transform.position == endPoint)
            {
                moving = false;
                endPoint = TrackPoints[1-pointIndex].position;
                pointIndex = 1 - pointIndex;
            }
        }
	}
}
