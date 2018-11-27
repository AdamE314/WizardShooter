using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mageAI : MonoBehaviour {

    private GameObject targetNode;
    private GameObject myPlayer;
    public GameObject myProjectile;

    private string myState = "default";

    //Attacking
    private float atkCooldown = 3f;
    private float atkTimer = 3f;
    public Animator mageAnim;

    //Nav
    public float moveSpeed = 12.0f;
    public float idealDist = 20f;

    private int layerId;
    private int layerMask;


	// Use this for initialization
	void Start () {
        myPlayer = GameObject.FindGameObjectWithTag("Player");

	}

    // Update is called once per frame
    void Update() {
        switch (myState) {
            case "default":
                if (targetNode != null)
                {

                    var _targpos = targetNode.transform.position;
                    _targpos = new Vector3(_targpos.x, transform.position.y, _targpos.z);
                    var _movepos = (Vector3.Normalize(_targpos - transform.position)) * moveSpeed;

                    //Movement
                    if (atkTimer < atkCooldown && atkTimer > 0)
                    {
                        //Check for collision with enemies
                        layerId = 9;
                        layerMask = 1 << layerId;

                        if (!Physics.Raycast(transform.position, _movepos, moveSpeed * Time.deltaTime, layerMask, QueryTriggerInteraction.Collide))
                        {
                            transform.position += _movepos * Time.deltaTime;
                        }
                        else
                        {
                            transform.position -= _movepos * Time.deltaTime;
                        }
                    }
                    else
                    {

                    }

                    atkTimer -= Time.deltaTime;
                    if (atkTimer <= 0)
                    {
                        mageAnim.Play("mageAttack");
                        if (atkTimer <= -0.25)
                        {
                            atkTimer = atkCooldown + 0.25f;
                            transform.localEulerAngles = myPlayer.transform.localEulerAngles;
                            shootBullet(transform.forward);
                            transform.rotation = Quaternion.identity;
                        }
                    }


                    //Enter attacking states
                    /*if (Vector3.Distance(transform.position, myPlayer.transform.position) < chargeRange)
                    {
                        myState = "windup";
                        myTimer = 0f;
                    }*/

                }
                else
                {

                    setTarget();

                }
                break;
            case "attacking":

                break;
        }
    }

    void setTarget()
    {

        var _allnodes = GameObject.FindGameObjectsWithTag("Node");
        var _distance = -1f;

        foreach (GameObject _n in _allnodes)
        {

            var distance = Vector3.Distance(_n.transform.position, transform.position);
            if (distance < _distance || _distance == -1)
            {

                targetNode = _n;
                _distance = distance;

            }

        }

    }

    void setNewTarget()
    {

        layerId = 8;
        layerMask = 1 << layerId;

        var _allnodes = Physics.OverlapSphere(targetNode.transform.position, 10f, layerMask);
        var _distance = Vector3.Distance(targetNode.transform.position, myPlayer.transform.position);
        var _pDist = Vector3.Distance(transform.position, myPlayer.transform.position);

        foreach (Collider _n in _allnodes)
        {

            var distance = Vector3.Distance(_n.transform.position, myPlayer.transform.position);
            
            GameObject _node = null;
            bool test = (Mathf.Sign(distance - _distance) != Mathf.Sign(_pDist - idealDist));
            if (test && _n.gameObject != targetNode)
            {

                _node = _n.gameObject;
                _distance = distance;

            }
            if (_node != null)
            {
                targetNode = _node;
            }

        }

    }

    void OnTriggerStay(Collider other)
    {

        if (other.tag == "Node" || other.tag == "Enemy")
        {
            if (other.gameObject == targetNode)
            {
                setNewTarget();
            }

        }
    }

    void shootBullet(Vector3 direction)
    {
        Vector3 _offset = new Vector3(0f, 1f, 0f) + direction * 2f;
        projectileMotion _proj = Instantiate(myProjectile, transform.position + _offset, Quaternion.LookRotation(direction)).GetComponent<projectileMotion>();
        _proj.transform.LookAt(new Vector3(myPlayer.transform.position.x,_proj.gameObject.transform.position.y,myPlayer.transform.position.z));
        _proj.projectileSpeed = 30f;
        _proj.shotByPlayer = false;
    }

}
