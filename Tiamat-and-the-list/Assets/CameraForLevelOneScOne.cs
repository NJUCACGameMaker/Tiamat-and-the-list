using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForLevelOneScOne : MonoBehaviour {

    public float maxH;

    public float maxX;
    public float minX;

    public PlayerManager MainPlayer;

    private int moveSpeed = 4;


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!MainPlayer.getCanMoved())
        {
            Transform player = MainPlayer.getSkillTransform();
            if (player != null)
            {
                Vector3 targetCamPos = new Vector3(player.position.x, player.position.y + 2.54f, 0);
                if (Mathf.Abs(transform.position.x - player.position.x) >= 0.001)
                {
                    Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, 0), targetCamPos, moveSpeed * Time.deltaTime);
                    transform.position = new Vector3(lerp.x, lerp.y, transform.position.z);
                }
                
            }
        }
        else
        {
            Vector3 targetCamPos = new Vector3(MainPlayer.transform.position.x, MainPlayer.transform.position.y + 2.54f, 0);
            if (Mathf.Abs(transform.position.x - MainPlayer.transform.position.x) >= 0.001)
            {
                Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, 0), targetCamPos, moveSpeed * Time.deltaTime);
                transform.position = new Vector3(lerp.x, lerp.y, transform.position.z);
            }
            
        }
        if (transform.position.x < minX)
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        if (transform.position.x > maxX)
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        if (transform.position.y > maxH)
        {
            transform.position = new Vector3(transform.position.x, maxH, transform.position.z);
        }
    }
}
