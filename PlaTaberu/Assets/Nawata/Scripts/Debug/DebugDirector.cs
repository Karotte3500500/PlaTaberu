using System.Collections;
using System.Collections.Generic;
using GameCharacterManagement;
using UnityEngine;

public class DebugDirector : MonoBehaviour
{
    //�L�����N�^�[��ύX����
    public void Change(int id)
    {
        CharacterData._Plataberu = PlataberuManager.GrowUp(CharacterData._Plataberu, id);
    }
}
