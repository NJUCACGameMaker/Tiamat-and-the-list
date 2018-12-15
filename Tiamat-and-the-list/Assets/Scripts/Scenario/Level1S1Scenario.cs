using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1S1Scenario : Scenario {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override Vector3 GetPlayerInitPos(string lastSceneName)
    {
        return new Vector3(-7.5f, -3f, 0f);
    }
}
