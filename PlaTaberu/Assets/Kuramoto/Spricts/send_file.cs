using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class send_file : MonoBehaviour
{
    ControlUI controlUI;

    public Button ReturnButton;

    private string filePath; // �A�b�v���[�h�������t�@�C���̃p�X���w��
    private string url = "http://192.168.11.3/upload.php"; // �T�[�o�[��URL���w��

    private test_rec testR;

    void Start()
    {
        testR = FindObjectOfType<test_rec>();

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

                testR.ReceiveFile();
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
}
