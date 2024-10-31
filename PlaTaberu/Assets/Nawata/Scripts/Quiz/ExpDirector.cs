using UnityEngine;
using UnityEngine.UI;

public class ExpDirector : MonoBehaviour
{
    [SerializeField]
    private Text expMess;

    private QuizDirector quizDirector;

    private string[] ExplanationTexts = new string[]
    {
        "マイクロプラスチックとは\n5mmよりちいさい\nプラスチックのことだよ\nじんこうしば や \nふくのせんい などから\nはっせい しているんだ",
        "マイクロプラスチックは\nなみ に ながされて\nすなはま の ひょうめんに\nあつまるんだ",
        "マイクロプラスチックは\nとても ちいさく\nたくさんあるから\nすべて なくすことは\nできないんだ",
        "あめがふっていると\nすなはま が ぬかるんで\nあぶないから ひかえよう",
    };

    private void Start()
    {
        quizDirector = FindObjectOfType<QuizDirector>();
    }
    private void Update()
    {
        if (quizDirector.wave < 3)
            expMess.text = ExplanationTexts[quizDirector.choice[quizDirector.wave]];
    }
}
