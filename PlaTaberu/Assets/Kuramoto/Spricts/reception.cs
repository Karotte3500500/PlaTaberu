using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class reception : MonoBehaviour
{
    ControlUI controlUI;

    public Button ReturnButton;

    private void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();

        ReturnButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        StartCoroutine(GetRequest("http://192.168.11.3:5000/get-xml", "downloadedData.xml"));
    }

    IEnumerator GetRequest(string uri, string fileName)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // リクエストを送信し、レスポンスが返ってくるまで待機
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // レスポンスデータを取得してファイルに保存
                string data = webRequest.downloadHandler.text;
                string path = Path.Combine(Application.persistentDataPath, fileName);
                File.WriteAllText(path, data);
                Debug.Log("File saved to: " + path);
            }
        }
    }
}
