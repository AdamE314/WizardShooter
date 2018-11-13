using System.Collections;
using System.Collections.Generic;
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

	// Use this for initialization
	void Start () {
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
                //Spawning
                GameObject _enm;
                var _rand = Random.value;
                if (_rand >= 0.5) _enm = bat; else _enm = charger;

                Vector3 _temp = new Vector3(Random.value-0.5f,0f, Random.value - 0.5f).normalized;
                Vector3 spawnOffset = _temp * (Random.value * arenaRadius);

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
}
