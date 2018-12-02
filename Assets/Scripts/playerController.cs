using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    //Move
	public float moveSpeed = 6.0f;
    //Look
    public Camera myCamera;
    public float lookSpeed = 15.0f;
    private float minLookY = -90.0f;
    private float maxLookY = 90.0f;
    private float lookY = 0.0f;

    //Shoot
    public GameObject myProjectile;
    public float projSpeed = 250f;
    private float shotCooldown = 0.2f;
    private float shotTimer = 0f;

    //Melee
    public GameObject myMelee;
    public float myMeleeDuration = 0.25f;
    public float myMeleeCooldown = 0.5f;
    private float myMeleeTimer = 0f;

    //Animation
    //public Canvas myCanvas;
    public Animator swordAnim;

    //Pre-initialization
    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Use this for initialization
    void Start () {
        swordAnim.transform.SetParent(myCamera.transform);
        //swordAnim.SetBool("Attacking", true);
	}

    void Update()
    {

        //Looking
        float lookX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * lookSpeed;
        lookY -= Input.GetAxis("Mouse Y") * lookSpeed;
        lookY = Mathf.Clamp(lookY,minLookY,maxLookY);
        myCamera.transform.localEulerAngles = new Vector3(lookY,0.0f,0.0f);
        transform.localEulerAngles = new Vector3(0.0f,lookX,0.0f);

        //Shooting
        shotTimer -= Time.deltaTime;
        if (myMeleeTimer >= myMeleeCooldown && shotTimer <= 0 && Input.GetButton("Fire1"))
        {
            shotTimer = shotCooldown;
            spawnBullet(myCamera.transform.forward, 0.0f);
        }

        myMeleeTimer += Time.deltaTime;
        if (myMeleeTimer >= myMeleeCooldown && Input.GetButtonDown("Fire2"))
        {
            myMeleeTimer = 0f;
            spawnMelee(myCamera.transform.forward);
            //swordAnim.SetBool("Attacking", true);
            swordAnim.Play("swordSwing");
            
        }

        if (myMeleeTimer >= myMeleeDuration)
        {
            //swordAnim.SetBool("Attacking", true);
            
        }

    }

    // Update is called once per frame
    void FixedUpdate () {

        //Moving
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed*Time.fixedDeltaTime;
        transform.position += moveDirection;

	}

    //Spawning bullets when firing
    void spawnBullet(Vector3 direction, float dirOffset)
    {
        Vector3 _offset = new Vector3(0f, 1f, 0f) + direction * 2f;
        projectileMotion _proj = Instantiate(myProjectile, transform.position + _offset, myCamera.transform.rotation).GetComponent<projectileMotion>();
        _proj.transform.RotateAround(_proj.transform.position, Vector3.up, dirOffset);
        _proj.projectileSpeed = projSpeed;
        _proj.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }

    void spawnMelee(Vector3 direction)
    {
        Vector3 _offset = new Vector3(0f, 1f, 0f) + direction * 3f;
        meleeAction _melee = Instantiate(myMelee, transform.position + _offset, myCamera.transform.rotation).GetComponent<meleeAction>();
        _melee.despawnTimer = myMeleeDuration;
        _melee.gameObject.transform.parent = myCamera.transform;
    }
}
