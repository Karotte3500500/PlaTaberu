using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowDirector_n : MonoBehaviour
{
    ControlUI controlUI;

    [SerializeField]
    private Text[] plastics;

    [SerializeField]
    private Text charName;

    private void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();
    }

    private void Update()
    {
        plastics[0].text = $"{CharacterData._RedPlastic}";
        plastics[1].text = $"{CharacterData._GreenPlastic}";
        plastics[2].text = $"{CharacterData._BluePlastic}";

        charName.text = $"{CharacterData._Plataberu.Name} lv.{CharacterData._Plataberu.Level}";
    }

    public void ReturnScene()
    {
        controlUI.SwitchScene(GlobalValue._PreviousScene);
    }
}
