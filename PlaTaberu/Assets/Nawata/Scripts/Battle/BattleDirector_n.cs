using GameCharacterManagement;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BattleDirector_n : MonoBehaviour
{
    [SerializeField]
    private GameObject friendImg;
    [SerializeField]
    private GameObject enemyImg;

    [SerializeField]
    private CharacterManager_n[] characterImg = new CharacterManager_n[2];

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

    /*メニューが表示されているか*/
    public bool OpeningMenu = false;

    private Commands_n commands;
    private Plataberu[] plataberus = new Plataberu[2] { CharacterData._Plataberu, GlobalValue.enemy };
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

        //CharacterManagerのコンポーネントを取得
        characterImg = new CharacterManager_n[2]
        {
            friendImg.GetComponent<CharacterManager_n>(),
            enemyImg.GetComponent<CharacterManager_n>(),
        };

        //味方のアバターを設定
        characterImg[0].Layer = 2;
        characterImg[0].ID = plataberus[0].ID;
        //敵のアバターを設定
        characterImg[1].Layer = 1;
        characterImg[1].ID = plataberus[1].ID;
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
    //戦闘処理をする
    private void RunBattle()
    {
        //デバッグ用
        int len = setTurn(plataberus[0]) > setTurn(plataberus[1]) ? setTurn(plataberus[0]) : setTurn(plataberus[1]);
        Debug.Log(len);
        for (int i = 0; i < len; i++)
        {
            action(0, i);
            action(1, i);
        }
    }

    //アニメーションを実行する
    private void RunBattleAnimation(int beru1, int beru2, int trun)
    {
        characterImg[beru1].CharacterAnimation
            = plataberus[beru1].BattleCommand.SelectedCommand[trun] + 4;

        //攻撃を受ける
        if (plataberus[beru1].BattleCommand.SelectedCommand[trun] == 0 && plataberus[beru2].BattleCommand.SelectedCommand[trun] != 1) ;
            characterImg[beru2].CharacterAnimation = 7;
    }

    private int count = 0;
    private void MoveBattle()
    {
        if (BattleRunned)
        {
            //ばちょるシーンじゃ
            count++;

            if (count % 100 == 0)
            {
                int num = (count / 100);
                RunBattleAnimation(num % 2, 1 - num % 2, (count / 100));
            }

            if (count > 100 * (setTurn(plataberus[0]) + setTurn(plataberus[1])))
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
            /*デバッグ用*/
            EnemyCommandSet();
            Debug.Log(plataberus[0].DebugString());
            Debug.Log(plataberus[1].DebugString());


            RunBattle();
            count = 0;
            BattleRunned = true;
        }
    }

    //デバッグ用
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
