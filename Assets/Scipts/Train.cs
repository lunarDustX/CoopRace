using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    public Transform[] TrainPoints;
    private int pointIndex = 1;
    public float speed = 10f;
    private Rigidbody rb;
    private bool moving = true;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (transform.position == TrainPoints[pointIndex].position) {
            pointIndex++;
            if (pointIndex == TrainPoints.Length) pointIndex = 0;
            //transform.forward = (TrainPoints[pointIndex].position - transform.position).normalized;

            Stop();
        }

        if (moving)
        {
            Vector3 pos = Vector3.Lerp(transform.position, TrainPoints[pointIndex].position, speed * Time.deltaTime);
            rb.MovePosition(pos);
        } else {
            // rotating
            transform.Rotate(Vector3.down *Time.deltaTime *90);
        }

	}

    void Stop() {
        moving = false;
        Invoke("StopEnd",1);
    }

    void StopEnd() {
        moving = true;
    }
}
