using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    
    [Range(0.1f, 5f)]
    public float WaitBetweenWobbles;

    [Range(1f, 50f)]
    public float Intensity;

    Quaternion _targetAngle;
	// Use this for initialization
	void Start () {
        InvokeRepeating("ChangeTargetAngle", 0, WaitBetweenWobbles);
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetAngle, Time.deltaTime);
	}

    void ChangeTargetAngle() {
        var intensity = Random.Range(0.1f, Intensity);
        var curve = Mathf.Sin(Random.Range(0, Mathf.PI * 2));
        _targetAngle = Quaternion.Euler(Vector3.forward * curve * intensity);
    }
}
