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

    private Plataberu[] plataberus = new Plataberu[2] { CharacterData._Plataberu, ServerCommunication._EnemyCharacter };

    private void Start()
    {
        foreach(var beru in plataberus)
            beru.BattleStatusReset();
        menu.SetActive(false);
    }

    private void Update()
    {
        
    }



    public void OpenMenu()
    {
        menu.SetActive(true);
    }
}
