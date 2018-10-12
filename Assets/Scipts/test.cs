using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public GameObject DashEffectPrefab;

    private void Start()
    {
        InvokeRepeating("SpawnDashEffect", .5f, 2f);
    }

    void SpawnDashEffect()
    {
        Instantiate(DashEffectPrefab, new Vector3(3, 3, 3), Quaternion.identity);
    }
}
