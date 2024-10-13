using System.Collections;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Sockets;

public class FileControl : MonoBehaviour
{
    private string url = "http://192.168.11.3/upload.php"; // サーバーのURLを指定
    // サーバー設定
    private string host = "192.168.11.3";  // サーバーのIPアドレス

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
    //        string filePath = Path.Combine(Application.persistentDataPath, $"{fileName}.xml");

    //        if (File.Exists(filePath))
    //        {
    //            File.Delete(filePath);
    //        }

    //        TcpClient client = null;
    //        int retryCount = 3;
    //        while (retryCount > 0)
    //        {
    //            try
    //            {
    //                client = new TcpClient(host, port);
    //                client.ReceiveTimeout = 1800000; // 30分のタイムアウトを設定するのです
    //                break;
    //            }
    //            catch
    //            {
    //                retryCount--;
    //                if (retryCount == 0) throw;
    //            }
    //        }

    //        NetworkStream stream = client.GetStream();

    //        using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
    //        {
    //            byte[] buffer = new byte[4096];
    //            int bytesRead;

    //            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
    //            {
    //                fileStream.Write(buffer, 0, bytesRead);
    //            }
    //        }

    //        Debug.Log("ファイル受信が完了しました: " + filePath);

    //        stream.Close();
    //        client.Close();

    //        SendProgress = 1;
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("エラーが発生しました: " + e.Message);
    //        SendProgress = -2;
    //    }
    //}

    public void ReceiveFile(string fileName, int port)
    {
        SendProgress = 0;
        try
        {
            //ファイルを保存するパス
            string filePath = Path.Combine(Application.persistentDataPath, $"{fileName}.xml");

            //ファイルが存在する場合は削除
            if (CheckIfFileExists(filePath))
            {
                File.Delete(filePath);
            }

            //ソケットのセットアップとリトライ処理
            TcpClient client = null;
            int retryCount = 3;
            while (retryCount > 0)
            {
                try
                {
                    client = new TcpClient(host, port);
                    client.ReceiveTimeout = 1800000; // 3分のタイムアウトを設定
                    break; // 接続成功
                }
                catch
                {
                    retryCount--;
                    if (retryCount == 0) throw;
                }
            }

            NetworkStream stream = client.GetStream();

            //受信したデータをファイルに書き込み
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;

                //データを受信し、バッファに書き込む
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                }
            }

            Debug.Log("ファイル受信が完了しました: " + filePath);

            //ソケットを閉じる
            stream.Close();
            client.Close();

            SendProgress = 1;
        }
        catch (Exception e)
        {
            Debug.LogError("エラーが発生しました: " + e.Message);
            SendProgress = -2;
        }
    }


    //public void ReceiveFile(string fileName, int port)
    //{
    //    SendProgress = 0;
    //    try
    //    {
    //        // ファイルを保存するパス
    //        string filePath = Path.Combine(Application.persistentDataPath, $"{fileName}.xml");

    //        // ファイルが存在する場合はリセット
    //        if (File.Exists(filePath))
    //        {
    //            File.Delete(filePath);
    //        }

    //        // ソケットのセットアップ
    //        TcpClient client = new TcpClient(host, port);
    //        client.ReceiveTimeout = 1800000; // 三分のタイムアウトを設定
    //        NetworkStream stream = client.GetStream();

    //        // 受信したデータをファイルに書き込み
    //        using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
    //        {
    //            byte[] buffer = new byte[1024];
    //            int bytesRead;
    //            bool receiving = true;

    //            // データを受信し、バッファに書き込む
    //            while (receiving)
    //            {
    //                bytesRead = stream.Read(buffer, 0, buffer.Length);
    //                if (bytesRead > 0)
    //                {
    //                    fileStream.Write(buffer, 0, bytesRead);
    //                }
    //                else
    //                {
    //                    // データの終端に達した場合、受信を終了
    //                    receiving = false;
    //                }
    //            }
    //        }

    //        Debug.Log("ファイル受信が完了しました: " + filePath);

    //        // ソケットを閉じる
    //        stream.Close();
    //        client.Close();

    //        SendProgress = 1;
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("エラーが発生しました: " + e.Message);
    //        SendProgress = -2;
    //    }
    //}


    //任意のファイルが存在するかどうかを確認するメソッド
    public bool CheckIfFileExists(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, $"{fileName}.xml");
        return File.Exists(filePath);
    }
}
