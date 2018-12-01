using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class uiText : MonoBehaviour
{


    public int hp;

    public int enemiesLeft;

    public int wavesLeft;

    public float timeTaken;

    public Text hpText;

    public Text enemiesLeftText;

    public Text wavesLeftText;

    public Text timerText;

    GameObject player;
    GameObject[] enemyList;
    GameObject boss;
    playerHealth playerhp;
    bossAI bosshp;


    void Start()
    { 
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemyList.Length;
        player = GameObject.FindGameObjectWithTag("Player");
        playerhp = player.GetComponent<playerHealth>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        bosshp = boss.GetComponent<bossAI>();
        wavesLeft = bosshp.myHealth;
        hp = playerhp.myHealth;
       // timeTaken = Time.timeSinceLevelLoad;
        hpText.text = "Health: " + hp;
        enemiesLeftText.text = "Enemies Remaining: " + enemiesLeft;
        wavesLeftText.text = "Waves Remaining: " + wavesLeft;
       // timerText.text = "Time: " + timeTaken;
    }


    void Update()
    {
      //  timeTaken = Time.timeSinceLevelLoad;
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemyList.Length;
        hp = playerhp.myHealth;
        wavesLeft = bosshp.myHealth;
        hpText.text = "Health: " + hp;
        enemiesLeftText.text = "Enemies Remaining: " + enemiesLeft;
        wavesLeftText.text = "Waves Remaining: " + wavesLeft;
       // timerText.text = "Time: "+ timeTaken;
    }

}
