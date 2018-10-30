using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyNavBasic : MonoBehaviour {

    public float moveSpeed = 0.5f;

    private GameObject targetNode = null;
    private GameObject myPlayer = null;

	// Use this for initialization
	void Start () {

        

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (myPlayer == null)
        {
            findPlayer();
        }
        else
        {

            if (targetNode != null)
            {

                var _targpos = targetNode.transform.position;
                var _movepos = (Vector3.Normalize(_targpos - transform.position)) * moveSpeed;

                transform.position += _movepos * Time.deltaTime;

            }
            else
            {

                setTarget();

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

        int layerId = 8;
        int layerMask = 1 << layerId;

        var _myNode = Physics.OverlapSphere(transform.position,1f,layerMask)[0].gameObject;

        var _allnodes = Physics.OverlapSphere(_myNode.transform.position,10f,layerMask);
        var _distance = Vector3.Distance(_myNode.transform.position,myPlayer.transform.position);

        foreach (Collider _n in _allnodes)
        {

            var distance = Vector3.Distance(_n.transform.position, myPlayer.transform.position);
            if (distance < _distance && _n.gameObject != _myNode)
            {

                targetNode = _n.gameObject;
                _distance = distance;

            }

        }

    }

    void findPlayer() {

        myPlayer = GameObject.FindGameObjectWithTag("Player");

    }

    void OnTriggerStay(Collider other)
    {

        if (other.tag == "Node") {

            setNewTarget();

        }
    }

}
