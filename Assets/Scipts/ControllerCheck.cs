using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCheck : MonoBehaviour {

    private string[] joystickNames;
	// Use this for initialization
	void Start () {
        joystickNames = Input.GetJoystickNames();
        for (int i = 0; i < 4; i++) {
            if (i >= joystickNames.Length) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
