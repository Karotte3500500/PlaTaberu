using GameCharacterManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDirector_n : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject menu;

    public List<float>[] damages = new List<float>[2];

    private Commands_n commands;
    private Plataberu[] plataberus = new Plataberu[2] { CharacterData._Plataberu, ServerCommunication._EnemyCharacter };

    private void Start()
    {
        foreach(var beru in plataberus)
            beru.BattleStatusReset();
        menu.SetActive(false);
        commands = FindObjectOfType<Commands_n>();
    }

    private int playTurn = -1;
    private void Update()
    {
        if (!commands.choicing && playTurn == -1) 
        {
            playTurn = 0;
            MoveBattle();
        }
    }


    public void OpenMenu()
    {
        menu.SetActive(true);
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
        if (turn > setTurn(plataberus[num])) return;
        plataberus[num].BattleMove(plataberus[num], plataberus[num == 0 ? 1 : 0], turn);
    }

    int waitNum = 0;
    int battleTurn = 0;
    private void MoveBattle()
    {
        if(waitNum == 0)
        {
            action(0, battleTurn);
        }
        if(waitNum > 30)
        {
            action(1, battleTurn);
            waitNum = 0;
            return;
        }
        waitNum++;
    }
}
