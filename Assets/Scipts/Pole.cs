using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour {

    private float distToGround;
	// Use this for initialization
	void Start () {
        distToGround = GetComponent<BoxCollider>().bounds.extents.y;
        Debug.Log(distToGround);
	}
	
    public bool IsOnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }
}
