using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Vector3 moveDir;
    private Rigidbody rb;
    public float speed;

    //public int gold = 1;
    public GameObject b;
    public GameObject p;

    public int playerNumber;

    private bool onGround;
    public int MAX_JUMP = 2;
    private int currentJumps;

    public LayerMask groundLayer;

    private string H_axisName;
    private string V_axisName;
    private string jumpButton;

    private float distToGround;
    private float poleLength = 1f;


	// Use this for initialization
	void Start () 
    {

        //distToGround = GetComponent<BoxCollider>().bounds.extents.y;

        rb = GetComponent<Rigidbody>();

        H_axisName = "Horizontal" + playerNumber;
        V_axisName = "Vertical" + playerNumber;
        jumpButton = "Jump" + playerNumber;

	}
	
	// Update is called once per frame
	void Update () 
    {
        float h = Input.GetAxisRaw(H_axisName);
        float v = Input.GetAxisRaw(V_axisName);
        moveDir = new Vector3(h, 0, v).normalized;

        // REBORN
        if (transform.position.y < -100f) {
            Reborn();
        }
	}

	private void FixedUpdate()
	{
        rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);

        if (Input.GetButtonDown(jumpButton) && (onGround || currentJumps < MAX_JUMP)) {
            rb.velocity = new Vector3(0, 6f, 0);
            currentJumps++;
            onGround = false;
        }
	}

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.collider.tag == "GROUND")
        {
            onGround = true;
            currentJumps = 0;
        }
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "GOLD") {
            //gold++;
            Destroy(other.gameObject);

            ScoreBoard.Instance.UpdateScore(playerNumber);
            //float s = Mathf.Sqrt(gold) * 5;
            //b.transform.localScale = new Vector3( s, 0.05f, s);
        }
	}

    void Reborn() {
        transform.position = new Vector3(0, 18f, 0);
        rb.velocity = Vector3.zero;

        poleLength -= 0.2f;
        Debug.Log(poleLength);
        poleLength = Mathf.Max(0.2f, poleLength);
        p.transform.localScale = new Vector3(1, poleLength, 1);
    }

    /*
    bool IsOnGround() {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f, groundLayer);
    }
    */
}
