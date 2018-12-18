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

    //角色动画控制器
    public Animator playerAnima;

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


    // Update is called once per frame
    void Update()
    {
        float currentPositionX = this.transform.position.x;
        currentSpeed = Math.Abs(currentPositionX - lastPositionX) / Time.deltaTime;
        lastPositionX = currentPositionX;
        playerAnima.SetFloat("MoveSpeed", currentSpeed);
    }

    private void OnDestroy()
    {
        InputManager.RemoveLeftMove(SkillLeftMove);
        InputManager.RemoveRightMove(SkillRightMove);
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
