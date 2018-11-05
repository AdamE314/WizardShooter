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
    private float shotCooldown = 0.2f;
    private float shotTimer = 0f;

    //Pre-initialization
    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Use this for initialization
    void Start () {
        
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
        if (shotTimer <= 0 && Input.GetButton("Fire1"))
        {
            shotTimer = shotCooldown;
            spawnBullet(myCamera.transform.forward, 0.0f);
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
        _proj.projectileSpeed = 25f;
    }
}
