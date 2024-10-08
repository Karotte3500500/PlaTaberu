using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(RawImage))]
public class ChromaKeyRawImage : MonoBehaviour
{
    // キーカラー（透過させる色）
    public Color keyColor = Color.green;

    // カメラのソリッドカラー（背景色）
    public Color cameraSolidColor = Color.black;

    // ターゲットカメラ
    public Camera targetCamera;

    // クロマキーのしきい値
    [Range(0, 1)]
    public float threshold = 0.1f;

    // クロマキーのスムーズネス
    [Range(0, 1)]
    public float smoothness = 0.1f;

    // クロマキーを有効にするかどうか
    public bool enableChromaKey = true;

    // 自動でカラーハンドリングを行うかどうか
    public bool autoSyncColors_SolidColorToKeyColor = false;
    public bool autoSyncColors_KeyColorToSolidColor = false;

    // キーカラーをソリッドカラーに同期するかどうか
    public bool syncKeyToSolid = false;

    // ソリッドカラーをキーカラーに同期するかどうか
    public bool syncSolidToKey = false;

    private RawImage rawImage;
    private Material chromaKeyMaterial;

    void Start()
    {
        // RawImageコンポーネントの取得
        rawImage = GetComponent<RawImage>();

        // クロマキー用のマテリアルを作成
        chromaKeyMaterial = new Material(Shader.Find("Custom/ChromaKeyRawImage"));

        // RawImageにマテリアルを設定
        rawImage.material = chromaKeyMaterial;

        // マテリアルの更新
        UpdateMaterial();
    }

    void Update()
    {
        // クロマキーが有効な場合の処理
        if (enableChromaKey)
        {
            UpdateMaterial();
            UpdateCameraColor();
        }
        else
        {
            DisableChromaKey();
        }

        HandleColorSync();
    }

    // クロマキー用マテリアルの更新
    void UpdateMaterial()
    {
        if (chromaKeyMaterial != null)
        {
            chromaKeyMaterial.SetColor("_KeyColor", keyColor);
            chromaKeyMaterial.SetFloat("_Threshold", threshold);
            chromaKeyMaterial.SetFloat("_Smoothness", smoothness);
            rawImage.material = chromaKeyMaterial;
        }
    }

    // クロマキーを無効化
    void DisableChromaKey()
    {
        if (chromaKeyMaterial != null)
        {
            chromaKeyMaterial.SetColor("_KeyColor", Color.clear);
            chromaKeyMaterial.SetFloat("_Threshold", 1);
            chromaKeyMaterial.SetFloat("_Smoothness", 1);
            rawImage.material = null;  // マテリアルを除去
        }
    }

    // カメラの背景色の更新
    void UpdateCameraColor()
    {
        if (targetCamera != null)
        {
            targetCamera.backgroundColor = cameraSolidColor;
        }
    }

    // 色の同期処理
    void HandleColorSync()
    {
        if (autoSyncColors_SolidColorToKeyColor)  // チェックボックスがオンの場合、自動同期を行う
        {
            cameraSolidColor = keyColor;
            UpdateCameraColor();
        }

        if (autoSyncColors_KeyColorToSolidColor)  // チェックボックスがオンの場合、自動同期を行う
        {
            keyColor = cameraSolidColor;
            UpdateMaterial();
        }

        if (syncKeyToSolid)
        {
            keyColor = cameraSolidColor;
            syncKeyToSolid = false;
            UpdateMaterial();
        }

        if (syncSolidToKey)
        {
            cameraSolidColor = keyColor;
            syncSolidToKey = false;
            UpdateCameraColor();
        }
    }

    // キーカラーをソリッドカラーに合わせる
    public void MatchKeyColorToSolidColor()
    {
        keyColor = cameraSolidColor;
        UpdateMaterial();
    }

    // ソリッドカラーをキーカラーに合わせる
    public void MatchSolidColorToKeyColor()
    {
        cameraSolidColor = keyColor;
        UpdateCameraColor();
    }

    // 初期状態にリセットする
    public void ResetToInitialState()
    {
        keyColor = Color.green;
        cameraSolidColor = Color.black;
        threshold = 0.1f;
        smoothness = 0.1f;
        enableChromaKey = true;
        UpdateMaterial();
        UpdateCameraColor();
    }
}
