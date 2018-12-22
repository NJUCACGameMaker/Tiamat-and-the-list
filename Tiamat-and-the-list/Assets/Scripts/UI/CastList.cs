using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastList : MonoBehaviour {

    public Vector2 from;
    public Vector2 to;
    public RectTransform list;
    public float moveSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        list.anchoredPosition = from;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(list.anchoredPosition, to) >= moveSpeed)
        {
            list.anchoredPosition = list.anchoredPosition + (Vector2)Vector3.Normalize(to - from) * moveSpeed;
        }
        else
        {
            list.anchoredPosition = to;
            SceneManager.LoadScene("Cover");
        }
	}
}
