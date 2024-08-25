using System.Collections;
using System.Collections.Generic;
using GameCharacterManagement;
using UnityEngine;
using UnityEngine.UI;

public class Commands_n : MonoBehaviour
{
    [SerializeField]
    private GameObject[] commands;
    [SerializeField]
    private Sprite[] icons;
    [SerializeField]
    private Text cost;

    private Plataberu myChar = CharacterData._Plataberu;
    private Plataberu enemy = ServerCommunication._EnemyCharacter;
    
    public int[] PopComs = new int[5];

    private void Start()
    {
        MyTime();
    }

    // Update is called once per frame
    private void Update()
    {
        cost.text = $"{myChar.BattleCommand.Cost}";
    }

    private void MyTime()
    {
        PopComs = myChar.BattleCommand.PopCommand;
        for (int i = 0; i < PopComs.Length; i++)
        {
            commands[i].GetComponent<Image>().sprite = icons[PopComs[i]];
            Text costNum = commands[i].transform.GetChild(0).GetComponent<Text>();
            switch (PopComs[i])
            {
                case 0:
                    costNum.text = $"{myChar.BattleCommand.AttackCost}";
                    break;
                case 1:
                    costNum.text = $"{myChar.BattleCommand.DefenseCost}";
                    break;
                case 2:
                    costNum.text = $"{myChar.BattleCommand.SkillCost}";
                    break;
            }


        }
    }
}
