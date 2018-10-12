using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaMovement : MonoBehaviour {

    #region Singleton
    public static ArenaMovement Instance;
	private void Awake()
	{
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
	}
    #endregion

    public GameObject linePrefab;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private Vector3 targetPos;

    private bool moving;

    public int index;
    public Transform[] Destinations;
    public List<Transform> Islands = new List<Transform>();
    public float speed;

    [SerializeField]
    Transform platform;

    float lerpTime;
    float currentTime;

    Rigidbody platformRB;
	// Use this for initialization
	void Start () {
        index = -1;

        moving = true;

        platformRB = platform.GetComponent<Rigidbody>();

        startPosition = platform.position;
        endPosition = startPosition;//Destinations[index].position;
	}

	private void Update()
	{
        Vector3 moveDir = startPosition-endPosition;
        platform.rotation = Quaternion.Slerp(platform.rotation, Quaternion.LookRotation(moveDir), 0.01f);
	}

	private void FixedUpdate()
	{
        
        if (platform.position == endPosition)
        {
            //index = (index + 1) % Destinations.Length;

            if (Islands.Count > 1 || (Islands.Count == 1 && platform.position != Islands[0].position)) {
                int i = Random.Range(0, Islands.Count);
                while (Islands[i].position == platform.position) {
                    i = Random.Range(0, Islands.Count);
                }

                index = i;
                startPosition = endPosition;
                endPosition = Islands[index].position;//Destinations[index].position;

                DisplayRoute();


                //platform.forward = startPosition-endPosition;

                lerpTime = (endPosition - startPosition).magnitude / 1f;
                currentTime = 0f;

                moving = true;
            } else {
                moving = false;
            }
        }

        if (moving)
        {
            currentTime += Time.fixedDeltaTime;
            if (currentTime >= lerpTime)
            {
                currentTime = lerpTime;
            }

            float perc = currentTime / lerpTime;
            perc = perc * perc * (3f - 2f * perc);

            targetPos = Vector3.Lerp(startPosition, endPosition, perc);

            platformRB.MovePosition(targetPos);
        }
	}

    void DisplayRoute() {

        Vector3 dir = (endPosition - startPosition).normalized;
        float len = Mathf.Floor((endPosition - startPosition).magnitude);
        for (int i = 0; i < len; i++) {
            Vector3 pos = startPosition + dir * (i+1);
            if ((i + 1) % 2 == 0)
            {
                GameObject line = Instantiate(linePrefab, pos, Quaternion.identity);
                line.transform.LookAt(endPosition);
            }
            //line.transform.rotation = 
        }
    }

}
