using System.Collections;
using System.IO;
using System;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class test_rec : MonoBehaviour
{
    ControlUI controlUI;

    public Button ReturnButton;

    /*�f�o�b�O�p   *���ۂ͍폜  */
    [SerializeField]
    private Text errorMess;

    // �T�[�o�[�ݒ�
    private string host = "192.168.11.3";  // �T�[�o�[��IP�A�h���X
    private int port = 5001;                         // �T�[�o�[�Ɠ����|�[�g�ԍ�

    private void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();

        ReturnButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        ReceiveFile();
    }

    public void ReceiveFile()
    {
        try
        {
            // �\�P�b�g�̃Z�b�g�A�b�v
            TcpClient client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();

            // �t�@�C����ۑ�����p�X
            string filePath = Path.Combine(Application.persistentDataPath, "received_file.xml");

            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath)) ;

                /*
                string xmlContent = "<root>\n\t<example>Sample Data</example>\n</root>";
                File.WriteAllText(filePath, xmlContent);
                Debug.Log("File created: " + filePath);
                */
            }

                // ��M�����f�[�^���t�@�C���ɏ�������
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                // �f�[�^����M���A�o�b�t�@�ɏ�������
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                }
            }

            Debug.Log("�t�@�C����M���������܂���: " + filePath);
            //�f�o�b�O��폜
            errorMess.text = "�t�@�C����M���������܂���: " + filePath;

            // �\�P�b�g�����
            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("�G���[���������܂���: " + e.Message);
            //�f�o�b�O��폜
            errorMess.text = "�G���[���������܂���: " + e.Message;
        }
    }
}
