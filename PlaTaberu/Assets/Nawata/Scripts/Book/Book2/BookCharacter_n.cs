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
        //キャラクターIDが変化した場合、キャラクターを変更する
        if (character.GetComponent<CharacterManager_n>().ID != GlobalSwitch._CharacterID)
        {
            character.GetComponent<CharacterManager_n>().ID = GlobalSwitch._CharacterID;
            SetUI();
        }
    }

    //キャラクターを切り替える
    public void Change(int deff)
    {
        //引数分キャラクターを変える
        int index = PlayerData._RecodedPlataberu.IndexOf(GlobalSwitch._CharacterID) + deff;

        //はみ出た場合の処理
        int maxIndex = PlayerData._RecodedPlataberu.Count - 1;
        if (index < 0)
            index = maxIndex;
        if (index > maxIndex)
            index = 0;

        GlobalSwitch._CharacterID = PlayerData._RecodedPlataberu[index];
    }

    public void SetUI()
    {
        //各データを表示
        Plataberu beru = PlataberuManager.GetPlataberu(GlobalSwitch._CharacterID);
        explanation.text = beru.Explanation;
        charName.text = beru.Name;
        items.text = $"アイテムスロット：{beru.ItemSlot.Length}個";

        //成長割合を表示
        for(int i = 0; i < growRa.Length; i++)
        {
            int num = (int)(beru.GrowthRatio.ToArray()[i] * beru.Tier / 2);
            string pipelines = "";
            for (int j = 0; j < num; j++)
                pipelines += "|";
            growRa[i].text = pipelines;
        }
        //リボンの設定
        for(int i = 0;i < ribbons.Length; i++)
        {
            ribbons[i].SetActive(i < beru.Tier);
        }
    }
}
