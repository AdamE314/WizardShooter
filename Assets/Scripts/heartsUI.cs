using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heartsUI : MonoBehaviour {

    GameObject player;
    playerHealth playerhp;
    int numHearts;
    public Image[] hearts;
    public Sprite displayedHeart;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerhp = player.GetComponent<playerHealth>();
	}

	void Update ()
    {
	    for(int i = 0; i < hearts.Length; i++)
        {
            if (i < playerhp.myHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
	}
}
