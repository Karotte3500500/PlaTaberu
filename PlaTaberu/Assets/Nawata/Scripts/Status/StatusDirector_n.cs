using System.Collections;
using System.Collections.Generic;
using GameCharacterManagement;
using UnityEngine.UI;
using UnityEngine;

public class StatusDirector_n : MonoBehaviour
{
    Plataberu myChar = CharacterData._Plataberu;
    ControlUI controlUI;

    [SerializeField]
    private Text charName;
    [SerializeField]
    private Text level;

    [SerializeField]
    private Text atk;
    [SerializeField]
    private Text def;
    [SerializeField]
    private Text hp;
    [SerializeField]
    private Text type;

    [SerializeField]
    private Text skillName;
    [SerializeField]
    private Text skillEx;

    [SerializeField]
    private Text atkCost;
    [SerializeField]
    private Text defCost;
    [SerializeField]
    private Text skillCost;

    [SerializeField]
    private GameObject star;

    private void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();
        for (int i = 0; i < myChar.Tier; i++)
            controlUI.SetUI(star, new Vector2(40 + (i * 60), 1870));
    }

    private void Update()
    {
        charName.text = myChar.Name;
        level.text = myChar.Level.ToString();

        atk.text = $"{(int)myChar.ActualStatus.ATK}";
        def.text = $"{(int)myChar.ActualStatus.DEF}";
        hp.text = $"{(int)myChar.ActualStatus.HP}";
        type.text = myChar.GrowthType;

        skillName.text = myChar.SkillName;
        skillEx.text = myChar.SkillExplanation;

        atkCost.text = myChar.BattleCommand.AttackCost.ToString();
        defCost.text = myChar.BattleCommand.DefenseCost.ToString();
        skillCost.text = myChar.BattleCommand.SkillCost.ToString();
    }
}
