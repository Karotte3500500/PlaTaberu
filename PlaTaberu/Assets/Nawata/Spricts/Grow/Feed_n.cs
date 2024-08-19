using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feed_n : MonoBehaviour
{
    [SerializeField]
    private Button feedBotton;

    public int cost = 99;

    void Update()
    {
        if(CharacterData._PlasticNum < cost)
        {
            feedBotton.interactable = false;
        }
        else
        {
            feedBotton.interactable = true;
        }
    }

    public void Feed()
    {

        CharacterData._PlasticNum -= cost;

    }
}
