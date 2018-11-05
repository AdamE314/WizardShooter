using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batAI : MonoBehaviour {

    public float moveSpeed = 8f;
    public float arcTime = 1f;
    public float arcHeight = 3f;
    public float swoopSpeed = 8f;

    private GameObject targetNode = null;
    private GameObject myPlayer = null;
    private string myState = "default";
    private float myHeight;
    private float swoopTimer = 0f;
    private float swoopRange = 6f;
    private Vector3 swoopDirection = Vector3.forward;

    private int layerId;
    private int layerMask;

	// Use this for initialization
	void Start () {

        myHeight = transform.position.y;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (myPlayer == null)
        {
            findPlayer();
        }
        else
        {

            switch (myState) {

                //Default state
                case "default":
                    if (targetNode != null)
                    {

                        var _targpos = targetNode.transform.position;
                        _targpos = new Vector3(_targpos.x, transform.position.y, _targpos.z);
                        var _movepos = (Vector3.Normalize(_targpos - transform.position)) * moveSpeed;

                        //Check for collision with enemies
                        layerId = 9;
                        layerMask = 1 << layerId;

                        if (!Physics.Raycast(transform.position, _movepos, moveSpeed * Time.deltaTime, layerMask, QueryTriggerInteraction.Collide))
                        {

                            transform.position += _movepos * Time.deltaTime;

                        }
                        else {

                            transform.position -= _movepos * Time.deltaTime;

                        }

                        

                        //Enter swoop state
                        //Get position along horizontal plane closest to player
                        var _dest = myPlayer.transform.position;
                        _dest.y = transform.position.y;
                        if (Vector3.Distance(_dest, transform.position) <= swoopRange){
                            //Init swoop state
                            myState = "swoop";
                            swoopTimer = 0f;
                            //Destination/Direction
                            swoopDirection = Vector3.Normalize(_dest - transform.position);

                        }

                    }
                    else
                    {

                        setTarget();

                    }
                    break;

                //Swoop state
                case "swoop":

                    transform.position += (swoopDirection * moveSpeed) * Time.deltaTime;

                    transform.position = new Vector3(transform.position.x, myHeight - Mathf.Sin(Mathf.PI * (swoopTimer / arcTime)) * arcHeight, transform.position.z);

                    swoopTimer += Time.deltaTime;

                    if (swoopTimer >= arcTime) {

                        myState = "default";

                        setNewTarget();

                    }

                    break;

            }

        }

	}

    void setTarget() {

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

    void setNewTarget() {

        layerId = 8;
        layerMask = 1 << layerId;

        var _allnodes = Physics.OverlapSphere(targetNode.transform.position,10f,layerMask);
        var _distance = Vector3.Distance(targetNode.transform.position,myPlayer.transform.position);

        foreach (Collider _n in _allnodes)
        {

            var distance = Vector3.Distance(_n.transform.position, myPlayer.transform.position);
            GameObject _node = null;
            if (distance < _distance && _n.gameObject != targetNode)
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

    void findPlayer() {

        myPlayer = GameObject.FindGameObjectWithTag("Player");

    }

    void playerDistance() {

        

    }

    void OnTriggerStay(Collider other)
    {

        if (other.tag == "Node" || other.tag == "Enemy") {

            setNewTarget();

        }
    }

}
