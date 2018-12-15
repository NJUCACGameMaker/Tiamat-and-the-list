using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1S1Scenario : Scenario {

    private bool beforeFateShown = false;
    private bool goToTheTrapShown = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!beforeFateShown)
        {
            beforeFateShown = true;
            scenarioHintOn = true;
            DialogManager.ShowDialog("Before_Fate", OnBeforeFateShownEnd);
        }
	}

    public void goToTrap()
    {
        if (!goToTheTrapShown)
        {
            goToTheTrapShown = true;
            scenarioHintOn = true;
            DialogManager.ShowDialog("Go_the_trap", OnGoToTheTrapShownEnd);
        }
    }

    void OnBeforeFateShownEnd()
    {
        scenarioHintOn = false;
    }

    void OnGoToTheTrapShownEnd()
    {
        scenarioHintOn = false;
    }

    public override Vector3 GetPlayerInitPos(string lastSceneName)
    {
        return new Vector3(-7.5f, -3f, 0f);
    }
}
