using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialS1Scenario : Scenario {

    public GameObject player;
    public GameObject deadBody;
    private Vector3 deadBodyOriginalPos;


    private bool scene1Shown = false;
    private bool pressEHint = false;
    private bool itemPickHint = false;
    private bool doorOpenHint = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!scene1Shown)
        {
            scene1Shown = true;
            scenarioHintOn = true;
            deadBodyOriginalPos = deadBody.transform.position;
            deadBody.transform.position = new Vector3(0.0f, -100.0f);
            DialogManager.ShowDialog("Scence1", OnScene1End);
        }

        if (!pressEHint && player.transform.position.x > 18.15f)
        {
            pressEHint = true;
            scenarioHintOn = true;
            DialogManager.ShowDialog("Hint1", OnPressEHintEnd);
        }

        if (!itemPickHint && player.transform.position.x > 46.5f)
        {
            itemPickHint = true;
            scenarioHintOn = true;
            DialogManager.ShowDialog("Hint2", OnPickItemHintEnd);
        }

        if (!doorOpenHint && player.transform.position.x > 53.2f)
        {
            doorOpenHint = true;
            scenarioHintOn = true;
            DialogManager.ShowDialog("Hint3", OnDoorOpenHintEnd);
        }
	}

    void OnScene1End()
    {
        scenarioHintOn = false;
        deadBody.transform.position = deadBodyOriginalPos;
    }

    void OnPressEHintEnd()
    {
        scenarioHintOn = false;
    }

    void OnPickItemHintEnd()
    {
        scenarioHintOn = false;
    }

    void OnDoorOpenHintEnd()
    {
        scenarioHintOn = false;
    }
}
