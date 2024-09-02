using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCharacterManagement;

public class CharacterManager_n : MonoBehaviour
{

    public virtual Plataberu character { get { return CharacterData._Plataberu; } }

    private GameObject charObj;

    [SerializeField]
    private List<GameObject> plataberus;

    //キャラクターの動き
    public int CharacterAnimation = 0;
    //キャラクターの表情
    public int CharacterFace = 0;

    /*効果を表示する*/
    public bool Exclamation = false;
    public bool Question = false;
    public bool tere = false;
    public bool douyo = false;

    private void Start()
    {
        charObj = Instantiate(plataberus[character.ID], this.transform);
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    void Update()
    {
        
    }
}
