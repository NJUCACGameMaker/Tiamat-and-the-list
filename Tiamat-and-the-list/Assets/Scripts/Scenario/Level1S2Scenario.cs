using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1S2Scenario : Scenario {
    
	
	// Update is called once per frame
	void Update () {
		
	}

    public override Vector3 GetPlayerInitPos(string lastSceneName)
    {
        return new Vector3(0f, -2.83f, 0f);
    }
}
