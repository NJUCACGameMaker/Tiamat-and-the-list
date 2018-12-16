using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour{

    void Start()
    {
        previousNum = Random.Range(0, 7) + Random.Range(0, 7) * 10;
        anotherNum = Random.Range(0, 7) + Random.Range(0, 7) * 10;
    }

    private int currentNum = 0;
    public int CurrentNum
    {
        get
        {
            return currentNum;
        }
    }
    private int previousNum = -1;
    //上一个房间是否为右侧房间
    private bool previousRight = true;
    private int anotherNum = 0;
    private int wanderCount = 0;
    //是否完成一次来回
    private bool roundFinish = true;

    [HideInInspector]
    public bool puzzleFinish = false;

    private void Wander()
    {
        if (roundFinish)
        {
            roundFinish = false;
        }
        else
        {
            roundFinish = true;
            wanderCount++;
        }
        if (wanderCount >= 7)
        {
            puzzleFinish = true;
        }
    }

	public int GetRight()
    {
        if (previousRight) return previousNum;
        else return anotherNum;
    }

    public int GetLeft()
    {
        if (previousRight) return anotherNum;
        else return previousNum;
    }

    public int GoRight()
    {
        int tempCurrent = currentNum;
        currentNum = GetRight();
        previousNum = tempCurrent;
        previousRight = false;
        anotherNum = Random.Range(0, 7) + Random.Range(0, 7) * 10;
        if ((previousNum % 10 == 0 && currentNum % 10 == 6) || 
            (previousNum % 10 == 6 && currentNum % 10 == 0))
        {
            Wander();
        }
        return currentNum;
    }

    public int GoLeft()
    {
        int tempCurrent = currentNum;
        currentNum = GetLeft();
        previousNum = tempCurrent;
        previousRight = true;
        anotherNum = Random.Range(0, 7) + Random.Range(0, 7) * 10;
        if ((previousNum % 10 == 0 && currentNum % 10 == 6) ||
            (previousNum % 10 == 6 && currentNum % 10 == 0))
        {
            Wander();
        }
        return currentNum;
    }

}
