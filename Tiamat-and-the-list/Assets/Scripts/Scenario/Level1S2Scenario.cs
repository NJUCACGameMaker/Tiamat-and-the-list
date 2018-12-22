using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Level1S2Scenario : Scenario {

    private bool toTheMagicShown = false;
    private bool firstTimeTouchShown = false;

	// Update is called once per frame
	void Update () {
		if (!toTheMagicShown)
        {
            toTheMagicShown = true;
            scenarioHintOn = true;
            DialogManager.ShowDialog("To_the_magic", OnToTheMagicShownEnd);
        }
	}

    public void FirstTimeTouch()
    {
        if (!firstTimeTouchShown)
        {
            firstTimeTouchShown = true;
            scenarioHintOn = true;
            DialogManager.ShowDialog("First_time_touch", OnFirstTimeTouchEnd);
        }
    }

    public override string GetArchive()
    {
        var root = new JSONClass()
        {
            { "ToTheMagicShown", new JSONData(toTheMagicShown) },
            { "FirstTimeTouchShown", new JSONData(firstTimeTouchShown) }
        };
        return root.ToString();
    }

    public override void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        toTheMagicShown = root["ToTheMagicShown"].AsBool;
        firstTimeTouchShown = root["FirstTimeTouchShown"].AsBool;
    }

    void OnToTheMagicShownEnd()
    {
        scenarioHintOn = false;
    }

    void OnFirstTimeTouchEnd()
    {
        scenarioHintOn = false;
    }

    public override Vector3 GetPlayerInitPos(string lastSceneName)
    {
        return new Vector3(0f, -2.83f, 0f);
    }
}
