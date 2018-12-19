using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForLevelOneScOne : MonoBehaviour {

    public float targetRight;
    public float distanceRight;
    public float distanceLeft;
    public float maxH;

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
                if (player.position.x >= targetRight)
                {
                    Vector3 targetCamPos = new Vector3(targetRight + distanceRight, 0, 0);
                    Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime);
                    transform.position = new Vector3(lerp.x, transform.position.y, transform.position.z);

                }
                else if (player.position.x < targetRight)
                {
                    Vector3 targetCamPos = new Vector3(targetRight-distanceLeft, 0, 0);
                    Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime);
                    transform.position = new Vector3(lerp.x, transform.position.y, transform.position.z);
                }
            }
        }
        else
        {
            if (MainPlayer.transform.position.x < targetRight)
            {
                Vector3 targetCamPos = new Vector3(targetRight - distanceLeft, 0, 0);
                Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime);
                transform.position = new Vector3(lerp.x, transform.position.y, transform.position.z);
            }
            else
            {
                Vector3 targetCamPos = new Vector3(targetRight + distanceRight, 0, 0);
                Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime);
                transform.position = new Vector3(lerp.x, transform.position.y, transform.position.z);
            }
        }
        if (transform.position.y > maxH)
            transform.position = new Vector3(transform.position.x, maxH, transform.position.z);
    }
}
