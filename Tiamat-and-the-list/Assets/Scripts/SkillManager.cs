using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;
using SimpleJSON;

public class SkillManager : MonoBehaviour
{

    public List<float> maxX;
    public List<float> minX;

    //移动速度
    public float moveSpeed = 8.0f;

    //高度层，最低为0，向上递增，用于判断是否与道具在同一层从而判断是否可交互。
    public int floorLayer = 0;

    //当前移动速度
    private float currentSpeed = 0f;
    private float lastPositionX;

    //音效控制器
    public AudioClip audioTorchSwitch;
    private AudioSource audioSource;

    //角色动画控制器
    public Animator playerAnima;

    private bool isLeft = false;
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

    void SkillLeftMove()
    {
        Debug.Log(isLeft);
        Debug.Log("Leftmove");
        float playerX = transform.localPosition.x;
        Debug.Log(playerX);
        float bg_1_x = minX[floorLayer];
        Debug.Log(bg_1_x);
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
        Debug.Log("Rightmove");
        float playerX = transform.localPosition.x;
        float bg_1_x = maxX[floorLayer];
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
