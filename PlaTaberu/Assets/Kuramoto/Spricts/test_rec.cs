using System.Collections;
using System.IO;
using System;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class test_rec : MonoBehaviour
{
    ControlUI controlUI;

    public Button ReturnButton;

    /*デバッグ用   *実際は削除  */
    [SerializeField]
    private Text errorMess;

    // サーバー設定
    private string host = "192.168.11.3";  // サーバーのIPアドレス
    private int port = 5001;                         // サーバーと同じポート番号

    private void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();

        ReturnButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        ReceiveFile();
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
                using (FileStream fs = File.Create(filePath)) ;

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
