using XmlConverting;
using UnityEngine.UI;
using UnityEngine;

public class CollectUIsDirector : MonoBehaviour
{
    [SerializeField]
    private Sprite[] infoImgs;
    [SerializeField]
    private Image infObj;
    [SerializeField]
    private Text timerText;
    private float timer = 3;
    [SerializeField]
    private GameObject timeMess;

    private FileControl file;
    private ControlUI controlUI;
    private int infoIndex = 0;

    private bool end = false;

    void Start()
    {
        file = FindObjectOfType<FileControl>();
        controlUI = FindObjectOfType<ControlUI>();
    }
    private void Update()
    {
        infObj.sprite = infoImgs[infoIndex];
        Debug.Log(infoIndex);

        if(infoIndex == 6 && !end)
        {
            if (file.SendProgress == -1)
                file.ReceiveFile("CollectedPlastics", 8080);
            if (file.SendProgress == 1)
            {
                ServerCommunication.collectedPlastics = ConvertorXML.ReadPlasticsData(Application.persistentDataPath + "/CollectedPlastics");
                Debug.Log($"{ServerCommunication.collectedPlastics.ATK},{ServerCommunication.collectedPlastics.DEF},{ServerCommunication.collectedPlastics.HP}");
                controlUI.SwitchScene("CollectResult");

                file.SendProgress = -1;
                end = true;
            }
        }

        if(infoIndex == 3)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                timer = 0;
            timerText.text = timer == 0 ? "ストップ" : $"{(int)timer}";
        }
        else
        {
            timerText.text = "";
            timeMess.SetActive(false);
        }
    }

    public void SetImg(int num)
    {
        if ((infoIndex > 0 && infoIndex < infoImgs.Length - 1) || (infoIndex <= 0 && num > 0) || (infoIndex >= infoImgs.Length - 1 && num < 0))
            infoIndex += num;
        if (infoIndex == 3)
        {
            timer = 4;
            timeMess.SetActive(true);
        }
    }
}
