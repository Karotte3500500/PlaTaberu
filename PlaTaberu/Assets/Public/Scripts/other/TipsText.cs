using UnityEngine;
using UnityEngine.UI;

public class TipsText : MonoBehaviour
{
    private GameObject parent;
    private Text tipsText;

    //tips�̕���
    private string[] tips =
    {
        /*�S�p9�����ȓ��ŉ��s
        "�Z�Z�Z�Z�Z�Z�Z�Z�Z"�������܂�*/
        "�C�m�S�~���͑��",
        "�Ă��ƂĂ����Ƃł�\n�Ă��Ƃł�",
        "Test3\n�f�o�b�O�߂�ǂ�",
        "�v���R��������",
    };

    private void Start()
    {
        parent = this.transform.parent.gameObject;
        tipsText = GetComponent<Text>();

        //tips�������_���ɑI��
        tipsText.text = tips[Random.Range(0,tips.Length)];
    }

    private void Update()
    {
        /*�t�F�[�h�C��*/
        Color color = tipsText.color;
        color.a = parent.GetComponent<Image>().color.a;
        tipsText.color = color;
    }
}
