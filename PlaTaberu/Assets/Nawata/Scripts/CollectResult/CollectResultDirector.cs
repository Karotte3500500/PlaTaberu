using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectResultDirector : MonoBehaviour
{
    [SerializeField]
    private GameObject star;
    [SerializeField]
    private Text[] addPlas;
    [SerializeField]
    private float[] addPlasNum = new float[3];

    private List<int> plastics = new List<int>();
    public int fallingInterval = 0;
    public int count = 0;
    public int plasticsIndex = 0;
    private Color[] colors = new Color[] { new Color(1.00f, 0.02f, 0.62f, 1.00f), new Color(0.00f, 0.83f, 1.00f, 1.00f), new Color(0.00f, 1.00f, 0.06f, 1.00f) };

    private ControlUI controlUI;

    private void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();

        int num = 0;
        foreach (var plas in ServerCommunication.collectedPlastics.ToArray())
        {
            for (int i = 0; i < plas; i++)
                plastics.Add(num);
            num++;
        }
        plastics = shuffleList(plastics);


        //string mess = "";
        //foreach (var pla in plastics)
        //    mess += $"{pla},";
        //Debug.Log(mess);

        if (plastics.Count == 0)
            plasticsIndex = -1;
        else
            fallingInterval = 700 / plastics.Count;
    }

    private void Update()
    {
        if (plasticsIndex != -1 && count % fallingInterval == 0)
        {
            float point = UnityEngine.Random.Range(0.0f, 4.5f) - 2.2f;
            float size = UnityEngine.Random.Range(0.09f, 0.2f);
            GameObject st = Instantiate(star);
            st.transform.position = new Vector2(point, 6.0f);
            st.transform.localScale = new Vector3(size, size, 1.0f);
            st.GetComponent<SpriteRenderer>().color = colors[plastics[plasticsIndex]];
            
            addPlasNum[plastics[plasticsIndex]] += 100f;
            for (int i = 0; i < 3; i++)
                addPlas[i].text = $"x {addPlasNum[i]}";

            plasticsIndex += 1;
            if(plasticsIndex >= plastics.Count)
            {
                plasticsIndex = -1;
                count = 0;
            }
        }

        if (count == 300 && plasticsIndex == -1)
        {
            controlUI.SwitchScene("Home");
        }

        count++;
    }

    private List<int> shuffleList(List<int> list)
    {
        int max = list.Count;
        List<int> randomNums = new List<int>();
        List<int> shuffledList = new List<int>();
        foreach (var item in list)
        {
            System.Random r = new System.Random();
            int randomNum;
            do
            {
                randomNum = r.Next(0, max);
            } while (randomNums.Contains(randomNum));
            randomNums.Add(randomNum);
            shuffledList.Add(list[randomNum]);
        }

        return shuffledList;
    }
}
