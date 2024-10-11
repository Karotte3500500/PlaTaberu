using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabDirector_n3 : MonoBehaviour
{
    private FileControl file;

    [SerializeField]
    private Text eMess;

    private void Start()
    {
        file = FindObjectOfType<FileControl>();
    }
    public void Test()
    {
        file.ReceiveFile($"test",5003);
    }
    private void Update()
    {
        eMess.text = $"êiíªÅF{file.SendProgress}";
    }
}
