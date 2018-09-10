using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Vector3 moveDir;
    private Rigidbody rb;
    public float moveSpeed;

    public GameObject b;
    public GameObject p;
    public GameObject DashEffectPrefab;

    public int playerNumber;

    public int MAX_JUMP = 2;
    public int MAX_DASH = 2;
   
    private bool onGround;
    private int currentJump;
    private int currentDash;
    private bool jump = false;

    private float dashTime;
    public float startDashTime;
    public float dashSpeed;

    public LayerMask groundLayer;

    private string H_axisName;
    private string V_axisName;
    private string jumpButton;

    private float distToGround;

    private float poleLength = 1f;
    private float deathPenaltyLength = 0.15f;
    private float minLength = 0.4f;

    private Transform spawnPoint;

	// Use this for initialization
	void Start () 
    {
        //distToGround = GetComponent<BoxCollider>().bounds.extents.y;
        spawnPoint = GameObject.Find("SpawnPoint").transform;

        rb = GetComponent<Rigidbody>();

        H_axisName = "Horizontal" + playerNumber;
        V_axisName = "Vertical" + playerNumber;
        jumpButton = "Jump" + playerNumber;

        dashTime = 0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //float h = Input.GetAxisRaw(H_axisName);
        //float v = Input.GetAxisRaw(V_axisName);

        float h = 0, v = 0;
        GamePadState state;
        jump = false;

        switch (playerNumber) {
            case 1: 
                h = GamePad.GetAxis(CAxis.LX, PlayerIndex.One);
                v = -GamePad.GetAxis(CAxis.LY, PlayerIndex.One);
                state = GamePad.GetState(PlayerIndex.One);
                if (state.Pressed(CButton.A)) jump = true;

                if (currentDash < MAX_DASH)
                {
                    if (state.Pressed(CButton.B)) 
                    { 
                        dashTime = startDashTime; 
                        currentDash++; 
                        Instantiate(DashEffectPrefab, transform.position, Quaternion.identity);
                    }
                }
                break;

            case 2:
                h = GamePad.GetAxis(CAxis.LX, PlayerIndex.Two);
                v = -GamePad.GetAxis(CAxis.LY, PlayerIndex.Two);
                state = GamePad.GetState(PlayerIndex.Two);
                if (state.Pressed(CButton.A)) jump = true;
                if (currentDash < MAX_DASH)
                {
                    if (state.Pressed(CButton.B))
                    {
                        dashTime = startDashTime;
                        currentDash++;
                        Instantiate(DashEffectPrefab, transform.position, Quaternion.identity);
                    }
                }
                break;
            case 3:
                h = GamePad.GetAxis(CAxis.LX, PlayerIndex.Three);
                v = -GamePad.GetAxis(CAxis.LY, PlayerIndex.Three);
                state = GamePad.GetState(PlayerIndex.Three);
                if (state.Pressed(CButton.A)) jump = true;
                if (currentDash < MAX_DASH)
                {
                    if (state.Pressed(CButton.B))
                    {
                        dashTime = startDashTime;
                        currentDash++;
                        Instantiate(DashEffectPrefab, transform.position, Quaternion.identity);
                    }
                }
                break;
            case 4:
                h = GamePad.GetAxis(CAxis.LX, PlayerIndex.Four);
                v = -GamePad.GetAxis(CAxis.LY, PlayerIndex.Four);
                state = GamePad.GetState(PlayerIndex.Four);
                if (state.Pressed(CButton.A)) jump = true;
                if (currentDash < MAX_DASH)
                {
                    if (state.Pressed(CButton.B))
                    {
                        dashTime = startDashTime;
                        currentDash++;
                        Instantiate(DashEffectPrefab, transform.position, Quaternion.identity);
                    }
                }
                break;
        }
        moveDir = new Vector3(h, 0, v).normalized;

        // REBORN
        if (transform.position.y < -100f) {
            Reborn();
        }
	}

	private void FixedUpdate()
	{
        // MOVE
        if (moveDir.magnitude > 0)
        {
            rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);
        }

        // JUMP
        if (jump) {   //if (Input.GetButtonDown(jumpButton)) {
            if (onGround || currentJump < MAX_JUMP) 
            {
                rb.velocity = new Vector3(0, 6f, 0);
                currentJump++;
                onGround = false;
                jump = false;
            }
        }

        // DASH
        if (dashTime > 0) {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0) {
                rb.velocity = Vector3.zero;
            } else {
                rb.velocity = transform.up * dashSpeed; 
            }
        } 
	}

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.collider.tag == "GROUND")
        {
            onGround = true;
            currentJump = 0;
            currentDash = 0;
        }
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "GOLD") 
        {
            Destroy(other.gameObject);
            // Score Effect

            ScoreBoard.Instance.UpdateScore(playerNumber);
            //float s = Mathf.Sqrt(gold) * 5;
            //b.transform.localScale = new Vector3( s, 0.05f, s);
        }
	}

    void Reborn() {
        transform.position = spawnPoint.position;
        rb.velocity = Vector3.zero;

        poleLength -= deathPenaltyLength;
        poleLength = Mathf.Max(minLength, poleLength);
        p.transform.localScale = new Vector3(1, poleLength, 1);
    }

    /*
    bool IsOnGround() {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f, groundLayer);
    }
    */
}
