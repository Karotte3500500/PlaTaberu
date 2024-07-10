using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GetFPS : MonoBehaviour
{
    private float time = 0;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (!GlobalSwitch._DisplaysFPS)
            return;

        time += Time.deltaTime;
        if (time > 0.5)
        {
            time = 0;
            this.gameObject.GetComponent<Text>().text = $"FPSÅF{(1f / Time.deltaTime):##.00}";
        }
    }
}
