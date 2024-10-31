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
        "�܂񂿂傤 �� �Ƃ���\n�Ȃ� �� ���Ƃɂ�\n���� �� ��������...",
        "�˂����イ���傤��\n�������悤",
        "�܂��̂Ђ� ���߂�\n�ӂ��Ă���Ƃ���\n�ׂ̂Ђɂ������イ���悤",
        "�}�C�N���v���X�`�b�N��\n���� �� ���� �� �Ђ낪��\n�ǂ��Ԃ� �� ���ׂ��Ⴄ��",
        "���ƂɂЂ낪����\n�}�C�N���v���X�`�b�N��\n���ׂ� �Ƃ�̂��� �̂�\n�ł��Ȃ���",
        "�������ȃv���X�`�b�N��\n�݂� �� �ɂ�������\n�Ԃ񂩂� �����\n�}�C�N���v���X�`�b�N��\n�Ȃ邱�Ƃ� �����",
        "�}�C�N���v���X�`�b�N��\n���� �� �Ђ傤�߂��\n������ �����",
        "�}�C�N���v���X�`�b�N��\n5�~��������\n�v���X�`�b�N�̂���",
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
