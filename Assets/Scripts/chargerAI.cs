using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargerAI : MonoBehaviour
{

    public float moveSpeed = 8f;

    private GameObject targetNode = null;
    private GameObject myPlayer = null;
    private string myState = "default";

    //Charging
    public float chargeRange = 10f;
    public float windupDelay = 3f;
    public float chargeSpeed = 14f;

    private int layerId;
    private int layerMask;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (myPlayer == null)
        {
            findPlayer();
        }
        else
        {

            switch (myState)
            {

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
                        else
                        {

                            transform.position -= _movepos * Time.deltaTime;

                        }

                    }
                    else
                    {

                        setTarget();

                    }
                    break;

                //Windup for charge
                case "windup":

                    break;

            }

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

    void findPlayer()
    {

        myPlayer = GameObject.FindGameObjectWithTag("Player");

    }

    void playerDistance()
    {



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

}
