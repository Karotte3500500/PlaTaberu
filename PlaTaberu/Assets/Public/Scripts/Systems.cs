using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Systems : MonoBehaviour
{
    private ControlUI controlUI;
    private void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();
        controlUI.SetUI((GameObject)Resources.Load("Nawata/FPS"));
    }
}
