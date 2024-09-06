using System.Collections;
using System.Collections.Generic;
using GameCharacterManagement;
using UnityEngine;

public static class PlayerData
{
    //ログインした回数を記録する
    public static int _LoginCount = 0;

    //プレイヤーが取得したプラスチック
    public static float _RedPlastic = 1000;
    public static float _GreenPlastic = 1000;
    public static float _BluePlastic = 1000;

    //プレイヤーが育成するプラタベル
    public static Plataberu _Plataberu = new Belu();
    //所持しているアイテムの個数
    public static int[] _Items = new int[5] { 0, 3, 3, 3, 3 };

    //セーブ
    public static void SavePlayerData()
    {
        PlayerPrefs.SetInt("SaveData_LoginCount", _LoginCount);

        PlayerPrefs.SetFloat("SaveData_RedPlastic", _RedPlastic);
        PlayerPrefs.SetFloat("SaveData_GreenPlastic", _GreenPlastic);
        PlayerPrefs.SetFloat("SaveData_BluePlastic", _BluePlastic);

        for (int i = 0; i < _Items.Length; i++)
            PlayerPrefs.SetInt($"SaveData_Item{i}", _Items[i]);
    }

    //ロード
    public static void LoadPlayerData()
    {
        _LoginCount = PlayerPrefs.GetInt("SaveData_LoginCount", _LoginCount);

        _RedPlastic = PlayerPrefs.GetFloat("SaveData_RedPlastic", _RedPlastic);
        _GreenPlastic = PlayerPrefs.GetFloat("SaveData_GreenPlastic", _GreenPlastic);
        _BluePlastic = PlayerPrefs.GetFloat("SaveData_BluePlastic", _BluePlastic);

        for (int i = 0; i < _Items.Length; i++)
            _Items[i] = PlayerPrefs.GetInt($"SaveData_Item{i}", _Items[i]);
    }
}
