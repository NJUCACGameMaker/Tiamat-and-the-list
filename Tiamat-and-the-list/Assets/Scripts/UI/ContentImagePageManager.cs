using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImagePageManager : MonoBehaviour {

    public RectTransform image;
    public RectTransform text;
	
	// Update is called once per frame
	void Update () {
        text.localPosition = new Vector3(0.0f, -image.sizeDelta.y, 0.0f);
        GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, image.sizeDelta.y + text.sizeDelta.y);
	}
}
