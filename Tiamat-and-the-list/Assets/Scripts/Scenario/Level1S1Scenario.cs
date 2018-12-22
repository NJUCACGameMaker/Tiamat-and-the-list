using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Level1S1Scenario : Scenario {

    private bool beforeFateShown = false;
    private bool goToTheTrapShown = false;
	
	// Update is called once per frame
	void Update () {
		if (!beforeFateShown)
        {
            beforeFateShown = true;
            scenarioHintOn = true;
            DialogManager.ShowDialog("Before_Fate", OnBeforeFateShownEnd);
        }
	}

    public void GoToTrap()
    {
        if (!goToTheTrapShown)
        {
            goToTheTrapShown = true;
            scenarioHintOn = true;
            DialogManager.ShowDialog("Go_the_trap", OnGoToTheTrapShownEnd);
        }
    }

    public override string GetArchive()
    {
        var root = new JSONClass()
        {
            { "BeforeFateShown", new JSONData(beforeFateShown) },
            { "GoToTheTrapShown", new JSONData(goToTheTrapShown) }
        };
        return root.ToString();
    }

    public override void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        beforeFateShown = root["BeforeFateShown"].AsBool;
        goToTheTrapShown = root["GoToTheTrapShown"].AsBool;
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
