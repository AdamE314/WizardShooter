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

    private bool bossIsDead = false;

    private bool postedScore = false;

    //public Text hpText;

    public Text winLoseText;

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
        timeTaken = 0f;
        //hpText.text = "Health: " + hp;
        enemiesLeftText.text = "Enemies Remaining: " + enemiesLeft;
        wavesLeftText.text = "Waves Remaining: " + wavesLeft;
        timerText.text = "Time: " + timeTaken;
        winLoseText.text = "";
    }


    void Update()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemyList.Length;
        hp = playerhp.myHealth;
        wavesLeft = Mathf.Max(bosshp.myHealth,0);
        if (wavesLeft <= 0) { bossIsDead = true; }
        if (!bossIsDead) { timeTaken += Time.deltaTime; }
        else if (!postedScore) { postedScore = true; this.GetComponent<HighscoresDemo>().Insert();  }
        //hpText.text = "Health: " + hp;
        enemiesLeftText.text = "Enemies Remaining: " + enemiesLeft;
        wavesLeftText.text = "Waves Remaining: " + wavesLeft;
        timerText.text = "Time: "+ Mathf.Floor(timeTaken*1000)/1000f;
        if (hp <= 0)
        {
            winLoseText.text = "You Lose!";
        }
        if (wavesLeft <= -1)
        {
            winLoseText.text = "You Win!";
            wavesLeftText.text = "Waves Remaining: 0";
        }
    }

}
