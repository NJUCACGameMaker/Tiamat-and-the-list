using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CameraManagerForLevelOne : MonoBehaviour
{
    public PlayerManager MainPlayer;
    private int moveSpeed = 4;
    public float size;
    public float maxH;

    public float targetLeft;
    public float targetRight;

    private void Start()
    {
       
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
                    Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime);
                    transform.position = new Vector3(lerp.x, transform.position.y, transform.position.z);
                }
                else if (player.position.x < targetRight && player.position.x > targetLeft)
                {
                    Vector3 targetCamPos = new Vector3((targetLeft +targetRight) / 2, 0, 0);
                    Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime);
                    transform.position = new Vector3(lerp.x, transform.position.y, transform.position.z);
                }
                else if (player.position.x <= targetLeft)
                {
                    Vector3 targetCamPos = new Vector3(targetLeft-size/2, 0, 0);
                    Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime);
                    transform.position = new Vector3(lerp.x, transform.position.y, transform.position.z);
                }
            }
        }
        else
        {
            if (MainPlayer.transform.position.x >= targetRight)
            {
                Vector3 targetCamPos = new Vector3(targetRight + size / 2, 0, 0);
                Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime);
                transform.position = new Vector3(lerp.x, transform.position.y, transform.position.z);
            }
            else if (MainPlayer.transform.position.x < targetRight && MainPlayer.transform.position.x > targetLeft)
            {
                Vector3 targetCamPos = new Vector3((targetLeft + targetRight) / 2, 0, 0);
                Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime);
                transform.position = new Vector3(lerp.x, transform.position.y, transform.position.z);
            }
            else if (MainPlayer.transform.position.x <= targetLeft)
            {
                Vector3 targetCamPos = new Vector3(targetLeft - size / 2, 0, 0);
                Vector3 lerp = Vector3.Lerp(new Vector3(transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime);
                transform.position = new Vector3(lerp.x, transform.position.y, transform.position.z);
            }
        }
        if (transform.position.y > maxH)
            transform.position = new Vector3(transform.position.x, maxH,transform.position.z);
    }
}
