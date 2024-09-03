using GameCharacterManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDirector_n : MonoBehaviour
{
    /*プレイヤーが操作するプラタベル*/
    [SerializeField]
    private GameObject character;
    /*UI*/
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject[] hpBar;
    [SerializeField]
    private Button decide;

    /**/
    public bool OpeningMenu = false;

    private Commands_n commands;
    private Plataberu[] plataberus = new Plataberu[2] { CharacterData._Plataberu, ServerCommunication._EnemyCharacter };
    private bool BattleRunned = false;

    private void Start()
    {
        foreach (var beru in plataberus)
        {
            /*デバッグ用*/
            beru.AddGrp(10000);
            beru.LevelUp();

            //ほんぺ
            beru.BattleStatusReset();
        }
        menu.SetActive(false);
        commands = FindObjectOfType<Commands_n>();
    }

    private void Update()
    {
        /*メニューが開いているなら処理を中止する*/
        OpeningMenu = menu.activeSelf;
        decide.interactable = !OpeningMenu;
        if (OpeningMenu) return;

        /*戦闘の処理*/
        if (!commands.choicing) 
        {
            MoveBattle();
        }

        //HPバーの処理
        setHpbar(0);
        setHpbar(1);
    }

    private int setTurn(Plataberu beru)
    {
        int turn = 0;
        foreach (var com in beru.BattleCommand.SelectedCommand)
            turn++;
        return turn;
    }

    private void action(int num,int turn)
    {
        if (turn > setTurn(plataberus[num]) - 1) return;
        plataberus[num].BattleMove(plataberus[num], plataberus[num == 0 ? 1 : 0], turn);
    }
    private void RunBattle()
    {
        int len = setTurn(plataberus[0]) > setTurn(plataberus[1]) ? setTurn(plataberus[0]) : setTurn(plataberus[1]);
        Debug.Log(len);
        for (int i = 0; i < len; i++)
        {
            action(0, i);
            action(1, i);
        }
    }

    private int count = 0;
    private void MoveBattle()
    {
        if (BattleRunned)
        {
            count++;
            if(count > 100)
            {
                //コマンド選択へ遷移
                commands.choicing = true;
                BattleRunned = false;
                foreach (var beru in plataberus)
                    beru.WaveReset();
            }
        }
        else
        {
            EnemyCommandSet();

            Debug.Log(plataberus[0].DebugString());
            Debug.Log(plataberus[1].DebugString());
            RunBattle();
            BattleRunned = true;
            count = 0;
        }
    }

    private void EnemyCommandSet()
    {
        int num = Random.Range(0, 4);
        for (int i = 0;i < num;i++)
        {
            plataberus[1].BattleCommand.SelectedCommand.Add(Random.Range(0, 3));
        }
    }

    private void setHpbar(int num)
    {
        hpBar[num].GetComponent<HPbar_n>().MaxValue = plataberus[num].ActualStatus.HP;
        hpBar[num].GetComponent<HPbar_n>().NowValue = plataberus[num].BattleStatus.HP;
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
    }
}
