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

    // Update is called once per frame
    void Update()
    {
        if (character.GetComponent<CharacterManager_n>().ID != GlobalSwitch._CharacterID)
            character.GetComponent<CharacterManager_n>().ID = GlobalSwitch._CharacterID;
    }
}
