using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour {

	public float moveSpeed = 6.0f;

    public Camera myCamera;
    public float lookSpeed = 15.0f;
    private float minLookY = -90.0f;
    private float maxLookY = 90.0f;
    private float lookY = 0.0f;

    //Pre-initialization
    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Use this for initialization
    void Start () {
        
	}

    void Update()
    {

        
        float lookX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * lookSpeed;
        lookY -= Input.GetAxis("Mouse Y") * lookSpeed;

        lookY = Mathf.Clamp(lookY,minLookY,maxLookY);

        myCamera.transform.localEulerAngles = new Vector3(lookY,0.0f,0.0f);

        transform.localEulerAngles = new Vector3(0.0f,lookX,0.0f);
        
    }

    // Update is called once per frame
    void FixedUpdate () {
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed*Time.fixedDeltaTime;

        transform.position += moveDirection;
	}
}
