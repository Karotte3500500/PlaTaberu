using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ChromaKeyRawImage))]
public class ChromaKeyRawImageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChromaKeyRawImage script = (ChromaKeyRawImage)target;

        EditorGUILayout.LabelField("【対象のカメラ】");
        script.targetCamera = (Camera)EditorGUILayout.ObjectField("Target Camera", script.targetCamera, typeof(Camera), true);
        EditorGUILayout.Space(10);

        // クロマキーの有効化
        script.enableChromaKey = EditorGUILayout.Toggle("Enable Chroma Key", script.enableChromaKey);

        EditorGUILayout.Space(10);
        //Reset to Initial State
        if (GUILayout.Button("初期化")) 
        {
            script.ResetToInitialState();
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("【カメラ設定】");
        EditorGUILayout.Space(5);
        
        //Camera Solid Color
        script.cameraSolidColor = EditorGUILayout.ColorField("カメラの背景色", script.cameraSolidColor);

        EditorGUILayout.Space(10);

        script.autoSyncColors_KeyColorToSolidColor = EditorGUILayout.Toggle("Auto Sync Colors / 使う場合は透過色側のチェックを外す", script.autoSyncColors_KeyColorToSolidColor);

        EditorGUILayout.Space(10);

        // ボタンの作成
        if (GUILayout.Button("カメラの背景色をキーの色に設定する")) //Match Key Color to Solid Color
        {
            script.MatchKeyColorToSolidColor();
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("【透過色設定】");
        EditorGUILayout.Space(5);

        // キーカラーとカメラのソリッドカラーの設定
        script.keyColor = EditorGUILayout.ColorField("透過する対象の色", script.keyColor); //Key Color

        EditorGUILayout.Space(10);

        // 自動色同期
        script.autoSyncColors_SolidColorToKeyColor = EditorGUILayout.Toggle("Auto Sync Colors / 使う場合はカメラ側のチェックを外す", script.autoSyncColors_SolidColorToKeyColor);

        EditorGUILayout.Space(10);

        // ボタンの作成
        if (GUILayout.Button("透過する対象の色をカメラの背景色にする"))//Match Solid Color to Key Color
        {
            script.MatchSolidColorToKeyColor();
        }
        EditorGUILayout.Space(10);

        // クロマキーのしきい値とスムーズネス
        script.threshold = EditorGUILayout.Slider("Threshold", script.threshold, 0f, 1f);
        script.smoothness = EditorGUILayout.Slider("Smoothness", script.smoothness, 0f, 1f);

        // スクリプトの変更があった場合、エディタに変更を保存
        if (GUI.changed)
        {
            EditorUtility.SetDirty(script);
        }
    }
}
#endif
