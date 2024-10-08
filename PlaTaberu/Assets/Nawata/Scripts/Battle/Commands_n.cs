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
    private bool[] selectedCommands = new bool[5];
    [SerializeField]
    private GameObject decide;
    [SerializeField]
    private Sprite[] icons;
    [SerializeField]
    private Text cost;

    private BattleDirector_n battleDirector;
    private Plataberu myChar = CharacterData._Plataberu;
    private Plataberu enemy = ServerCommunication._EnemyCharacter;
    private int[] costs = new int[3];
    private bool once = true;
    private bool resetedComs = false;

    public int[] PopComs = new int[5];
    //�R�}���h��I������p�[�g�Ȃ�ture
    public bool choicing = true;

    private void Start()
    {
        battleDirector = FindObjectOfType<BattleDirector_n>();
        costs = new int[3]
        {
            myChar.BattleCommand.AttackCost,
            myChar.BattleCommand.DefenseCost,
            myChar.BattleCommand.SkillCost
        };

        choicing = true;
        for(int i = 0; i < selectedCommands.Length; i++)
            selectedCommands[i] = true;
//        MyTime();
    }

    private void Update()
    {
        //���j���[���J���Ă���Ԃ͑���s��
        int comsIndex = 0;

        if(battleDirector.OpeningMenu && !resetedComs)
        {
            foreach (var com in commands)
            {
                selectedCommands[comsIndex] = com.GetComponent<Button>().interactable;
                com.GetComponent<Button>().interactable = false;
                resetedComs = true;
            }
            comsIndex++;
        }

        if(!battleDirector.OpeningMenu && resetedComs)
        {
            foreach (var com in commands)
            {
                com.GetComponent<Button>().interactable = selectedCommands[comsIndex];
                resetedComs = false;
                comsIndex++;
            }
        }

        //�R�X�g��\��
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
            //choicing = true;
        }
    }

    //�R�}���h�̃V���b�t���ƃ��U���g
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

    //�R�}���h���I�����ꂽ�ꍇ�̏���
    public void select(int num)
    {
        commands[num].GetComponent<Button>().interactable = false;
        myChar.BattleCommand.SelectedCommand.Add(PopComs[num]);
        myChar.BattleCommand.Cost -= costs[PopComs[num]];
    }

    //�R�}���h�I�����m�肷��
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
