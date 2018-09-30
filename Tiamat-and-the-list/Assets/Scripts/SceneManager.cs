using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public List<Interoperable> interoperables;
    public Transform playerTrans;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SetNearPlayer();
	}

    void SetNearPlayer()
    {
        float radio = 2;
        Interoperable tempNearest = null;

        foreach (Interoperable interoperable in interoperables)
        {
            float distance = Mathf.Abs(playerTrans.position.x - interoperable.transform.position.x);
            
            if (distance <= interoperable.detectDist && radio > (distance / interoperable.detectDist))
            {
                radio = distance / interoperable.detectDist;
                tempNearest = interoperable;
            }
        }
        foreach (Interoperable interoperable in interoperables)
        {
            if (interoperable == tempNearest)
            {
                if (!interoperable.NearPlayer)
                {
                    interoperable.ShowHint();
                    interoperable.NearPlayer = true;
                    Debug.Log("ShowHintAbout:" + interoperable);
                }
            }
            else
            {
                if (interoperable.NearPlayer)
                {
                    interoperable.UnshowHint();
                    interoperable.NearPlayer = false;
                }
            }
        }
    }
}
