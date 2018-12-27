using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour{

    public static MazeController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
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
    public bool puzzleFinished = false;

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
        if (wanderCount >= 3)
        {
            puzzleFinished = true;
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

    public static int GoRight() { return instance._GoRight(); }
    public int _GoRight()
    {
        int tempCurrent = currentNum;
        currentNum = GetRight();
        previousNum = tempCurrent;
        previousRight = false;
        do
        {
            anotherNum = RandomOne() + Random.Range(0, 7) * 10;
        } while (anotherNum == currentNum);
        if ((previousNum % 10 == 0 && currentNum % 10 == 6) || 
            (previousNum % 10 == 6 && currentNum % 10 == 0))
        {
            Wander();
        } else {
            wanderCount = 0;
        }
        Debug.Log(previousNum + " " + currentNum + " " + anotherNum);
        return currentNum;
    }

    public static int GoLeft() { return instance._GoLeft(); }
    public int _GoLeft()
    {
        int tempCurrent = currentNum;
        currentNum = GetLeft();
        previousNum = tempCurrent;
        previousRight = true;
        anotherNum = RandomOne() + Random.Range(0, 7) * 10;
        if ((previousNum % 10 == 0 && currentNum % 10 == 6) ||
            (previousNum % 10 == 6 && currentNum % 10 == 0))
        {
            Wander();
        }
        Debug.Log(previousNum + " " + currentNum + " " + anotherNum);
        return currentNum;
    }

    public static bool PuzzleFinished() { return instance.puzzleFinished; }

    public static int GetCurrentNumber() { return instance.CurrentNum; }

    private int RandomOne()
    {
        int temp = Random.Range(0, 8);
        if (temp <= 6) return temp;
        else
        {
            if (currentNum % 10 == 6) return 0;
            else
            {
                return 6;
            }
        }
    }
}
