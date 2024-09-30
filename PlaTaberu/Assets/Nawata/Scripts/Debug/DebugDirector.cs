using System.Collections;
using System.Collections.Generic;
using GameCharacterManagement;
using UnityEngine;

public class DebugDirector : MonoBehaviour
{
    //キャラクターを変更する
    public void Change(int id)
    {
        CharacterData._Plataberu = PlataberuManager.GrowUp(CharacterData._Plataberu, id);
    }
}
