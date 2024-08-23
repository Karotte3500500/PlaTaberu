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
            // �T�[�o�[��IP�A�h���X�ƃ|�[�g�ԍ���ݒ�
            string serverIpAddress = "192.168.43.187"; // �T�[�o�[��IP�A�h���X(�K��)
            int serverPort = 8000; // �T�[�o�[�̃|�[�g�ԍ��i�K���j

            // CSV�t�@�C���ɏ�������IP�A�h���X���擾
            string localIpAddress = GetLocalIpAddress();

            // CSV�t�@�C���ɏ�������
            using (StreamWriter writer = new StreamWriter("ip_addresses.csv", true))
            {
                writer.WriteLine(localIpAddress);
            }

            Console.WriteLine($"IP�A�h���X {localIpAddress} ��CSV�t�@�C���ɏ������݂܂����B");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"�G���[���������܂���: {ex.Message}");
        }
    }

    // ���[�J��IP�A�h���X���擾���郁�\�b�h
    static string GetLocalIpAddress()
    {
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530); // Google DNS�T�[�o�[�ɐڑ�
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            return endPoint?.Address.ToString() ?? "IP�A�h���X���擾�ł��܂���ł���";
        }
    }
}
