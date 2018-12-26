using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Level1S3Scenario : Scenario {

    private bool reachPointShown = false;
	
	// Update is called once per frame
	void Update () {
		if (!reachPointShown)
        {
            reachPointShown = true;
            scenarioHintOn = true;
            DialogManager.ShowDialog("reach_point", OnReachPointShownEnd);
        }
	}

    public override string GetArchive()
    {
        var root = new JSONClass()
        {
            { "ReachPointShown", new JSONData(reachPointShown) }
        };
        return root.ToString();
    }

    public override void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        reachPointShown = root["ReachPointShown"].AsBool;
    }

    void OnReachPointShownEnd()
    {
        scenarioHintOn = false;
    }


    public override Vector3 GetPlayerInitPos(string lastSceneName)
    {
        return new Vector3(-4.18f, -2.83f, 0f);
    }
}
