using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;


public class TaskTest_n4 : MonoBehaviour
{
    public bool go = false;
    public int num = 0;
    async void Start()
    {
        UnityMainThreadDispatcher.CreateInstance();

        Debug.Log("�񓯊��������J�n����̂ł�");
        await DoSomethingAsync();
        Debug.Log("�񓯊����������������̂ł�");
    }

    private void Update()
    {
        if (go)
        {
            Debug.Log("RFTGHJ");
            Tset();
            Debug.Log("�ʂʂʂʂʂʂ�  ");
            go = false;
        }
        num++;
    }

    public async void Tset()
    {
        Debug.Log("�񓯊��������J�n����̂ł�");
        await DoSomethingAsync();
        Debug.Log("�񓯊����������������̂ł�");
    }

    async Task DoSomethingAsync()
    {
        // �����Ŏ��Ԃ̂����鏈�����V�~�����[�g����̂ł�
        await Task.Delay(2000); // 2�b�҂̂ł�
        Debug.Log("2�b�o�߂����̂ł�");

        // ���C���X���b�h�ɖ߂��Ă���Unity��API���g�p����̂ł�
        await Task.Run(() =>
        {
            // ���C���X���b�h�ɖ߂�����
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                Debug.Log("Unity��API���g�p����̂ł�");
            });
        });

        // �����Ŏ��Ԃ̂����鏈�����V�~�����[�g����̂ł�
        await Task.Delay(2000); // 2�b�҂̂ł�
        Debug.Log("2�b�o�߂����̂ł�");
    }
}
