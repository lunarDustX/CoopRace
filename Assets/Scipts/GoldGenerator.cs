using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldGenerator : MonoBehaviour {

    public GameObject goldPrefab;
    public float w;
    public float h;
    public float spawnTime;
    public float height;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Generate", 10, spawnTime);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Generate() 
    {
        float x = Random.Range(-w, w);
        float z = Random.Range(-h, h);
        Vector3 pos = new Vector3(x, height, z);
        Instantiate(goldPrefab, pos, Quaternion.identity);
    }
}
