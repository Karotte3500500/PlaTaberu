using GameCharacterManagement;
using XmlConverting;
using UnityEngine.UI;
using UnityEngine;
using System.IO;

public class ToBattleDirector : MonoBehaviour
{
    [SerializeField]
    private Text mess;

    private int count = 0;
    private bool receiveData = false;
    private string path;
    private bool endProcess = false;

    private FileControl fileControl;
    private ControlUI controlUI;

    private void Start()
    {
        fileControl = FindObjectOfType<FileControl>();
        controlUI = FindObjectOfType<ControlUI>();
        ServerCommunication.SetAddress();

        path = Application.persistentDataPath + $"/Plataberu_{ServerCommunication.UserName}";

        Debug.Log(path);
        Debug.Log($"Plataberu_{ServerCommunication.UserName}");

        Debug.Log("�f�[�^�̏�������");
        ConvertorXML.ConvertXML(CharacterData._Plataberu, path);
        Debug.Log("�������݊���");
        Debug.Log("���M�J�n");
        StartCoroutine(fileControl.UploadFileCoroutine($"Plataberu_{ServerCommunication.UserName}"));

    }
    private void Update()
    {
        if (!endProcess)
        {

            Debug.Log(fileControl.SendProgress);
            if (!receiveData && fileControl.SendProgress == 1 && count > 180)
            {
                Debug.Log("��M�J�n");
                mess.text = "�f�[�^�����サ�񂿂イ";
                fileControl.SendProgress = -1;
                fileControl.ReceiveFile($"Plataberu_{ServerCommunication.EnemyName}", 5001);
                if (fileControl.SendProgress == 1)
                    receiveData = true;
            }
            if (receiveData)
            {
                Debug.Log("�f�[�^�ϊ���");
                mess.text = "�f�[�^���ւ񂩂񂿂イ";
                string pathB = Application.persistentDataPath + $"/Plataberu_{ServerCommunication.EnemyName}";
                ServerCommunication._EnemyCharacter = ConvertorXML.ConvertPlataberu(pathB);
                Debug.Log("�ϊ�����");
                mess.text = "�ւ񂩂񂩂��傤";
                Debug.Log(ServerCommunication._EnemyCharacter.DebugString());
                endProcess = true;
                mess.text = "�������ɂ����������܂���";
                controlUI.SwitchScene("Battle");
            }

            if (count == 3600)
                controlUI.SwitchScene("Home");
            count++;
        }
    }
}
