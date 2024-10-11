using System.Collections;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Sockets;

public class FileControl : MonoBehaviour
{
    private string url = "http://192.168.11.3/upload.php"; // �T�[�o�[��URL���w��
    // �T�[�o�[�ݒ�
    private string host = "192.168.11.3";  // �T�[�o�[��IP�A�h���X

    public int SendProgress = -1;

    public IEnumerator UploadFileCoroutine(string fileName)
    {
        SendProgress = 0;
        if (!CheckIfFileExists(fileName))
            CreateFile(fileName);
        string path = Path.Combine(Application.persistentDataPath, $"{fileName}.xml");

        byte[] fileData = File.ReadAllBytes(path);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, Path.GetFileName(path));

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("�t�@�C���̃A�b�v���[�h�ɐ������܂����I");
                Debug.Log(www.downloadHandler.text);
                SendProgress = 1;
            }
            else
            {
                Debug.LogError("�t�@�C���̃A�b�v���[�h�Ɏ��s���܂����B");
                Debug.LogError("�X�e�[�^�X�R�[�h: " + www.responseCode);
                Debug.LogError("���X�|���X: " + www.downloadHandler.text);
                SendProgress = -2;
            }
        }
    }
    public void CreateFile(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, $"{fileName}.xml");
        if (!File.Exists(path))
        {
            string xmlContent = "<root>\n\t<example>Sample Data</example>\n</root>";
            File.WriteAllText(path, xmlContent);
            Debug.Log("File created: " + path);
        }
    }

    //public void ReceiveFile(string fileName, int port)
    //{
    //    SendProgress = 0;
    //    try
    //    {
    //        // �\�P�b�g�̃Z�b�g�A�b�v
    //        TcpClient client = new TcpClient(host, port);
    //        NetworkStream stream = client.GetStream();

    //        // �t�@�C����ۑ�����p�X
    //        string filePath = Path.Combine(Application.persistentDataPath, $"{fileName}.xml");

    //        if (!File.Exists(filePath))
    //        {
    //            File.Create(filePath);
    //        }

    //        // ��M�����f�[�^���t�@�C���ɏ�������
    //        using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
    //        {
    //            byte[] buffer = new byte[1024];
    //            int bytesRead;

    //            // �f�[�^����M���A�o�b�t�@�ɏ�������
    //            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
    //            {
    //                fileStream.Write(buffer, 0, bytesRead);
    //            }
    //        }

    //        Debug.Log("�t�@�C����M���������܂���: " + filePath);

    //        // �\�P�b�g�����
    //        stream.Close();
    //        client.Close();

    //        SendProgress = 1;
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("�G���[���������܂���: " + e.Message);
    //        SendProgress = -2;
    //    }
    //}

    public void ReceiveFile(string fileName, int port)
    {
        SendProgress = 0;
        try
        {
            // �\�P�b�g�̃Z�b�g�A�b�v
            TcpClient client = new TcpClient(host, port);
            client.ReceiveTimeout = 10000; // 10�b�̃^�C���A�E�g��ݒ�
            NetworkStream stream = client.GetStream();

            // �t�@�C����ۑ�����p�X
            string filePath = Path.Combine(Application.persistentDataPath, $"{fileName}.xml");

            // �t�@�C�������݂��Ȃ��ꍇ�͍쐬
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            // ��M�����f�[�^���t�@�C���ɏ�������
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;
                bool receiving = true;

                // �f�[�^����M���A�o�b�t�@�ɏ�������
                while (receiving)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                    else
                    {
                        // �f�[�^�̏I�[�ɒB�����ꍇ�A��M���I��
                        receiving = false;
                    }
                }
            }

            Debug.Log("�t�@�C����M���������܂���: " + filePath);

            // �\�P�b�g�����
            stream.Close();
            client.Close();

            SendProgress = 1;
        }
        catch (Exception e)
        {
            Debug.LogError("�G���[���������܂���: " + e.Message);
            SendProgress = -2;
        }
    }


    // �C�ӂ̃t�@�C�������݂��邩�ǂ������m�F���郁�\�b�h
    public bool CheckIfFileExists(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, $"{fileName}.xml");
        return File.Exists(filePath);
    }
}
