using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createNavNodes : MonoBehaviour {

    public Transform Node;
    private int angleDist = 40;
    private int layerDist = 7;

    private float distance = 8;

	// Use this for initialization
	void Start () {
        for (int i = 0;i < angleDist; i++)
        {
            var angle = i * (360 / angleDist);
            var v3Dir = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
            for (int j = 0; j < layerDist; j++)
            {
                if (j > layerDist / 2 || i % 2 == 0)
                {
                    var _dist = v3Dir * (j * distance);
                    var _mynode = Instantiate(Node, transform.position + _dist, Quaternion.identity);
                    _mynode.parent = transform;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
