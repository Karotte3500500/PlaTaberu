using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowDirector_n : MonoBehaviour
{
    ControlUI controlUI;

    [SerializeField]
    private Text plaNum;
    [SerializeField]
    private Text pointNum;

    private void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();
    }

    private void Update()
    {
        plaNum.text = $"{CharacterData._PlasticNum}";
    }
}
