using UnityEngine;
using UnityEngine.UI;

public class TipsText : MonoBehaviour
{
    private GameObject parent;
    private Text tipsText;

    //tips‚Ì•¶Í
    private string[] tips =
    {
        /*‘SŠp9•¶šˆÈ“à‚Å‰üs
        "ZZZZZZZZZ"©‚±‚±‚Ü‚Å*/
        "‚Ü‚ñ‚¿‚å‚¤ ‚Ì ‚Æ‚«‚Ì\n‚È‚İ ‚Ì ‚ ‚Æ‚É‚Í\n‚²‚İ ‚ª ‚½‚­‚³‚ñ...",
        "‚Ë‚Á‚¿‚ã‚¤‚µ‚å‚¤‚É\n‚«‚ğ‚Â‚¯‚æ‚¤.",
        "‚Ü‚¦‚Ì‚Ğ‚É ‚ ‚ß‚ª\n‚Ó‚Á‚Ä‚¢‚é‚Æ‚«‚Í\n‚×‚Â‚Ì‚Ğ‚É‚©‚¢‚µ‚ã‚¤‚µ‚æ‚¤",
    };

    private void Start()
    {
        parent = this.transform.parent.gameObject;
        tipsText = GetComponent<Text>();

        //tips‚ğƒ‰ƒ“ƒ_ƒ€‚É‘I‘ğ
        tipsText.text = tips[Random.Range(0,tips.Length)];
    }

    private void Update()
    {
        /*ƒtƒF[ƒhƒCƒ“*/
        Color color = tipsText.color;
        color.a = parent.GetComponent<Image>().color.a;
        tipsText.color = color;
    }
}
