using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1S3Scenario : Scenario {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public override Vector3 GetPlayerInitPos(string lastSceneName)
    {
        return new Vector3(-4.18f, -2.83f, 0f);
    }
}
