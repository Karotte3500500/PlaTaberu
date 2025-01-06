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

        //パスを設定
        #if UNITY_ANDROID
            appPath = Application.persistentDataPath;
        #else
            appPath = Application.dataPath;
        #endif


        // WebCamTextureのインスタンスを生成
        webCam = new WebCamTexture(2400, 3200, 60);
        // RawImageのテクスチャにWebCamTextureのインスタンスを設定
        rawImage.texture = webCam;
        // カメラ表示開始
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
        //WebCamTextureをTexture2Dに変換
        Texture2D texture = new Texture2D(webCam.width, webCam.height, TextureFormat.ARGB32, false);
        //カメラのピクセルデータを設定
        texture.SetPixels(webCam.GetPixels());
        //テクスチャーを確定
        texture.Apply();

        //エンコード
        byte[] bin = texture.EncodeToPNG();
        //エンコードが終わったら削除
        Object.Destroy(texture);

        imagePath = $"/test{pictureNum}.png";

        //ファイルを保存
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
