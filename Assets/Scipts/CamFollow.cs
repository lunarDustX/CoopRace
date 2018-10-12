using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

    [Range(0f, 1f)]
    public float smoothFactor = .5f;
    public Transform boatTransform;
    private Vector3 camOffset;

	// Use this for initialization
	void Start () {
        camOffset = transform.position - boatTransform.position;
	}
	
	void FixedUpdate () {
        Vector3 newPos = boatTransform.position + camOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
    }
}
