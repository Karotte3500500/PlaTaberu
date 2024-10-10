using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabDirector_n3 : MonoBehaviour
{
    private FileControl file;
    private void Start()
    {
        file = FindObjectOfType<FileControl>();
    }
    public void Test()
    {
        file.ReceiveFile($"Plataberu",5003);
    }
}
