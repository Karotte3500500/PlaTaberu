using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCharacterManagement;

public class LvUpUI_n : MonoBehaviour
{
    [SerializeField]
    private Text lv;
    [SerializeField]
    private Text status;
    [SerializeField]
    private Text statusNum;

    private Plataberu myChar = CharacterData._Plataberu;
    int count;
    int speed = 20;

    void Start()
    {
        count = 0;
        lv.text = $"{myChar.OldLevel} Å@ {myChar.Level}";
        status.text = "";
        statusNum.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(count == speed)
        {
            status.text += "H  P:";
            statusNum.text += $"{((int)myChar.ActualStatus.HP)}";
        }
        if(count == speed * 2)
        {
            status.text += "\nATK:";
            statusNum.text += $"\n{((int)myChar.ActualStatus.ATK)}";
        }
        if (count == speed * 3)
        {
            status.text += "\nDEF:";
            statusNum.text += $"\n{((int)myChar.ActualStatus.DEF)}";
        }
        count++;
    }
}
