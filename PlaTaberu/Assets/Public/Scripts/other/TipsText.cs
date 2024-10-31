using UnityEngine;
using UnityEngine.UI;

public class TipsText : MonoBehaviour
{
    private GameObject parent;
    private Text tipsText;

    //tipsの文章
    private string[] tips =
    {
        /*全角9文字以内で改行
        "〇〇〇〇〇〇〇〇〇"←ここまで*/
        "まんちょう の ときの\nなみ の あとには\nごみ が たくさん...",
        "ねっちゅうしょうに\nきをつけよう",
        "まえのひに あめが\nふっているときは\nべつのひにかいしゅうしよう",
        "マイクロプラスチックが\nうみ や かわ に ひろがり\nどうぶつ が たべちゃうよ",
        "そとにひろがった\nマイクロプラスチックは\nすべて とりのぞく のは\nできないんだ",
        "おおきなプラスチックが\nみず や にっこうで\nぶんかい されて\nマイクロプラスチックに\nなることが あるよ",
        "マイクロプラスチックは\nすな の ひょうめんに\nおおく あるよ",
        "マイクロプラスチックは\n5ミリいかの\nプラスチックのこと",
    };

    private void Start()
    {
        parent = this.transform.parent.gameObject;
        tipsText = GetComponent<Text>();

        //tipsをランダムに選択
        tipsText.text = tips[Random.Range(0,tips.Length)];
    }

    private void Update()
    {
        /*フェードイン*/
        Color color = tipsText.color;
        color.a = parent.GetComponent<Image>().color.a;
        tipsText.color = color;
    }
}
