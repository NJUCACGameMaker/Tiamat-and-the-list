using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : Interoperable {

    public PlayerManager Apkal;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Mathf.Abs(Apkal.transform.position.x - transform.position.x);
        if (distance>detectDist)
        {
            Apkal.floorLayer = 0;
        }
	}
    public override void WithinRange()
    {
        Apkal.floorLayer = 2;

    }
}
