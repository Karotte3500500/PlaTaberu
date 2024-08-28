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
    private GameObject decide;
    [SerializeField]
    private Sprite[] icons;
    [SerializeField]
    private Text cost;

    private Plataberu myChar = CharacterData._Plataberu;
    private Plataberu enemy = ServerCommunication._EnemyCharacter;
    private int[] costs = new int[3];
    private bool once = true;

    public int[] PopComs = new int[5];
    public bool choicing = true;

    private void Start()
    {
        costs = new int[3]
        {
            myChar.BattleCommand.AttackCost,
            myChar.BattleCommand.DefenseCost,
            myChar.BattleCommand.SkillCost
        };
        choicing = true;
        MyTime();
    }

    private void Update()
    {
        cost.text = $"{myChar.BattleCommand.Cost}";

        if (choicing)
        {
            if (once)
            {
                once = false;
                MyTime();
            }
            int index = 0;
            foreach(var com in PopComs)
            {
                if (myChar.BattleCommand.Cost < costs[com])
                    commands[index].GetComponent<Button>().interactable = false;
                index++;
            }
        }
        else
        {
            choicing = true;
        }
    }

    private void MyTime()
    {
        decide.SetActive(true);
        PopComs = myChar.BattleCommand.PopCommand;
        for (int i = 0; i < PopComs.Length; i++)
        {
            commands[i].GetComponent<Button>().interactable = true;
            commands[i].GetComponent<Image>().sprite = icons[PopComs[i]];
            Text costNum = commands[i].transform.GetChild(0).GetComponent<Text>();
            costNum.text = $"{costs[PopComs[i]]}";
        }
    }

    public void select(int num)
    {
        commands[num].GetComponent<Button>().interactable = false;
        myChar.BattleCommand.SelectedCommand.Add(PopComs[num]);
        myChar.BattleCommand.Cost -= costs[PopComs[num]];
    }

    public void Decide()
    {
        foreach(var coms in commands)
        {
            coms.GetComponent<Button>().interactable = false;
        }
        decide.SetActive(false);
        choicing = false;
        once = true;
    }
}
