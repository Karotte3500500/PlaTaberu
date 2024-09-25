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
    //記録されたプラタベル（IDを保存）
    public static List<int> _RecodedPlataberu = new List<int>() { 1,2,5,6,8 };
    //記録されたプラタベルの数
    private static int recodedNum = 1; 


    //セーブ
    public static void SavePlayerData()
    {
        //ログイン回数
        PlayerPrefs.SetInt("SaveData_LoginCount", _LoginCount);

        //プラスチックのデータ
        PlayerPrefs.SetFloat("SaveData_RedPlastic", _RedPlastic);
        PlayerPrefs.SetFloat("SaveData_GreenPlastic", _GreenPlastic);
        PlayerPrefs.SetFloat("SaveData_BluePlastic", _BluePlastic);

        //所持しているアイテムの個数
        for (int i = 0; i < _Items.Length; i++)
            PlayerPrefs.SetInt($"SaveData_Item{i}", _Items[i]);

        /*記録されたプラタベルの数の取得*/
        recodedNum = 0;
        foreach (int beru in _RecodedPlataberu) recodedNum++;
        //記録されたプラタベルの数
        PlayerPrefs.SetInt($"SaveData_recodedNum",recodedNum);
        //記録されたプラタベル
        for (int i = 0; i < recodedNum; i++)
            PlayerPrefs.SetInt($"SavaData_RecodedPlataberu{i}", _RecodedPlataberu[i]);
    }

    //ロード
    public static void LoadPlayerData()
    {
        //ログイン回数
        _LoginCount = PlayerPrefs.GetInt("SaveData_LoginCount", _LoginCount);

        //プラスチックのデータ
        _RedPlastic = PlayerPrefs.GetFloat("SaveData_RedPlastic", _RedPlastic);
        _GreenPlastic = PlayerPrefs.GetFloat("SaveData_GreenPlastic", _GreenPlastic);
        _BluePlastic = PlayerPrefs.GetFloat("SaveData_BluePlastic", _BluePlastic);

        //所持しているアイテム
        for (int i = 0; i < _Items.Length; i++)
            _Items[i] = PlayerPrefs.GetInt($"SaveData_Item{i}", _Items[i]);

        //記録されたプラタベルの数
        recodedNum = PlayerPrefs.GetInt("SaveData_recodedNum", recodedNum);
        //記録されたプラタベル
        if (recodedNum > 1)
        {
            _RecodedPlataberu.Clear();
            for (int i = 0; i < recodedNum; i++)
                _RecodedPlataberu.Add(PlayerPrefs.GetInt($"SavaData_RecodedPlataberu{i}", 1));
        }
    }

    //プラタベルを記録する
    public static int Recode(int id)
    {
        //既に記録されている場合は-1を返す
        foreach (var num in _RecodedPlataberu)
            if (num == id) return -1;

        //追加と並び替え
        _RecodedPlataberu.Add(id);
        _RecodedPlataberu.Sort();

        return id;
    }
    //一応Plataberu型でも処理可能
    public static int Recode(Plataberu beru)
    {
        return Recode(beru.ID);
    }
}
