using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : Interoperable {

    public PlayerManager Apkal;
    public string dialogSection;
    public Level1S1Scenario scenario;
    public Animator doorAnimator;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Mathf.Abs(Apkal.transform.position.x - transform.position.x);
        if (distance > detectDist)
        {
            Apkal.floorLayer = 0;
            doorAnimator.SetBool("open", true);
        }
	}
    public override void WithinRange()
    {
        scenario.GoToTrap();
        Apkal.floorLayer = 1;
        doorAnimator.SetBool("open", false);
    }
}
