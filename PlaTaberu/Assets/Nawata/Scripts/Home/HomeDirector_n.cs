using System.Collections;
using System.Collections.Generic;
using GameCharacterManagement;
using UnityEngine.UI;
using UnityEngine;

public class HomeDirector_n : MonoBehaviour
{
    [SerializeField]
    private Text charName;
    [SerializeField]
    private Text charLevel;
    [SerializeField]
    private Text charType;
    [SerializeField]
    private Text[] plastics;

    private Plataberu myChar = CharacterData._Plataberu;

    private void Update()
    {
        charName.text = myChar.Name;
        charLevel.text = $"{myChar.Level}";
        charType.text = myChar.GrowthType;

        plastics[0].text = $"{CharacterData._RedPlastic}";
        plastics[1].text = $"{CharacterData._GreenPlastic}";
        plastics[2].text = $"{CharacterData._BluePlastic}";
    }
}
