using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCharacterManagement;

public class Feed_n : MonoBehaviour
{
    private ControlUI controlUI;
    private Plataberu myChara = CharacterData._Plataberu;
    private int upLevel;

    [SerializeField]
    private Button feedBotton;
    [SerializeField]
    private GameObject lvUpUI;

    private GameObject lvUpUIobj;
    private int count = 0;
    public int cost = 10;

    private void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();
    }

    private void Update()
    {
        if (upLevel == 0)
        {
            if (CharacterData._PlasticNum < cost)
            {
                feedBotton.interactable = false;
            }
            else
            {
                feedBotton.interactable = true;
            }
        }
        else if(count < 80)
        {
            if (count == 0)
            {
                lvUpUIobj = controlUI.SetUI(lvUpUI);
                feedBotton.interactable = false;
                Debug.Log(myChara.DebugString());
            }
            count++;
        }
        else
        {
            Destroy(lvUpUIobj);
            count = 0;
            upLevel = 0;
        }
    }

    public void Feed()
    {

        CharacterData._PlasticNum -= cost;
        myChara.AddGrp(cost);
        upLevel = myChara.LevelUp();
    }
}
