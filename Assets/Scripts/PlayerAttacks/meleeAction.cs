using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAction : MonoBehaviour {

    public float despawnTimer = .25f;
    private GameObject myPlayer;

	// Use this for initialization
	void Start () {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {

        //Check if colliding with enemy
        if (other.tag == "Enemy")
        {
            enemyHealth _mytarg = other.GetComponent<enemyHealth>();
            _mytarg.myHealth -= 2;
        }
        if (other.tag == "Boss")
        {
            bossAI _mytarg = other.GetComponent<bossAI>();
            _mytarg.getHit();
        }

    }
}
