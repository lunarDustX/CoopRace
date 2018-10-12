using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Player : MonoBehaviour {

    #region var defination
    // INPUT
    private bool jumpButton = false;
    private bool dashButton = false;

    private string H_axisName;
    private string V_axisName;
    private string jumpButtonName;
    //

    [Header("Basics")]
    public int playerNumber;
    public bool outside;
    private Vector3 moveDir;
    private Rigidbody rb;
    private bool alive;
    public float rebornTime;
    public float poleLength;
    private float deathPenaltyLength = 0.15f;
    private float minLength = 0.4f;

    public GameObject b;
    public GameObject p;

    private float distToGround;
    private Transform spawnPoint;

    [Header("Movements")]
    public float moveSpeed;
    public int MAX_JUMP;
    public int MAX_DASH;
    public float jumpHeight;
   
    private float dashTime;
    public float startDashTime;
    public float dashSpeed;

    public bool onGround;
    private int currentJump;
    private int currentDash;

    [Header("Sounds")]
    public AudioClip[] AttackSounds;
    private AudioSource audioSource;
    private AudioClip a_dash;
    public AudioClip a_land;
    public float landVolume;
    public float dashVolume;

    [Header("Effects")]
    public GameObject DashEffectPrefab;
    public GameObject DustBigEffectPrefab;
    public GameObject DustSmallEffectPrefab;
    public GameObject hitEffectPrefab;
    public GameObject deadEffectPrefab;

    [Header("Materials")]
    public Material playerMaterial;
    public Material dashMat;
    public Material defaultMat;

    #endregion

    // Use this for initialization
    void Start () 
    {
        alive = true;
        //distToGround = GetComponent<BoxCollider>().bounds.extents.y;
        b.transform.GetChild(0).GetComponent<MeshRenderer>().material = playerMaterial;

        p.transform.localScale = new Vector3(1, poleLength, 1);

        audioSource = GetComponent<AudioSource>();
        a_dash = AttackSounds[playerNumber];

        spawnPoint = GameObject.Find("SpawnPoint").transform;

        rb = GetComponent<Rigidbody>();

        //H_axisName = "Horizontal" + playerNumber;
        //V_axisName = "Vertical" + playerNumber;
        //jumpButtonName = "Jump" + playerNumber;

        dashTime = 0f;
	}
	
	// Update is called once per frame
	void Update () 
    {

        InputDection();

        if (dashButton) {
            if (currentDash < MAX_DASH) {
                Dash();
            }
        }

        // Dead 
        if (IsDead() && alive)
        {
            PlayerDie();

        }

        // OffScreen
        if (OffScreen()) {
            //Debug.Log(playerNumber);
        }
	}

    private void PlayerDie() {
        // EFFECT && SOUND
        Instantiate(deadEffectPrefab, transform.position, Quaternion.identity);

        transform.localScale = Vector3.zero;
        alive = false;
        if (LevelSettings.Instance.permanentDeath == false)
        {
            RebornInSeconds(rebornTime);
        }
    }

    private bool OffScreen() {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1) return false;
        return true;
    }

    private void Dash() {
        dashTime = startDashTime;
        currentDash++;
        GameObject dash = Instantiate(DashEffectPrefab, transform.position, Quaternion.identity);
        //dash.transform.rotation = Quaternion.LookRotation(transform.forward);
        dash.transform.forward = -transform.up;
        //GameObject dash = Instantiate(DashEffectPrefab, transform.position, Quaternion.Euler(transform.forward));
        dash.GetComponent<ParticleSystemRenderer>().material = playerMaterial;
        audioSource.PlayOneShot(a_dash, dashVolume);
        //p.GetComponentInChildren<MeshRenderer>().material = dashMat;
    }

    private bool IsDead()
    {
        switch (LevelSettings.Instance.deadMode) {
            case LevelSettings.DEADMODE.HEIGHT:
                if (transform.position.y < -10f) return true;
                break;
            case LevelSettings.DEADMODE.OUT:
                if (outside) return true;
                break;
        }
        return false;
    }

	private void FixedUpdate()
	{

        /*
        if (rot) {
            rot = false;
            rb.AddRelativeTorque(new Vector3(1, 0 ,0)* 100, ForceMode.Impulse);
        }
        */

        // MOVE PHYSICS
        if (moveDir.magnitude > 0)
        {
            rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);
        }

        // JUMP PHYSICS
        if (jumpButton) {   //if (Input.GetButtonDown(jumpButtonName)) {
            if (onGround || currentJump < MAX_JUMP) 
            {
                rb.velocity = new Vector3(0, jumpHeight, 0);
                //rb.velocity = new Vector3(rb.velocity.x, 6f, rb.velocity.z);
                currentJump++;
                onGround = false;
                jumpButton = false;
            }
        }

        // DASH PHYSICS
        if (dashTime > 0) {
            dashTime -= Time.fixedDeltaTime;
            if (dashTime <= 0) {
                rb.velocity = Vector3.zero;
                //p.GetComponentInChildren<MeshRenderer>().material = defaultMat;
            } else {
                rb.velocity = transform.up * dashSpeed; 
            }
        } 
	}

    // FOR DASH RESET

	private void OnCollisionStay(Collision collision)
	{
        if (collision.collider.tag == "Arena") {
            //onGround = true;
            currentJump = 0;
            currentDash = 0;
        }
	}
	

	private void OnCollisionEnter(Collision collision)
	{

        if (collision.collider.CompareTag("Ground"))
        {
            if (!outside)
            {
                outside = true;
                // OUT EFFECT
                CameraShaker.Instance.ShakeOnce(3f, 6f, 0.12f, .6f);
            }
        }
      

        // LAND
        if (collision.collider.tag == "Arena")
        {
            outside = false;

            currentJump = 0;
            currentDash = 0;

            // audio
            if (!onGround) {
                onGround = true;
                audioSource.PlayOneShot(a_land, landVolume);
            } else {
                audioSource.PlayOneShot(a_land, landVolume * 0.03f);
            }


            if (collision.contacts[0].thisCollider.tag == "PlayerBase")
            {
                Instantiate(DustBigEffectPrefab, collision.contacts[0].point, Quaternion.identity);
            } else if (collision.contacts[0].thisCollider.tag == "PlayerPole") {
                Instantiate(DustSmallEffectPrefab, collision.contacts[0].point, Quaternion.identity);
            }
        }

        // ATTACK
        if (collision.collider.CompareTag("PlayerBase") || collision.collider.CompareTag("PlayerPole")) {
            if (dashTime > 0)
            {

                if (EffectSetting.Instance.punchCameraShake)
                {
                    CameraShaker.Instance.ShakeOnce(1f, 0.6f, 0.08f, 0.08f);
                }

                Instantiate(hitEffectPrefab, collision.contacts[0].point, Quaternion.identity);
                Freezer.Instance.Freeze(0.06f);
            }
        }
	}

    void RebornInSeconds(float t) {
        Invoke("Reborn", t);
    }

    void Reborn() 
    {
        // EFFECT!

        outside = false;
        alive = true;
        onGround = false;

        transform.localScale = Vector3.one;
        transform.position = spawnPoint.position;
        rb.velocity = Vector3.zero;


        if (LevelSettings.Instance.deathPenalty)
        {
            poleLength -= deathPenaltyLength;
            poleLength = Mathf.Max(minLength, poleLength);
            p.transform.localScale = new Vector3(1, poleLength, 1);
            rb.mass = poleLength;
        }


    }

    void InputDection() {
        float h = 0, v = 0;
        GamePadState state;

        // Input Detection
        switch (playerNumber)
        {
            case 1:
                h = GamePad.GetAxis(CAxis.LX, PlayerIndex.One);
                v = -GamePad.GetAxis(CAxis.LY, PlayerIndex.One);
                state = GamePad.GetState(PlayerIndex.One);
                jumpButton = state.Pressed(CButton.A);
                dashButton = state.Pressed(CButton.B);
                break;

            case 2:
                h = GamePad.GetAxis(CAxis.LX, PlayerIndex.Two);
                v = -GamePad.GetAxis(CAxis.LY, PlayerIndex.Two);
                state = GamePad.GetState(PlayerIndex.Two);
                jumpButton = state.Pressed(CButton.A);
                dashButton = state.Pressed(CButton.B);
                break;
            case 3:
                h = GamePad.GetAxis(CAxis.LX, PlayerIndex.Three);
                v = -GamePad.GetAxis(CAxis.LY, PlayerIndex.Three);
                state = GamePad.GetState(PlayerIndex.Three);
                jumpButton = state.Pressed(CButton.A);
                dashButton = state.Pressed(CButton.B);
                break;
            case 4:
                h = GamePad.GetAxis(CAxis.LX, PlayerIndex.Four);
                v = -GamePad.GetAxis(CAxis.LY, PlayerIndex.Four);
                state = GamePad.GetState(PlayerIndex.Four);
                jumpButton = state.Pressed(CButton.A);
                dashButton = state.Pressed(CButton.B);
                break;
        }

        moveDir = new Vector3(h, 0, v).normalized;
    }
}