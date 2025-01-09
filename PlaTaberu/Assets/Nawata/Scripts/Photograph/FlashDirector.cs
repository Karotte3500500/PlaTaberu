using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashDirector : MonoBehaviour
{
    private float Speed = 0;
    private bool isLightable = false;

    private float timeCount = 1;
    private Image img;
    private void Start()
    {
        img = this.GetComponent<Image>();
        img.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    private void Update()
    {
        if (isLightable)
        {
            lightFlash();
        }
    }

    public void Light(float speed)
    {
        if (speed <= 0)
            speed = 1;
        Speed = speed;
        timeCount = 1;
        isLightable = true;
    }
    
    private void lightFlash()
    {
        Color color = img.color;
        color.a = 1.0f * timeCount;

        timeCount -= (Speed / 100);

        img.color = color;

        if(color.a < 0.001f)
            isLightable = false;
    }
}
