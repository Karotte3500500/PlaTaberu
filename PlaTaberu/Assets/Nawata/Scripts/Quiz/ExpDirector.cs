using UnityEngine;
using UnityEngine.UI;

public class ExpDirector : MonoBehaviour
{
    [SerializeField]
    private Text expMess;

    private QuizDirector quizDirector;

    private string[] ExplanationTexts = new string[]
    {
        "�}�C�N���v���X�`�b�N�Ƃ�\n5mm��肿������\n�v���X�`�b�N�̂��Ƃ���\n���񂱂����� �� \n�ӂ��̂��� �Ȃǂ���\n�͂����� ���Ă����",
        "�}�C�N���v���X�`�b�N��\n�Ȃ� �� �Ȃ������\n���Ȃ͂� �� �Ђ傤�߂��\n���܂��",
        "�}�C�N���v���X�`�b�N��\n�ƂĂ� ��������\n�������񂠂邩��\n���ׂ� �Ȃ������Ƃ�\n�ł��Ȃ���",
        "���߂��ӂ��Ă����\n���Ȃ͂� �� �ʂ�����\n���ԂȂ����� �Ђ����悤",
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
