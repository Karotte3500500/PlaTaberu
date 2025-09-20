using GameCharacterManagement;
using UnityEngine.UI;
using UnityEngine;

public class HomeDirector_n : MonoBehaviour
{
    [SerializeField]
    private Text charName;
    [SerializeField]
    private Text charLevel;
    [SerializeField]
    private Text charType;
    [SerializeField]
    private GameObject typeBack;

    [SerializeField]
    private GameObject menue;


    [SerializeField]
    private Text[] plastics;

    private Plataberu myChar = CharacterData._Plataberu;

    private void Start()
    {
        menue.SetActive(false);
    }

    private void Update()
    {
        /*情報を表示*/
        charName.text = myChar.Name;
        charLevel.text = $"{myChar.Level}";
        charType.text = myChar.GrowthType;

        plastics[0].text = $"{CharacterData._RedPlastic}";
        plastics[1].text = $"{CharacterData._GreenPlastic}";
        plastics[2].text = $"{CharacterData._BluePlastic}";

        Color color;
        switch (charType.text)
        {
            case "ジェネラル":
                color = new Color(0.28f, 0.28f, 0.28f, 1.00f);
                break;
            case "テクニカル":
                color = new Color(0.34f, 0.88f, 0.09f, 1.00f);
                break;
            case "アタッカー":
                color = new Color(1.00f, 0.35f, 0.15f, 1.00f);
                break;
            case "ディフェンサー":
                color = new Color(0.07f, 0.69f, 0.80f, 1.00f);
                break;
            default:
                color = new Color(0.28f, 0.28f, 0.28f, 1.00f);
                break;
        }
        typeBack.GetComponent<Image>().color = color;
    }

    public void OpenMenu()
    {
        menue.SetActive(true);
    }
    public void Quit()
    {
        //PlayerData.SavePlayerData();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ContinueApp()
    {
        menue.SetActive(false);
    }

    public void help()
    {

    }
}
