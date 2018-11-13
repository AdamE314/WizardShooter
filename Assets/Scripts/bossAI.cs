using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAI : MonoBehaviour {

    //Animation
    public Animator anim;

    //States
    public string myState = "default";
    //Stats
    public int myHealth = 5;
    private GameObject myPlayer;
    //Shooting
    public GameObject myProjectile;
    public float burstDelay = 5f;
    public float shotDelay = 1f;
    public int shotCount = 3;
    public int spreadCount = 3;
    public float spreadAngle = 15f;
    //Invulnerable
    private float invulnHeight = 5f;
    //Vulnerable
    public float vulnWindow = 15f;
    private float vulnHeight = 2.5f;

    //Counters and Timers
    //Shooting
    private float burstTimer = 0f;
    private float shotTimer = 0f;
    private int shotCounter = 0;
    //Vulnerable
    private float vulnTimer = 0f;
    private bool isHit = false;

    //Spawning reference
    private bossSpawner spawner;

    // Use this for initialization
    void Start () {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        spawner = GetComponent<bossSpawner>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (myState)
        {
            case "default":

                burstTimer += Time.deltaTime;
                if (burstTimer >= burstDelay)
                {

                    if (burstTimer >= burstDelay + (shotDelay / 2f))
                    {
                        anim.SetBool("Attacking", true);
                    }

                    if (shotCounter < shotCount)
                    {
                        shotTimer += Time.deltaTime;
                        if (shotTimer >= shotDelay)
                        {
                            shootBurst(spreadCount, spreadAngle);
                            shotTimer = 0f;
                            shotCounter++;
                        }
                    }
                    else
                    {
                        shotTimer = 0f;
                        burstTimer = 0f;
                        shotCounter = 0;
                    }
                }
                else
                {
                    anim.SetBool("Attacking", false);
                }
                transform.position = new Vector3(transform.position.x,invulnHeight,transform.position.z);

                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !spawner.canSpawn)
                {
                    myState = "vulnerable";
                    vulnTimer = 0f;
                    isHit = false;
                }

                break;

            case "vulnerable":

                anim.SetBool("Attacking", false);

                spawner.canSpawn = true;

                vulnTimer += Time.deltaTime;

                if (vulnTimer >= vulnWindow)
                {
                    if (isHit)
                    {
                        myState = "default";
                        myHealth--;
                        Debug.Log("Oof owwie ouch");
                    }
                }

                transform.position = new Vector3(transform.position.x, vulnHeight, transform.position.z);

                break;
        }

	}

    void shootBurst(int num, float angle) {
        for (float i = 0; i < num; i++)
        {
            float _bdir = angle*Mathf.Ceil(i/2)*Mathf.Pow(-1f,i);
            shootBullet(Vector3.Normalize(myPlayer.transform.position - transform.position), _bdir);
        }
    }

    void shootBullet(Vector3 direction, float dirOffset) {
        Vector3 _offset = new Vector3(0f, 1f, 0f) + direction * 2f;
        projectileMotion _proj = Instantiate(myProjectile, transform.position + _offset,Quaternion.LookRotation(direction)).GetComponent<projectileMotion>();
        _proj.transform.RotateAround(_proj.transform.position, Vector3.up, dirOffset);
        _proj.projectileSpeed = 20f;
        _proj.shotByPlayer = false;
    }

    public void getHit()
    {
        if(myState == "vulnerable")
        {
            isHit = true;
        }
    }
}
