using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour {

    public static Crowd Instance;

    public AudioClip a_cheer;
    private AudioSource audioSource;

	private void Awake()
	{
        if (Instance == null) {
            Instance = this;
        }
        return;
	}

	private void Start()
	{
        audioSource = GetComponent<AudioSource>();
	}

    public void Cheer() {
        audioSource.PlayOneShot(a_cheer);
    }
}
