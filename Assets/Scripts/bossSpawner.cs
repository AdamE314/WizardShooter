using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class bossSpawner : MonoBehaviour {

    //Enabling spawning
    bossAI myAI;
    public bool canSpawn = true;

    //Spawning stats
    int enmNum;
    int enmCount = 0;
    float spawnDelay = 1f;
    float spawnTimer;

    //Enemies
    public GameObject bat;
    public GameObject charger;
    public float arenaRadius;
    public Vector3 arenaCenter;
    public int[,] enmSpawn;
    public int myWave = 0;

	// Use this for initialization
	void Start () {
        enmSpawn = defineSpawns(0);
        myAI = GetComponent<bossAI>();
        enmNum = 1;
        spawnTimer = spawnDelay;
	}
	
	// Update is called once per frame
	void Update () {

        var cState = myAI.myState;

        if (cState == "default" && canSpawn)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0 && enmCount < enmNum)
            {
                int waveEnmCount = 0;
                //for(int i=0;i<)
                //Spawning
                GameObject _enm;
                var _rand = UnityEngine.Random.value;
                if (_rand >= 0.5) _enm = bat; else _enm = charger;

                Vector3 _temp = new Vector3(UnityEngine.Random.value-0.5f,0f, UnityEngine.Random.value - 0.5f).normalized;
                Vector3 spawnOffset = _temp * (UnityEngine.Random.value * arenaRadius);

                Vector3 enmSpawn = arenaCenter + spawnOffset;

                Instantiate(_enm,enmSpawn,Quaternion.identity);

                //Incrementing
                enmCount++;

                if (enmCount >= enmNum)
                {
                    canSpawn = false;
                    enmNum++;
                }
            }
        } else
        {
            enmCount = 0;
        }

	}

    int[,] defineSpawns(int bossid)
    {
        int[,] mySpawns = new int[GetComponent<bossAI>().myHealth, 2];

        //Round 1 
        mySpawns[0, 0] = 5; //Bats
        mySpawns[0, 1] = 0; //Charger

        //Round 2
        mySpawns[1, 0] = 10; //Bats
        mySpawns[1, 1] = 0; //Charger

        //Round 3 
        mySpawns[2, 0] = 8; //Bats
        mySpawns[2, 1] = 1; //Charger

        //Round 4
        mySpawns[3, 0] = 13; //Bats
        mySpawns[3, 1] = 2; //Charger

        //Round 5
        mySpawns[4, 0] = 20; //Bats
        mySpawns[4, 1] = 4; //Charger

        return mySpawns;
    }
}
