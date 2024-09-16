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
        "ŠC—mƒSƒ~–â‘è‚Í‘å•Ï",
        "‚Ä‚·‚Æ‚Ä‚«‚·‚Æ‚Å‚·\n‚Ä‚·‚Æ‚Å‚·",
        "Test3\nƒfƒoƒbƒO‚ß‚ñ‚Ç‚¢",
        "ƒvƒƒRƒ“Œ’“¬Ü",
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
