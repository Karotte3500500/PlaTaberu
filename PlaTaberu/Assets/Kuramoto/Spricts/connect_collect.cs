using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

public class connect_collect : MonoBehaviour
{
    public Button ConnectButton;

    // Start is called before the first frame update
    void Start()
    {
        ReturnButton.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    static void OnClick()
    {
        try
        {
            // サーバーのIPアドレスとポート番号を設定
            string serverIpAddress = "192.168.43.187"; // サーバーのIPアドレス(適当)
            int serverPort = 8000; // サーバーのポート番号（適当）

            // CSVファイルに書き込むIPアドレスを取得
            string localIpAddress = GetLocalIpAddress();

            // CSVファイルに書き込む
            using (StreamWriter writer = new StreamWriter("ip_addresses.csv", true))
            {
                writer.WriteLine(localIpAddress);
            }

            Console.WriteLine($"IPアドレス {localIpAddress} をCSVファイルに書き込みました。");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"エラーが発生しました: {ex.Message}");
        }
    }

    // ローカルIPアドレスを取得するメソッド
    static string GetLocalIpAddress()
    {
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530); // Google DNSサーバーに接続
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            return endPoint?.Address.ToString() ?? "IPアドレスが取得できませんでした";
        }
    }
}
