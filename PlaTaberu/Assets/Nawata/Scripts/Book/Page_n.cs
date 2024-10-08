using GameCharacterManagement;
using UnityEngine.UI;
using UnityEngine;

public class Page_n : MonoBehaviour
{
    [SerializeField]
    private GameObject icon;
    [SerializeField]
    private GameObject frame;
    [SerializeField]
    private Image back;
    [SerializeField]
    private Text characterName;

    [SerializeField]
    private Sprite[] backs;

    [SerializeField]
    private Sprite[] iconImg;

    public int CharacterID = -1;

    private ControlUI controlUI;

    private void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();
    }

    private void Update()
    {
        if (CharacterID != -1)
        {
            //IDからプラタベルを参照
            Plataberu chara = PlataberuManager.GetPlataberu(CharacterID);

            icon.GetComponent<Image>().sprite = iconImg[CharacterID];
            characterName.text = chara.Name;

            string type = chara.GrowthType;
            Color color;
            switch (type)
            {
                case "ジェネラル":
                    color = new Color(0.38f, 0.38f, 0.38f, 1.00f);
                    break;
                case "テクニカル":
                    color = new Color(0.35f, 0.50f, 0.00f, 1.00f);
                    break;
                case "アタッカー":
                    color = new Color(0.60f, 0.30f, 0.00f, 1.00f);
                    break;
                case "ディフェンサー":
                    color = new Color(0.00f, 0.49f, 0.60f, 1.00f);
                    break;
                default:
                    color = new Color(0.38f, 0.38f, 0.38f, 1.00f);
                    break;
            }
            frame.GetComponent<Image>().color = color;

            back.sprite = backs[chara.Tier - 1];
        }
    }

    public void ToBook2()
    {
        GlobalSwitch._CharacterID = CharacterID;
        controlUI.SwitchScene("Book2");
    }
}
