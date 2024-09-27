using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class send_file : MonoBehaviour
{
    ControlUI controlUI;

    public Button ReturnButton;

    private string filePath; // アップロードしたいファイルのパスを指定
    private string url = "http://192.168.11.3/upload.php"; // サーバーのURLを指定

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
        CreateExampleFile(); // ファイル作成メソッドを呼び出し
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
                Debug.Log("ファイルのアップロードに成功しました！");
                Debug.Log(www.downloadHandler.text);

                testR.ReceiveFile();
            }
            else
            {
                Debug.LogError("ファイルのアップロードに失敗しました。");
                Debug.LogError("ステータスコード: " + www.responseCode);
                Debug.LogError("レスポンス: " + www.downloadHandler.text);
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
