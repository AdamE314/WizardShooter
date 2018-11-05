using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createNavNodes : MonoBehaviour {

    public Transform Node;

    private float distance = 8;

	// Use this for initialization
	void Start () {
        for (int i = -4; i < 5; i++)
        {
            for (int j = -4; j < 5; j++)
            {
                var _mynode = Instantiate(Node,transform.position+(new Vector3(i*distance,0.0f,j*distance)),Quaternion.identity);
                _mynode.parent = transform;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
