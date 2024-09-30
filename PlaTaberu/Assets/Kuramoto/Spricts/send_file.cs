using System.Collections;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net.Sockets;

public class send_file : MonoBehaviour
{
    ControlUI controlUI;

    public Button ReturnButton;

    /*�f�o�b�O�p   *���ۂ͍폜  */
    [SerializeField]
    private Text errorMess;

    private string filePath; // �A�b�v���[�h�������t�@�C���̃p�X���w��
    private string url = "http://192.168.11.3/upload.php"; // �T�[�o�[��URL���w��
    // �T�[�o�[�ݒ�
    private string host = "192.168.11.3";  // �T�[�o�[��IP�A�h���X
    private int port = 5001;                         // �T�[�o�[�Ɠ����|�[�g�ԍ�

    void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();
        filePath = Application.persistentDataPath + @"/texample.xml";

        ReturnButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        CreateExampleFile(); // �t�@�C���쐬���\�b�h���Ăяo��
        StartCoroutine(UploadFileCoroutine(filePath, url));
    }

    IEnumerator UploadFileCoroutine(string filePath, string url)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, Path.GetFileName(filePath));

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("�t�@�C���̃A�b�v���[�h�ɐ������܂����I");
                Debug.Log(www.downloadHandler.text);

                ReceiveFile();
            }
            else
            {
                Debug.LogError("�t�@�C���̃A�b�v���[�h�Ɏ��s���܂����B");
                Debug.LogError("�X�e�[�^�X�R�[�h: " + www.responseCode);
                Debug.LogError("���X�|���X: " + www.downloadHandler.text);
            }
        }
    }
    void CreateExampleFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "texample.xml");
        if (!File.Exists(path))
        {
            string xmlContent = "<root>\n\t<example>Sample Data</example>\n</root>";
            File.WriteAllText(path, xmlContent);
            Debug.Log("File created: " + path);
        }
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
                using (FileStream fs = File.Create(filePath));

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
