using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class switching : MonoBehaviour
{
    ControlUI controlUI;

    public Button ReturnButton;

    private void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();

        ReturnButton.onClick.AddListener(OnClick);
    }

    public void OnClick() 
    {
        controlUI.SwitchScene("Grow"); 
    } 
}
