using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CameraManagerForLevelOne : MonoBehaviour
{
    public PlayerManager MainPlayer;
    private int moveSpeed = 4;
    public float size;

    private float targetLeft;
    private float targetRight;

    private void Start()
    {
        targetLeft =- size / 2;
        targetRight = size / 2;
    }

    void Update()
    {
        if (!MainPlayer.getCanMoved())
        {
            Transform player = MainPlayer.getSkillTransform();
            if (player != null)
            {
                if (player.position.x >= targetRight)
                {
                    Vector3 targetCamPos = new Vector3(targetRight + size/2, 0, 0);
                    if (Mathf.Abs(transform.position.x - targetRight-size/2f)>=0.001)
                    {
                        Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime);
                        transform.position = new Vector3(lerp.x, transform.position.y, transform.position.z);
                    }
                    else
                    {
                        targetRight = targetRight + size;
                        targetLeft = targetLeft + size;
                        Debug.Log("ok");
                    }
                }
                else if (player.position.x <= targetLeft)
                {
                    Vector3 targetCamPos = new Vector3(targetLeft-size/2, 0, 0);
                    if (Mathf.Abs(transform.position.x - targetLeft + size / 2f) >= 0.001)
                    {
                        Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime);
                        transform.position = new Vector3(lerp.x, transform.position.y, transform.position.z);
                    }
                    else
                    {
                        targetRight = targetRight - size;
                        targetLeft = targetLeft - size;
                    }
                }
            }
        }
    }
}
