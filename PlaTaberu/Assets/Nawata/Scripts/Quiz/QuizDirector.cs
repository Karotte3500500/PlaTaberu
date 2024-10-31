using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using GameCharacterManagement;

public class QuizDirector : MonoBehaviour
{
    private ControlUI controlUI;

    [SerializeField]
    private Text questionText;
    [SerializeField]
    private Text waveText;
    [SerializeField]
    private Text timer;
    [SerializeField]
    private Text[] resultText;

    [SerializeField]
    private Text answer;
    [SerializeField]
    private Image answersBack;

    [SerializeField]
    private GameObject Exp;
    [SerializeField]
    private GameObject playObj;
    [SerializeField]
    private GameObject resultObj;
    [SerializeField]
    private Text[] resultTexts;

    public int wave = 0;
    private int beforWave = -1;
    private bool isPlaying = true;

    private bool hadOpenedResult = false;
    private float time = 0;
    private int[] result = new int[2];

    private int countNum = 0;

    public List<int> choice = new List<int>();

    private void Start()
    {
        playObj.SetActive(true);
        controlUI = FindObjectOfType<ControlUI>();
        Exp.SetActive(false);
        resultObj.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            int randomNum;
            do
            {
                System.Random r = new System.Random();
                randomNum = r.Next(0, questionTexts.Length);
                Debug.Log(randomNum);
            } while (choice.Contains(randomNum));
            choice.Add(randomNum);
            Debug.Log(randomNum);
        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            SetText();

            if (wave != beforWave)
            {
                beforWave = wave;
                questionText.text = questionTexts[choice[wave]].Split('|')[1];
            }

            time += Time.deltaTime;
        }
        else
        {
            if (!hadOpenedResult)
            {
                playObj.SetActive(false);
                resultObj.SetActive(true);

                string resultAdd = "";

                List<Item> items = new List<Item>();
                var random = new System.Random();
                for (int i = 0; i < result[0]; i++)
                {
                    int num = random.Next(1, 5);
                    resultAdd += PlataberuManager.GetItem(num).Name + "\n";
                    PlayerData._Items[num] += 1;
                }

                resultTexts[0].text = $"{result[0]}";
                resultTexts[1].text = $"{result[1]}";
                resultTexts[2].text = $"{time:000.0}";
                resultTexts[3].text = $"{resultAdd}";

                hadOpenedResult = true;
            }
            if (countNum == 300)
                controlUI.SwitchScene("Home");

            countNum++;
        }
    }

    private void SetText()
    {
        waveText.text = $"3/{(wave + 1)}";
        timer.text = $"{time:000.0}";
        resultText[0].text = $"{result[0]}";
        resultText[1].text = $"{result[1]}";
    }

    public void Decide(int num)
    {
        int res = num == (questionTexts[choice[wave]].Split("|")[0][0] - '0') ? 0 : 1;
        result[res] += 1;
        
        if(res == 0)
        {
            answer.text = "せいかい";
            answersBack.color = new Color(0.00f, 0.56f, 1.00f, 1);
        }
        else
        {
            answer.text = "まちがい";
            answersBack.color = new Color(1.00f, 0.00f, 0.64f, 1);
        }

        Exp.SetActive(true);
    }

    public void CloseExp()
    {
        if (!isPlaying) return;
        wave += 1;
        if (wave >= 3)
            isPlaying = false;
        else
            Exp.SetActive(false);
    }

    private string[] questionTexts = new string[]
    {
        "0|マイクロプラスチックは、\n5mmよりちいさい\nプラスチックのこと",
        "1|マイクロプラスチックは、\nすな の ふかいところに\nたまっている",
        "0|そとにひろがった\nマイクロプラスチックは\nすべて かいしゅうできない",
        "1|そうちをつかうときは\nまえのひにあめが\nふってるときがいい",
    };
}
