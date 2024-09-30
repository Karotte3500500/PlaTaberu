using System.Collections;
using System.Collections.Generic;
using GameCharacterManagement;
using UnityEngine;

public class BookCharacter_n : MonoBehaviour
{
    [SerializeField]
    private GameObject character;

    void Start()
    {

    }

    void Update()
    {
        //キャラクターIDが変化した場合、キャラクターを変更する
        if (character.GetComponent<CharacterManager_n>().ID != GlobalSwitch._CharacterID)
            character.GetComponent<CharacterManager_n>().ID = GlobalSwitch._CharacterID;
    }
}
