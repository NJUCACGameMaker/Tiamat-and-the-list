using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Level1S1Scenario : Scenario {

    public SupportingRoleController roleController;

    private bool beforeFateShown = true;
    private bool goToTheTrapShown = false;
    private bool afterFateShown = false;
	
	// Update is called once per frame
	void Update () {
		if (!beforeFateShown)
        {
            beforeFateShown = true;
            scenarioHintOn = true;
            CollectionArchive.MusicCollect("GARSUMENE");
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
            { "GoToTheTrapShown", new JSONData(goToTheTrapShown) },
            { "AfterFateShown", new JSONData(afterFateShown) }
        };
        return root.ToString();
    }

    public override void LoadArchive(string archiveLine)
    {
        var root = JSON.Parse(archiveLine);
        beforeFateShown = root["BeforeFateShown"].AsBool;
        goToTheTrapShown = root["GoToTheTrapShown"].AsBool;
        afterFateShown = root["AfterFateShown"].AsBool;
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
        if (lastSceneName == "Level1-Scene3") {
            Debug.Log("GetPlayerInitPos");
            if (!afterFateShown)
            {
                StartCoroutine(roleController.MoveTo(new Vector3(10.0f, -3.0f, 0.0f), AfterFate));
            }
            return new Vector3(6.0f, -3f, 0.0f);
        }
        else return new Vector3(-7.5f, -3f, 0f);
    }

    void AfterFate()
    {
        afterFateShown = true;
        scenarioHintOn = true;
        DialogManager.ShowDialog("After_Fate", OnAfterFateShown);
    }

    void OnAfterFateShown()
    {
        scenarioHintOn = false;
    }
}
