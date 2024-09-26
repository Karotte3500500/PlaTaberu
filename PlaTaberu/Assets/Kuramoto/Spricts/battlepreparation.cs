using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class SocketClient : MonoBehaviour
{
    // サーバー設定
    private string host = "192.168.11.3";  // サーバーのIPアドレス
    private int port = 5001;                         // サーバーと同じポート番号

    void Start()
    {
        ReceiveFile();
    }

    private void ReceiveFile()
    {
        try
        {
            // ソケットのセットアップ
            TcpClient client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();

            // ファイルを保存するパス
            string filePath = Path.Combine(Application.persistentDataPath, "received_file.xml");

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

            // ソケットを閉じる
            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("エラーが発生しました: " + e.Message);
        }
    }
}
