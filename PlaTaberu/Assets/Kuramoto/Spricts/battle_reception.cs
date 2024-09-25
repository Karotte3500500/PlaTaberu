using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class battle_reception : MonoBehaviour
{
    void Start()
    {
        CreateExampleFile(); // ファイル作成メソッドを呼び出し
        access();
    }

    public void access()
    {
        StartCoroutine(PostRequest("http://192.168.11.3:5000/upload-xml", "texample.xml"));
    }

    IEnumerator PostRequest(string uri, string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(path))
        {
            Debug.LogError("File not found: " + path);
            yield break;
        }

        byte[] fileData = File.ReadAllBytes(path);
        UnityWebRequest webRequest = new UnityWebRequest(uri, UnityWebRequest.kHttpVerbPOST);
        webRequest.uploadHandler = new UploadHandlerRaw(fileData);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/xml");

        // リクエストを送信し、レスポンスが返ってくるまで待機
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            Debug.Log("File uploaded successfully");
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
