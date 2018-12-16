using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tran_Left : Interoperable
{

    public PlayerManager Apkal;
    public Tran_Right Right;
    public MazeController Maze;
    public bool Tran = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(Apkal.transform.position.x - transform.position.x);
        if (!Tran && distance > detectDist)
            Tran = true;

    }
    public override void WithinRange()
    {
        if (Tran)
        {
            Maze.GetLeft();
            Apkal.transform.position = new Vector3(6, Apkal.transform.position.y, 0);
            Right.Tran = false;
           
        }
    }

    
}
