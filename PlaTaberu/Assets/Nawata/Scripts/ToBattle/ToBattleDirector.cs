using GameCharacterManagement;
using XmlConverting;
using UnityEngine;
using System.IO;

public class ToBattleDirector : MonoBehaviour
{
    private int count = 0;
    private bool sendData = false;
    private string path;

    private FileControl fileControl;
    private ControlUI controlUI;

    private void Start()
    {
        path = Application.persistentDataPath + $"/Plataberu_{ServerCommunication.UserName}";

        Debug.Log(Application.persistentDataPath + $"/Plataberu_{ServerCommunication.UserName}");
        Debug.Log($"Plataberu_{ServerCommunication.UserName}");

        fileControl = FindObjectOfType<FileControl>();
        controlUI = FindObjectOfType<ControlUI>();

        ConvertorXML.ConvertXML(CharacterData._Plataberu, path);
        fileControl.UploadFileCoroutine($"Plataberu_{ServerCommunication.UserName}");

    }
    private void Update()
    {
        Debug.Log(fileControl.SendProgress);
        if (fileControl.SendProgress == 1 && !sendData)
        {
            Debug.Log("efcd");
            sendData = true;
            fileControl.SendProgress = -1;
            fileControl.ReceiveFile($"Plataberu_{ServerCommunication.EnemyName}", 5001);
        }
        if (fileControl.SendProgress == 1 && sendData)
        {
            string pathB = Application.persistentDataPath + $"/Plataberu_{ServerCommunication.EnemyName}.xml";
            ServerCommunication._EnemyCharacter = ConvertorXML.ConvertPlataberu(pathB);
            Debug.Log(ServerCommunication._EnemyCharacter.DebugString());
            controlUI.SwitchScene("Battle");
        }

        if (count == 3600)
            controlUI.SwitchScene("Home");
        count++;
    }
}
