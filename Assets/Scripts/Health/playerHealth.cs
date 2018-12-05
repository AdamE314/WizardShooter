using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour {

    public int myHealth = 3;
    private float myTime = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (myHealth <= 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameObject.GetComponent<playerController>().enabled = false;
            myTime -= Time.deltaTime;
            if (myTime <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }

	}
}
