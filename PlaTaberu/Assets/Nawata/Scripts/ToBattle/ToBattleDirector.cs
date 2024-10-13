using GameCharacterManagement;
using XmlConverting;
using UnityEngine;
using System.IO;

public class ToBattleDirector : MonoBehaviour
{
    private int count = 0;
    private bool receiveData = false;
    private string path;

    private FileControl fileControl;
    private ControlUI controlUI;

    private void Start()
    {
        fileControl = FindObjectOfType<FileControl>();
        controlUI = FindObjectOfType<ControlUI>();
        ServerCommunication.SetAddress();

        path = Application.persistentDataPath + $"/Plataberu_{ServerCommunication.UserName}";

        Debug.Log(Application.persistentDataPath + $"/Plataberu_{ServerCommunication.UserName}");
        Debug.Log($"Plataberu_{ServerCommunication.UserName}");

        Debug.Log("�f�[�^�̏�������");
        ConvertorXML.ConvertXML(CharacterData._Plataberu, path);
        Debug.Log("�������݊���");
        Debug.Log("���M�J�n");
        StartCoroutine(fileControl.UploadFileCoroutine($"Plataberu_{ServerCommunication.UserName}"));

    }
    private void Update()
    {
        Debug.Log(fileControl.SendProgress);
        if (!receiveData && fileControl.SendProgress == 1 && count > 180)
        {
            Debug.Log("��M�J�n");
            fileControl.SendProgress = -1;
            fileControl.ReceiveFile($"Plataberu_{ServerCommunication.EnemyName}", 5001);
            if (fileControl.SendProgress == 1)
                receiveData = true;
        }
        if (receiveData)
        {
            Debug.Log("�f�[�^�ϊ���");
            string pathB = Application.persistentDataPath + $"/Plataberu_{ServerCommunication.EnemyName}";
            ServerCommunication._EnemyCharacter = ConvertorXML.ConvertPlataberu(pathB);
            Debug.Log("�ϊ�����");
            Debug.Log(ServerCommunication._EnemyCharacter.DebugString());
            controlUI.SwitchScene("Battle");
        }

        if (count == 3600)
            controlUI.SwitchScene("Home");
        count++;
    }
}
