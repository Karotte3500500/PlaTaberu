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
    private GameObject typeBack;

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
            controlUI.SetUI(star, new Vector2(60 + (i * 100), 1870));
    }

    private void Update()
    {
        charName.text = myChar.Name;
        level.text = myChar.Level.ToString();

        atk.text = $"{(int)myChar.ActualStatus.ATK}";
        def.text = $"{(int)myChar.ActualStatus.DEF}";
        hp.text = $"{(int)myChar.ActualStatus.HP}";
        type.text = myChar.GrowthType;
        Color color;
        switch (type.text)
        {
            case "ジェネラル":
                color = new Color(0.28f, 0.28f, 0.28f, 0.85f);
                break;
            case "テクニカル":
                color = new Color(0.34f, 0.88f, 0.09f, 0.85f);
                break;
            case "アタッカー":
                color = new Color(1.00f, 0.35f, 0.15f, 0.85f);
                break;
            case "ディフェンサー":
                color = new Color(0.07f, 0.69f, 0.80f, 0.85f);
                break;
            default:
                color = new Color(0.28f, 0.28f, 0.28f, 0.85f);
                break;
        }
        typeBack.GetComponent<Image>().color = color;

        skillName.text = myChar.SkillName;
        skillEx.text = myChar.SkillExplanation;

        atkCost.text = myChar.BattleCommand.AttackCost.ToString();
        defCost.text = myChar.BattleCommand.DefenseCost.ToString();
        skillCost.text = myChar.BattleCommand.SkillCost.ToString();
    }
}
