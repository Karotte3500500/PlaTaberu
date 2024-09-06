using System.Collections;
using System.Collections.Generic;
using GameCharacterManagement;
using UnityEngine;

public static class PlayerData
{
    //���O�C�������񐔂��L�^����
    public static int _LoginCount = 0;

    //�v���C���[���擾�����v���X�`�b�N
    public static float _RedPlastic = 1000;
    public static float _GreenPlastic = 1000;
    public static float _BluePlastic = 1000;

    //�v���C���[���琬����v���^�x��
    public static Plataberu _Plataberu = new Belu();
    //�������Ă���A�C�e���̌�
    public static int[] _Items = new int[5] { 0, 3, 3, 3, 3 };

    //�Z�[�u
    public static void SavePlayerData()
    {
        PlayerPrefs.SetInt("SaveData_LoginCount", _LoginCount);

        PlayerPrefs.SetFloat("SaveData_RedPlastic", _RedPlastic);
        PlayerPrefs.SetFloat("SaveData_GreenPlastic", _GreenPlastic);
        PlayerPrefs.SetFloat("SaveData_BluePlastic", _BluePlastic);

        for (int i = 0; i < _Items.Length; i++)
            PlayerPrefs.SetInt($"SaveData_Item{i}", _Items[i]);
    }

    //���[�h
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
