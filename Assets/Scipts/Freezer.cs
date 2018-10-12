using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour {

    public static Freezer Instance;

	#region Singleton
	private void Awake()
	{
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
	}

	#endregion

	[Range(0f, 1f)]
    public float duration;
    public float frozeScale;

    public void Freeze(float t) {
        duration = t;
        StartCoroutine("DoFreeze");
    }

    IEnumerator DoFreeze() {
        float original = 1f;
        Time.timeScale = frozeScale;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
    }
}
