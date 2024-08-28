using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionDirector : MonoBehaviour
{
    Image image;

    private void Start()
    {
        image = this.GetComponent<Image>();
    }

    private void Update()
    {
        Color color = image.color;
        color.a += 0.02f;
        image.color = color;

        if (color.a >= 1)
            SceneManager.LoadScene(GlobalSwitch.SwitchingScenes);
    }
}
