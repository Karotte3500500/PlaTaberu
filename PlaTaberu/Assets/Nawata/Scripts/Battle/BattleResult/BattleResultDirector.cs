using UnityEngine.UI;
using UnityEngine;

public class BattleResultDirector : MonoBehaviour
{
    [SerializeField]
    private Text resultMess;
    [SerializeField]
    private Text messJp;
    [SerializeField]
    private SpriteRenderer[] backImgs;

    string[] mess = new string[] { "VICTORY", "DEFEAT" };
    string[] jp = new string[] { "‚µ‚å‚¤‚è", "‚Í‚¢‚Ú‚­" };
    Color[] colors = new Color[] { new Color(1.00f, 0.70f, 0.29f, 1.00f), new Color(0.29f, 0.61f, 1.00f, 1.00f) };

    private void Start()
    {
        resultMess.text = mess[GlobalValue._Victory];
        messJp.text = jp[GlobalValue._Victory];
        foreach (var backImg in backImgs)
            backImg.color = colors[GlobalValue._Victory];
    }
}
