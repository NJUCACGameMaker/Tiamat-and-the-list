using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour {
    private float timer;
    public float targetTime = 4.0f;
    private GameObject loadImage;
    private GameObject loadText;

    private Color imageColor;
    private Color textColor;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        loadImage = transform.GetChild(0).gameObject;
        loadText = transform.GetChild(1).gameObject;
        imageColor = loadImage.GetComponent<Image>().color;
        textColor = loadText.GetComponent<Text>().color;

        setAlpha(0);
        timer = 0f;
	}

    private void setAlpha(float a)
    {
        loadImage.GetComponent<Image>().color = new Color(imageColor.r, imageColor.g, imageColor.b, a);
        loadText.GetComponent<Text>().color = new Color(textColor.r, textColor.g, textColor.b, a);
    }
	
	// Update is called once per frame
	void Update () {
        if (timer < 0.25f)
        {
            timer += Time.deltaTime;
            if (timer > 0.25f)
                timer = 0.25f;
            setAlpha(timer * 4);
        } else if (timer >= targetTime - 0.25f)
        {
            timer += Time.deltaTime;
            if (timer > targetTime)
                Destroy(this.gameObject);
            setAlpha((targetTime - timer) * 4);
        } else
        {
            timer += Time.deltaTime;
        }

	}
}
