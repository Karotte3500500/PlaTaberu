using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public RawImage rawImage;

    [SerializeField]
    private ControlUI controlUI;

    [SerializeField]
    private Text pathStr;

    private WebCamTexture webCam;

    private string imagePath;
    private string appPath;
    private int pictureNum = 0;

    void Start()
    {

        //�p�X��ݒ�
        #if UNITY_ANDROID
            appPath = Application.persistentDataPath;
        #else
            appPath = Application.dataPath;
        #endif


        // WebCamTexture�̃C���X�^���X�𐶐�
        webCam = new WebCamTexture(2400, 3200, 60);
        // RawImage�̃e�N�X�`����WebCamTexture�̃C���X�^���X��ݒ�
        rawImage.texture = webCam;
        // �J�����\���J�n
        webCam.Play();
    }

    public void Stop()
    {
        if (webCam.isPlaying)
        {
            webCam.Pause();
            Take();
            webCam.Play();
        }
    }

    public void Take()
    {
        //WebCamTexture��Texture2D�ɕϊ�
        Texture2D texture = new Texture2D(webCam.width, webCam.height, TextureFormat.ARGB32, false);
        //�J�����̃s�N�Z���f�[�^��ݒ�
        texture.SetPixels(webCam.GetPixels());
        //�e�N�X�`���[���m��
        texture.Apply();

        //�G���R�[�h
        byte[] bin = texture.EncodeToPNG();
        //�G���R�[�h���I�������폜
        Object.Destroy(texture);

        imagePath = $"/test{pictureNum}.png";

        //�t�@�C����ۑ�
        File.WriteAllBytes(appPath + imagePath, bin);
        Debug.Log(appPath + imagePath);
        pathStr.text = appPath + imagePath;

        pictureNum++;
    }

    public void Close(string sceneName)
    {
        webCam.Stop();
        webCam = null;
        controlUI.SwitchScene(sceneName);
    }
}
