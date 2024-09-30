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

    /*デバッグ用   *実際は削除  */
    [SerializeField]
    private Text errorMess;

    private string filePath; // アップロードしたいファイルのパスを指定
    private string url = "http://192.168.11.3/upload.php"; // サーバーのURLを指定
    // サーバー設定
    private string host = "192.168.11.3";  // サーバーのIPアドレス
    private int port = 5001;                         // サーバーと同じポート番号

    void Start()
    {
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

                ReceiveFile();
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

    public void ReceiveFile()
    {
        try
        {
            // ソケットのセットアップ
            TcpClient client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();

            // ファイルを保存するパス
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

            // 受信したデータをファイルに書き込み
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                // データを受信し、バッファに書き込む
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                }
            }

            Debug.Log("ファイル受信が完了しました: " + filePath);
            //デバッグ後削除
            errorMess.text = "ファイル受信が完了しました: " + filePath;

            // ソケットを閉じる
            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("エラーが発生しました: " + e.Message);
            //デバッグ後削除
            errorMess.text = "エラーが発生しました: " + e.Message;
        }
    }
}
