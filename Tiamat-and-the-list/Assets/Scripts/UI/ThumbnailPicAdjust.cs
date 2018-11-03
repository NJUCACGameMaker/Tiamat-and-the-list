using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//缩略图调整
public class ThumbnailPicAdjust : MonoBehaviour {

    private float width;
    private float height;
    //因伸展（即AnchorMin和Max不一致）造成被隐藏的高度
    public float stretchedHeight;
    private Sprite sprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void Adjust()
    {
        width = GetComponent<RectTransform>().sizeDelta.x;
        height = GetComponent<RectTransform>().sizeDelta.y + stretchedHeight;
        sprite = GetComponent<Image>().sprite;
        if (sprite != null)
        {
            float spWidth = sprite.rect.width;
            float spHeight = sprite.rect.height;

            if (width * spHeight >= height * spWidth)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(spWidth * height / spHeight, height - stretchedHeight);
            }
            else
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(width, width * spHeight / spWidth - stretchedHeight);
            }
        }
    }
}
