using System.Collections;
using System.Collections.Generic;
using GameCharacterManagement;
using UnityEngine;
using UnityEngine.UI;

public class Circulation_n : MonoBehaviour
{
    [SerializeField]
    private GameObject transition;
    [SerializeField]
    private CharacterManager_n characterManager;
    [SerializeField]
    private Text mess;

    private int count = 0;
    private float time = 0;
    private float co = 5;

    private ControlUI controlUI;
    private Plataberu myChar = CharacterData._Plataberu;

    void Start()
    {
        mess.text = "";
        transition.SetActive(false);
        time = 0;
        controlUI = FindObjectOfType<ControlUI>();
    }
    bool stoped = false;
    void Update()
    {
        if (stoped)
        {
            count++;
            switch (count)
            {
                case 100:
                    transition.GetComponent<RawImage>().color = Color.clear;
                    transition.SetActive(true);
                    break;
                case 110:
                    transition.GetComponent<RawImage>().color = Color.white;
                    break;
                case 290:
                    myChar = PlataberuManager.GrowUp(myChar, myChar.GrowUP());
                    CharacterData._Plataberu = myChar;
                    characterManager.ID = myChar.ID;
                    break;
                case 300:
                    transition.SetActive(false);
                    mess.text = $"‚¨‚ß‚Å‚Æ‚¤II\n{myChar.Name}‚É‚È‚Á‚½‚æ";
                    break;
                case 600:
                    controlUI.SwitchScene("Grow", true);
                    break;
            }                
        }
        else
        {
            stoped = motion();
        }
    }

    private bool motion()
    {
        time += 0.01f;
        co -= 0.01f;

        Vector2 pos = this.transform.position;
        pos.y = Mathf.Sin(time) * co;
        pos.x = Mathf.Cos(time) * co;

        if (time >= Mathf.PI * 2)
        {
            time = 0;
        }

        this.transform.position = pos;

        return Mathf.Abs(pos.x) < 0.001 && Mathf.Abs(pos.y) < 0.001;
    }
}