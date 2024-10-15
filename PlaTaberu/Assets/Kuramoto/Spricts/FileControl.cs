using System.Collections;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Sockets;
using System.Threading.Tasks;

public class FileControl : MonoBehaviour
{
    private string url = "http://192.168.11.3/upload.php"; // サーバーのURLを指定
    // サーバー設定
    private string host = "192.168.11.3";  // サーバーのIPアドレス

    private string appPath;

    public int SendProgress = -1;

    private void Start()
    {
        appPath = Application.persistentDataPath;
    }

    public IEnumerator UploadFileCoroutine(string fileName)
    {
        SendProgress = 0;
        if (!CheckIfFileExists(fileName))
            CreateFile(fileName);
        string path = Path.Combine(appPath, $"{fileName}.xml");

        byte[] fileData = File.ReadAllBytes(path);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, Path.GetFileName(path));

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("ファイルのアップロードに成功しました！");
                Debug.Log(www.downloadHandler.text);
                SendProgress = 1;
            }
            else
            {
                Debug.LogError("ファイルのアップロードに失敗しました。");
                Debug.LogError("ステータスコード: " + www.responseCode);
                Debug.LogError("レスポンス: " + www.downloadHandler.text);
                SendProgress = -2;
            }
        }
    }
    public void CreateFile(string fileName)
    {
        string path = Path.Combine(appPath, $"{fileName}.xml");
        if (!File.Exists(path))
        {
            string xmlContent = "<root>\n\t<example>Sample Data</example>\n</root>";
            File.WriteAllText(path, xmlContent);
            Debug.Log("File created: " + path);
        }
    }

    public void ReceiveFile(string fileName, int port)
    {
        receiveFile(fileName, port);
    }

    private async void receiveFile(string fileName, int port)
    {
        await receive(fileName, port);
    }

    private async Task receive(string fileName, int port)
    {
        await Task.Run(() =>
        {
            SendProgress = 0;
            try
            {
                string filePath = Path.Combine(appPath, $"{fileName}.xml");

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                TcpClient client = null;
                int retryCount = 3;
                while (retryCount > 0)
                {
                    try
                    {
                        client = new TcpClient(host, port);
                        client.ReceiveTimeout = 60000; // 30分のタイムアウト
                        break;
                    }
                    catch
                    {
                        retryCount--;
                        if (retryCount == 0) throw;
                    }
                }

                NetworkStream stream = client.GetStream();

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, bytesRead);
                    }
                }

                Debug.Log("ファイル受信が完了しました: " + filePath);

                stream.Close();
                client.Close();

                SendProgress = 1;
            }
            catch (Exception e)
            {
                Debug.LogError("エラーが発生しました: " + e.Message);
                SendProgress = -2;
            }
        });
    }

    //任意のファイルが存在するかどうかを確認するメソッド
    public bool CheckIfFileExists(string fileName)
    {
        string filePath = Path.Combine(appPath, $"{fileName}.xml");
        return File.Exists(filePath);
    }
}
