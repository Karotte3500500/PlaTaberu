using System.Collections;
using System.Collections.Generic;
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
    private Text characterName;

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
            Plataberu chara = PlataberuManager.GetPlataberu(CharacterID);

            icon.GetComponent<Image>().sprite = iconImg[CharacterID];
            characterName.text = chara.Name;

            string type = chara.GrowthType;
            Color color = type == "ジェネラル" ? new Color(1f, 1f, 1f) : (type == "テクニカル" ? new Color(0.603f, 0.924f, 0.465f) :
                (type == "アタッカー" ? new Color(1f, 0.522f, 0.362f) : new Color(0.447f, 0.973f, 1f)));

            frame.GetComponent<Image>().color = color;
        }
    }

    public void ToBook2()
    {
        GlobalSwitch._CharacterID = CharacterID;
        controlUI.SwitchScene("Book2");
    }
}
