using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSetting : MonoBehaviour {
    
	#region Singleton
    public static EffectSetting Instance;
	private void Awake()
	{
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
	}
    #endregion

    public bool punchCameraShake;
}
