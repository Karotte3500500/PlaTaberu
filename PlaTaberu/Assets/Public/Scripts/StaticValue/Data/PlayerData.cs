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
    //�L�^���ꂽ�v���^�x���iID��ۑ��j
    public static List<int> _RecodedPlataberu = new List<int>() { 1,2,5,6,8 };
    //�L�^���ꂽ�v���^�x���̐�
    private static int recodedNum = 1; 


    //�Z�[�u
    public static void SavePlayerData()
    {
        //���O�C����
        PlayerPrefs.SetInt("SaveData_LoginCount", _LoginCount);

        //�v���X�`�b�N�̃f�[�^
        PlayerPrefs.SetFloat("SaveData_RedPlastic", _RedPlastic);
        PlayerPrefs.SetFloat("SaveData_GreenPlastic", _GreenPlastic);
        PlayerPrefs.SetFloat("SaveData_BluePlastic", _BluePlastic);

        //�������Ă���A�C�e���̌�
        for (int i = 0; i < _Items.Length; i++)
            PlayerPrefs.SetInt($"SaveData_Item{i}", _Items[i]);

        /*�L�^���ꂽ�v���^�x���̐��̎擾*/
        recodedNum = 0;
        foreach (int beru in _RecodedPlataberu) recodedNum++;
        //�L�^���ꂽ�v���^�x���̐�
        PlayerPrefs.SetInt($"SaveData_recodedNum",recodedNum);
        //�L�^���ꂽ�v���^�x��
        for (int i = 0; i < recodedNum; i++)
            PlayerPrefs.SetInt($"SavaData_RecodedPlataberu{i}", _RecodedPlataberu[i]);
    }

    //���[�h
    public static void LoadPlayerData()
    {
        //���O�C����
        _LoginCount = PlayerPrefs.GetInt("SaveData_LoginCount", _LoginCount);

        //�v���X�`�b�N�̃f�[�^
        _RedPlastic = PlayerPrefs.GetFloat("SaveData_RedPlastic", _RedPlastic);
        _GreenPlastic = PlayerPrefs.GetFloat("SaveData_GreenPlastic", _GreenPlastic);
        _BluePlastic = PlayerPrefs.GetFloat("SaveData_BluePlastic", _BluePlastic);

        //�������Ă���A�C�e��
        for (int i = 0; i < _Items.Length; i++)
            _Items[i] = PlayerPrefs.GetInt($"SaveData_Item{i}", _Items[i]);

        //�L�^���ꂽ�v���^�x���̐�
        recodedNum = PlayerPrefs.GetInt("SaveData_recodedNum", recodedNum);
        //�L�^���ꂽ�v���^�x��
        if (recodedNum > 1)
        {
            _RecodedPlataberu.Clear();
            for (int i = 0; i < recodedNum; i++)
                _RecodedPlataberu.Add(PlayerPrefs.GetInt($"SavaData_RecodedPlataberu{i}", 1));
        }
    }

    //�v���^�x�����L�^����
    public static int Recode(int id)
    {
        //���ɋL�^����Ă���ꍇ��-1��Ԃ�
        foreach (var num in _RecodedPlataberu)
            if (num == id) return -1;

        //�ǉ��ƕ��ёւ�
        _RecodedPlataberu.Add(id);
        _RecodedPlataberu.Sort();

        return id;
    }
    //�ꉞPlataberu�^�ł������\
    public static int Recode(Plataberu beru)
    {
        return Recode(beru.ID);
    }
}
