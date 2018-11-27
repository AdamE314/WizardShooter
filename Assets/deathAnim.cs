using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathAnim : MonoBehaviour {

    public Animator anim;

    private float Timer = 10f;

	// Update is called once per frame
	void Update () {
        if (Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            anim.SetBool("Grounded",true);
        }
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Destroy(gameObject);
        }
	}
}
