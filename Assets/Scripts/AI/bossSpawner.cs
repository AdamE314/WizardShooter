using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class bossSpawner : MonoBehaviour {

    //Enabling spawning
    bossAI myAI;
    public bool canSpawn = true;

    //Spawning stats
    int enmNum;
    int enmCount = 0;
    float spawnDelay = 1f;
    float spawnTimer;
    int[,] enemySpawns;
    public int bossId = 0;

    //Enemies
    enum enm {bat, charger, mage};
    int enmLength = 3;
    int waveNum = 0;
    public GameObject bat;
    public GameObject charger;
    public GameObject mage;
    GameObject[] enmTypes;
    public float arenaRadius;
    public Vector3 arenaCenter;

	// Use this for initialization
	void Start () {
        myAI = GetComponent<bossAI>();
        enmNum = 1;
        spawnTimer = spawnDelay;
        enemySpawns = setEnemySpawns(bossId,myAI);
        enmTypes = new[] {bat, charger, mage};
    }
	
	// Update is called once per frame
	void Update () {

        var cState = myAI.myState;

        if (cState == "default" && canSpawn)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                if (myAI.myHealth < 0)
                {
                    //Destroy(gameObject);
                }
                else
                {
                    //Layer mask to check for player
                    int layerId = 11;
                    int layerMask = 1 << layerId;
                    GameObject _enm;
                    Vector3 enmSpawn;
                    for (int i = 0; i < enmLength; i++)
                    {
                        _enm = enmTypes[i];
                        for (int j = 0; j < enemySpawns[waveNum, i]; j++)
                        {
                            do
                            {
                                Vector3 _temp = new Vector3(UnityEngine.Random.value - 0.5f, 0f, UnityEngine.Random.value - 0.5f).normalized;
                                Vector3 spawnOffset = _temp * (UnityEngine.Random.value * arenaRadius);

                                enmSpawn = arenaCenter + spawnOffset;
                            } while (Physics.OverlapSphere(enmSpawn, 20f, layerMask).Length > 0);


                            Instantiate(_enm, enmSpawn, Quaternion.identity);
                        }
                    }
                    waveNum++;
                    canSpawn = false;
                }
            }

        } else
        {
            enmCount = 0;
        }

	}

    int[,] setEnemySpawns(int boss, bossAI mAI)
    {

        int mHP = mAI.myHealth;
        int mEnms = enmLength;
        int[,] _enms = new int[mHP, mEnms];

        switch (boss)
        {
            case 0:
                //Round 1
                _enms[0, (int) enm.bat] = 5;
                _enms[0, (int) enm.charger] = 0;
                _enms[0, (int) enm.mage] = 0;
                //Round 2
                _enms[1, (int)enm.bat] = 8;
                _enms[1, (int)enm.charger] = 0;
                _enms[1, (int)enm.mage] = 0;
                //Round 3
                /*_enms[2, (int)enm.bat] = 8;
                _enms[2, (int)enm.charger] = 1;
                _enms[2, (int)enm.mage] = 0;
                //Round 4
                _enms[3, (int)enm.bat] = 10;
                _enms[3, (int)enm.charger] = 3;
                _enms[3, (int)enm.mage] = 0;
                //Round 5
                _enms[4, (int)enm.bat] = 15;
                _enms[4, (int)enm.charger] = 5;
                _enms[4, (int)enm.mage] = 0;
                //Round 6
                _enms[5, (int)enm.bat] = 15;
                _enms[5, (int)enm.charger] = 3;
                _enms[5, (int)enm.mage] = 1;
                //Round 7
                _enms[6, (int)enm.bat] = 15;
                _enms[6, (int)enm.charger] = 5;
                _enms[6, (int)enm.mage] = 3;
                //Round 8
                _enms[7, (int)enm.bat] = 5;
                _enms[7, (int)enm.charger] = 10;
                _enms[7, (int)enm.mage] = 10;
                //Round 9
                _enms[8, (int)enm.bat] = 10;
                _enms[8, (int)enm.charger] = 10;
                _enms[8, (int)enm.mage] = 10;
                //Round 10
                _enms[9, (int)enm.bat] = 15;
                _enms[9, (int)enm.charger] = 10;
                _enms[9, (int)enm.mage] = 10;*/
                break;
        }

        return _enms;

    }

}
