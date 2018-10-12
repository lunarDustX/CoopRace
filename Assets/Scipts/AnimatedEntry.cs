using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedEntry : MonoBehaviour {

    public float effectTime = 1f;
    public float delay = 0f;

    //[Space(0)]
    [Header("Scale")]
    public Vector3 startScale;
    public AnimationCurve scaleCurve;

    [Header("Position")]
    public Vector3 startPos;
    public AnimationCurve posCurve;

    Vector3 endPos;
    Vector3 endScale;

	// Use this for initialization
	void Start () {
        
	}

    public void Appear() {
        SetupVars();
        StartCoroutine(Animation());
    }
	
    void SetupVars() {
        endScale = transform.localScale;
        endPos = transform.localPosition;
        startPos += endPos;
    }

    IEnumerator Animation() {
        transform.localPosition = startPos;
        transform.localScale = startScale;
        yield return new WaitForSecondsRealtime(delay);
        float time = 0f;
        float perc = 0f;
        float lastTime = Time.realtimeSinceStartup;
        do
        {
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            perc = Mathf.Clamp01(time/effectTime);
            transform.localScale = Vector3.LerpUnclamped(startScale, endScale, scaleCurve.Evaluate(perc));
            transform.localPosition = Vector3.LerpUnclamped(startPos, endPos, posCurve.Evaluate(perc));
            yield return null;
        } while (perc < 1);
    }
}
