using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCharacterManagement;

public class CharacterManager_n : MonoBehaviour
{
    public int ID;
    private int characterID = -1;

    private GameObject charObj;

    [SerializeField]
    private List<GameObject> plataberus;

    //�L�����N�^�[�̓���
    public int CharacterAnimation = 0;
    //�L�����N�^�[�̕\��
    public int CharacterFace = 0;

    /*���ʂ�\������*/
    public bool Exclamation = false;
    public bool Question = false;
    public bool tere = false;
    public bool douyo = false;

    private void Start()
    {
        ID = CharacterData._Plataberu.ID;
    }
    private void Update()
    {
        if(characterID != ID)
        {
            if (charObj != null)
                Destroy(charObj);

            charObj = Instantiate(plataberus[ID], this.transform);
            characterID = ID;
        }
    }
}
