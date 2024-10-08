using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContensDirector_n : MonoBehaviour
{
    [SerializeField]
    private GameObject page;
    [SerializeField]
    private GameObject scroll;

    private void Start()
    {
        foreach(var id in PlayerData._RecodedPlataberu)
        {
            GameObject pObje = Instantiate(page, scroll.transform);
            pObje.GetComponent<Page_n>().CharacterID = id;
        }
    }
}
