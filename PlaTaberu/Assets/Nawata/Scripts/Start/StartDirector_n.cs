using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDirector_n : MonoBehaviour
{
    ControlUI controlUI;

    private void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();
    }

    public void ToGrowScene()
    {
        controlUI.SwitchScene("Grow");
    }
}
