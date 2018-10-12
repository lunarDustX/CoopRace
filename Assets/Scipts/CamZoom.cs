using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoom : MonoBehaviour {

    private Camera cam;

    public float maxZoom;
    public float minZoom;

    private GameObject[] targets;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        targets = GameObject.FindGameObjectsWithTag("Player");
	}

	private void FixedUpdate()
	{
        Zoom();
	}

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatDistance()/20f);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.fixedDeltaTime);
    }

    private float GetGreatDistance() {
        float dis = 0f;
        for (int i = 0; i < targets.Length; i++) {
            for (int j = i+1; j < targets.Length; j++) {
                //if (targets[i].ac)
                if (Vector3.Distance(targets[i].transform.position, targets[j].transform.position) > dis) {
                    dis = Vector3.Distance(targets[i].transform.position, targets[j].transform.position);
                }
            }
        }
        return dis;
    }
}
