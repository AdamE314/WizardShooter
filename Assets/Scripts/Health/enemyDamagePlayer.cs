using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamagePlayer : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHealth _mytarg = other.GetComponent<playerHealth>();
            _mytarg.myHealth -= 1;
        }
    }
}
