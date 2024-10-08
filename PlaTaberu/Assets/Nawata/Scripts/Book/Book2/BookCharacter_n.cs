using GameCharacterManagement;
using UnityEngine;
using UnityEngine.UI;

public class BookCharacter_n : MonoBehaviour
{
    [SerializeField]
    private GameObject character;

    [SerializeField]
    private Text explanation;
    [SerializeField]
    private Text charName;
    [SerializeField]
    private Text[] growRa;
    [SerializeField]
    private Text items;

    [SerializeField]
    private GameObject[] ribbons;


    void Start()
    {
        SetUI();
    }

    void Update()
    {
        //�L�����N�^�[ID���ω������ꍇ�A�L�����N�^�[��ύX����
        if (character.GetComponent<CharacterManager_n>().ID != GlobalSwitch._CharacterID)
        {
            character.GetComponent<CharacterManager_n>().ID = GlobalSwitch._CharacterID;
            SetUI();
        }
    }

    //�L�����N�^�[��؂�ւ���
    public void Change(int deff)
    {
        //�������L�����N�^�[��ς���
        int index = PlayerData._RecodedPlataberu.IndexOf(GlobalSwitch._CharacterID) + deff;

        //�͂ݏo���ꍇ�̏���
        int maxIndex = PlayerData._RecodedPlataberu.Count - 1;
        if (index < 0)
            index = maxIndex;
        if (index > maxIndex)
            index = 0;

        GlobalSwitch._CharacterID = PlayerData._RecodedPlataberu[index];
    }

    public void SetUI()
    {
        //�e�f�[�^��\��
        Plataberu beru = PlataberuManager.GetPlataberu(GlobalSwitch._CharacterID);
        explanation.text = beru.Explanation;
        charName.text = beru.Name;
        items.text = $"�A�C�e���X���b�g�F{beru.ItemSlot.Length}��";

        //����������\��
        for(int i = 0; i < growRa.Length; i++)
        {
            int num = (int)(beru.GrowthRatio.ToArray()[i] * beru.Tier / 2);
            string pipelines = "";
            for (int j = 0; j < num; j++)
                pipelines += "|";
            growRa[i].text = pipelines;
        }
        //���{���̐ݒ�
        for(int i = 0;i < ribbons.Length; i++)
        {
            ribbons[i].SetActive(i < beru.Tier);
        }
    }
}
