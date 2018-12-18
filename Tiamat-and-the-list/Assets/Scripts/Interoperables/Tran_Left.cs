using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Tran_Left : Interoperable
{
    public PlayerManager Apkal;
    public Tran_Right right;
    public Image image;
    public TextMeshPro number;
    public string nextSceneName;

    public bool tran = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(Apkal.transform.position.x - transform.position.x);
        if (!tran && distance > detectDist)
            tran = true;

    }
    public override void WithinRange()
    {
        if (tran)
        {
            image.color = new Color(0, 0, 0, 1);
            Apkal.transform.position = new Vector3(6, Apkal.transform.position.y, 0);
            StartCoroutine(SetBackLight());
            right.tran = false;
            MazeController.GoLeft();
            number.text = MazeController.GetCurrentNumber().ToString();
            if (MazeController.PuzzleFinished())
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    IEnumerator SetBackLight()
    {
        for (int seconds = 0; seconds < 16; seconds++)
            yield return 0;
        while (image.color.a>0)
        {
            float A = (image.color.a)*0.9f;
            if (A > 0.02)
            {
                image.color = new Color(0, 0, 0, A);
            }
            else
                image.color = new Color(0,0,0, 0);
            yield return null;
        }
    }
    
}
