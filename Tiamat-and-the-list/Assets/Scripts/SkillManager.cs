using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;
using SimpleJSON;

public class SkillManager : MonoBehaviour
{

    public float maxX;
    public float minX;

    //移动速度
    public float moveSpeed = 8.0f;

    //当前移动速度
    private float currentSpeed = 0f;
    private float lastPositionX;

    //音效控制器
    public AudioClip audioTorchSwitch;
    private AudioSource audioSource;
    

    public bool isLeft;
    // Use this for initialization
    void Start()
    {
        InputManager.AddOnLeftMove(SkillLeftMove);
        InputManager.AddOnRightMove(SkillRightMove);
        lastPositionX = this.transform.position.x;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioTorchSwitch;
    }

    private void OnDestroy()
    {
        InputManager.RemoveLeftMove(SkillLeftMove);
        InputManager.RemoveRightMove(SkillRightMove);
    }

    public void SetLeft(bool isLeft)
    {
        if (isLeft)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            this.isLeft = true;
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            this.isLeft = false;
        }
    }

    void SkillLeftMove()
    {
        float playerX = transform.position.x;
        float bg_1_x = minX;
        //transform.LookAt(new Vector3(transform.position.x-5,transform.position.y,transform.position.z));
        if (!isLeft)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isLeft = true;
        }
        if (playerX >= bg_1_x)
        {
            transform.Translate(Time.deltaTime * Vector3.left * moveSpeed, Space.World);
        }

    }

    void SkillRightMove()
    {
        float playerX = transform.position.x;
        float bg_1_x = maxX;
        //transform.LookAt(new Vector3(transform.position.x + 5, transform.position.y, transform.position.z));
        if (isLeft)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isLeft = false;
        }
        if (playerX <= bg_1_x)
        {
            transform.Translate(Time.deltaTime * Vector3.right * moveSpeed, Space.World);
        }

    }
}
