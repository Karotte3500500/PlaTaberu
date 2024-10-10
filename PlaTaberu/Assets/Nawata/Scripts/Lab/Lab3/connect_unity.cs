using UnityEngine;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class FileReceiver : MonoBehaviour
{
    public int port = 5003;

    void Start()
    {
        Thread receiverThread = new Thread(StartServer);
        receiverThread.IsBackground = true;
        receiverThread.Start();
    }

    void StartServer()
    {
        TcpListener server = new TcpListener(IPAddress.Any, port);
        server.Start();
        Debug.Log($"サーバーがポート{port}で待機しています...");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            Debug.Log("接続しました...");

            using (FileStream fs = new FileStream("received_file.xml", FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fs.Write(buffer, 0, bytesRead);
                }
            }

            stream.Close();
            client.Close();
            Debug.Log("ファイルの受信が完了しました。");
        }
    }
}
