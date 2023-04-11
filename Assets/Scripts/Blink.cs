using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Blink : MonoBehaviour
{
    public float blinkTime = 0.75f;

    private Image image;
    private float switchTime = 0f;
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= switchTime)
		{
            image.enabled = !image.isActiveAndEnabled;
            switchTime += blinkTime;
		}
    }

}
