using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportingRoleController : MonoBehaviour {
    
    public delegate void NoneParaFunc();

    //当前移动速度
    private float currentSpeed = 0f;
    private float lastPositionX;

    //移动速度
    public float moveSpeed = 4.0f;

    //角色动画控制器
    public Animator playerAnima;
    public bool isLeft = true;

    void Update()
    {
        float currentPositionX = this.transform.position.x;
        currentSpeed = Mathf.Abs(currentPositionX - lastPositionX) / Time.deltaTime;
        lastPositionX = currentPositionX;
        playerAnima.SetFloat("MoveSpeed", currentSpeed);
    }

    public void SetLeft(bool isLeft)
    {
        if (isLeft)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            this.isLeft = true;
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            this.isLeft = false;
        }
    }

    void LeftMove()
    {
        if (!isLeft)
        {
            SetLeft(true);
        }
        transform.Translate(Time.deltaTime * Vector3.left * moveSpeed, Space.World);
    }

    void RightMove()
    {
        if (isLeft)
        {
            SetLeft(false);
        }
        transform.Translate(Time.deltaTime * Vector3.right * moveSpeed, Space.World);
    }

    public IEnumerator MoveTo(Vector2 target, NoneParaFunc noneParaFunc)
    {
        Debug.Log("MoveTo");
        if (target.x > transform.position.x)
        {
            float offsetX = target.x - transform.position.x;
            float offsetY = target.y - transform.position.y;
            while (target.x > transform.position.x)
            {
                RightMove();
                float deltaX = moveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x,
                    deltaX / offsetX * offsetY + transform.position.y);
                yield return null;
            }
            transform.position = target;
        }
        else
        {
            float offsetX = transform.position.x - target.x;
            float offsetY = target.y - transform.position.y;
            while (target.x < transform.position.x)
            {
                LeftMove();
                float deltaX = moveSpeed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x,
                    deltaX / offsetX * offsetY + transform.position.y);
                yield return null;
            }
            transform.position = target;
        }
        noneParaFunc();
    }
}
