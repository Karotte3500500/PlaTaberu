using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCharacterManagement;

public class CharacterManager_n : MonoBehaviour
{

    public Plataberu character = CharacterData._Plataberu;

    private GameObject charObj;

    [SerializeField]
    private List<GameObject> plataberus;

    public int CharcterAnimation = 0;

    void Start()
    {
        charObj = Instantiate(plataberus[character.ID], this.transform);
        this.transform.localScale = new Vector3(0.26f, 0.26f, 0.26f);
    }

    void Update()
    {
        
    }
}
