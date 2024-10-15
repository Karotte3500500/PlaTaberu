using GameCharacterManagement;
using XmlConverting;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BattleDirector_n : MonoBehaviour
{
    [SerializeField]
    private GameObject friendImg;
    [SerializeField]
    private GameObject enemyImg;

    private GameObject[] characters;

    [SerializeField]
    private CharacterManager_n[] characterImg = new CharacterManager_n[2];

    /*�v���C���[�����삷��v���^�x��*/
    [SerializeField]
    private GameObject character;
    /*UI*/
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject[] hpBar;
    [SerializeField]
    private Button decide;

    /*���j���[���\������Ă��邩*/
    public bool OpeningMenu = false;

    //�I���������ǂ���
    public bool EndBattle = false;

    /*�\�������HP*/
    public float[] displayHP = new float[2];

    private Commands_n commands;
    private FileControl file;
    private Plataberu[] plataberus = new Plataberu[2] { CharacterData._Plataberu, ServerCommunication._EnemyCharacter };
    private bool BattleRunned = false;
    private bool gottenBeta = false;

    private void Start()
    {
        ServerCommunication.SetAddress();
        GlobalValue.enemy = ServerCommunication._EnemyCharacter;
        for (int i = 0; i < 2; i++)
        {
            var beru = plataberus[i];

            /*�f�o�b�O�p*/
            beru.AddGrp(10000);
            beru.LevelUp();

            //�ق��
            beru.BattleStatusReset();
            displayHP[i] = beru.ActualStatus.HP;
        }
        menu.SetActive(false);
        file = FindObjectOfType<FileControl>();
        commands = FindObjectOfType<Commands_n>();

        characters = new GameObject[2] { friendImg, enemyImg };

        //CharacterManager�̃R���|�[�l���g���擾
        characterImg = new CharacterManager_n[2]
        {
            friendImg.GetComponent<CharacterManager_n>(),
            enemyImg.GetComponent<CharacterManager_n>(),
        };

        //�����̃A�o�^�[��ݒ�
        characterImg[0].Layer = 2;
        characterImg[0].ID = plataberus[0].ID;
        characterImg[0].back = true;
        //�G�̃A�o�^�[��ݒ�
        characterImg[1].Layer = 1;
        characterImg[1].ID = plataberus[1].ID;
    }

    private void Update()
    {
        /*���j���[���J���Ă���Ȃ珈���𒆎~����*/
        OpeningMenu = menu.activeSelf || EndBattle;
        decide.interactable = !OpeningMenu;
        if (OpeningMenu) return;

        /*�퓬�̏���*/
        if (!commands.choicing) 
        {
            MoveBattle();
        }

        //HP�o�[�̏���
        setHpbar(0);
        setHpbar(1);
    }

    private int count = 0;
    private int turnCount = 1;
    private bool endAnimation = false;
    private void MoveBattle()
    {
        if (BattleRunned)
        {
            //�퓬���[�V�����̔���
            bool isBattleMotion = true;
            foreach (var img in characters)
            {
                img.transform.GetChild(0);
                string animationName = img.transform.GetChild(0).gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name;
                isBattleMotion = animationName != "idol";
                if (!isBattleMotion)
                {
                    break;
                }
            }
            //���݂����ҋ@���[�V�������K�[�h���̂݃J�E���g����
            if (!isBattleMotion)
            {
                count++;

                //�\������HP�̌v�Z
                if (!endAnimation && turnCount > 0)
                {
                    int num = (count / 100);
                    endAnimation = true;

                    var damages = plataberus[1 - (num % 2)].DamagesInflicted;
                    if (damages.Count >= turnCount)
                        displayHP[1 - (num % 2)] -= damages[turnCount - 1];
                }
                if(count % 200 == 0)
                {
                    characterImg[0].CharacterAnimation = 1;
                    characterImg[1].CharacterAnimation = 1;
                }
            }

            if (count % 100 == 0)
            {
                int num = (count / 100);
                Debug.Log($"num1:{(num % 2)}  num2;{(1 - (num % 2))}  turn:{turnCount}");
                //�A�j���[�V���������蓖�Ă�
                RunBattleAnimation(num % 2, 1 - (num % 2), turnCount);
            }

            if (count >= 100 * (setTurn(plataberus[0]) + setTurn(plataberus[1])))
            {
                //�R�}���h�I���֑J��
                turnCount = 1;
                count = 0;
                commands.choicing = true;
                BattleRunned = false;
                gottenBeta = false;
                foreach (var beru in plataberus)
                    beru.WaveReset();

                //HP��ݒ�@�@����̏����Ɠ������Ȃ�����
                for (int i = 0; i < 2; i++)
                {
                    displayHP[i] = plataberus[i].BattleStatus.HP;

                    //�\������HP��0�ɂȂ����ꍇ
                    if ((int)displayHP[i] <= 0)
                    {
                        displayHP[i] = 0;
                        //���s���������i���݂��ɕ������ꍇ�A�����������j
                        EndBattle = true;
                        GlobalValue._Victory = 1 - i;
                    }
                }
            }
        }
        else
        {
            /*�f�o�b�O�p*/
            //EnemyCommandSet();
            Debug.Log(plataberus[0].DebugString());
            Debug.Log(plataberus[1].DebugString());
            //beta�t�@�C�����͂����Ȃ�
            if (gottenBeta)
            {
                ////****�����̏����͕s���Ȃ̂ŗv�m�F****////
                //�t�@�C���̑���M��Ԃ��m�F
                switch (file.SendProgress)
                {
                    //����ɑ���M�ł����ꍇ
                    case 1:
                        if (!ServerCommunication.alpha)
                        {
                            var data
                                = ConvertorXML.DeserializeBattleDataAlpha(Application.persistentDataPath + $"/BattleData_Alpha").WriteData(plataberus[0], plataberus[1]);
                            plataberus[0] = data.Friend;
                            plataberus[1] = data.Enemy;
                            RunBattle();
                        }
                        gottenBeta = true;
                        file.SendProgress = -1;
                        break;
                    //�܂�����M����Ă��Ȃ��ꍇ
                    case -1:
                        if (!ServerCommunication.alpha)
                        {
                            file.ReceiveFile($"BattleData_Alpha", 5002);
                        }
                        else
                        {
                            RunBattle();
                            string fileName = $"BattleData_Alpha";
                            ConvertorXML.SerializeBattleDataAlpha(plataberus[0], plataberus[1], Application.persistentDataPath + "/" + fileName);
                            StartCoroutine(file.UploadFileCoroutine(fileName));
                        }
                        break;
                    //����M�ŃG���[�����������ꍇ
                    case -2:
                        if (!ServerCommunication.alpha)
                        {
                            file.ReceiveFile($"BattleData_Alpha", 5002);
                        }
                        else
                        {
                            RunBattle();
                            string fileName = $"BattleData_Alpha";
                            ConvertorXML.SerializeBattleDataAlpha(plataberus[0], plataberus[1], Application.persistentDataPath + "/" + fileName);
                            StartCoroutine(file.UploadFileCoroutine(fileName));
                        }
                        break;
                }
                ////////////////////////////////////////////////////
                count = 0;
                BattleRunned = true;
            }
            //beta�t�@�C�����͂��Ă��Ȃ��Ȃ�
            else
            {
                //�t�@�C���̑���M��Ԃ��m�F
                switch (file.SendProgress)
                {
                    //����ɑ���M�ł����ꍇ
                    case 1:
                        if (ServerCommunication.alpha)
                            plataberus[1]
                                = ConvertorXML.DeserializeBattleDataBeta(Application.persistentDataPath + $"/BattleData_Beta").WriteData(plataberus[1]);
                        gottenBeta = true;
                        file.SendProgress = -1;
                        break;
                    //�܂�����M����Ă��Ȃ��ꍇ
                    case -1:
                        if (ServerCommunication.alpha)
                        {
                            file.ReceiveFile($"BattleData_Beta", 5002);
                        }
                        else
                        {
                            string fileName = $"BattleData_Beta";
                            ConvertorXML.SerializeBattleDataBeta(plataberus[0], Application.persistentDataPath + "/" + fileName);
                            StartCoroutine(file.UploadFileCoroutine(fileName));
                        }
                        break;
                    //����M�ŃG���[�����������ꍇ
                    case -2:
                        if (ServerCommunication.alpha)
                        {
                            file.ReceiveFile($"BattleData_Beta", 5002);
                        }
                        else
                        {
                            string fileName = $"BattleData_Beta";
                            ConvertorXML.SerializeBattleDataBeta(plataberus[0], Application.persistentDataPath + "/" + fileName);
                            StartCoroutine(file.UploadFileCoroutine(fileName));
                        }
                        break;
                }
            }
        }
    }

    private int setTurn(Plataberu beru)
    {
        int turn = 0;
        foreach (var com in beru.BattleCommand.SelectedCommand)
            turn++;
        return turn;
    }

    private void action(int num, int turn)
    {
        if (turn > setTurn(plataberus[num]) - 1) return;
        plataberus[num].BattleMove(plataberus[num], plataberus[num == 0 ? 1 : 0], turn);
    }
    //�퓬����������
    private void RunBattle()
    {
        //�f�o�b�O�p
        int len = setTurn(plataberus[0]) > setTurn(plataberus[1]) ? setTurn(plataberus[0]) : setTurn(plataberus[1]);
        Debug.Log(len);
        for (int i = 0; i < len; i++)
        {
            action(0, i);
            action(1, i);
        }
    }

    //�A�j���[�V���������s����
    private void RunBattleAnimation(int beru1, int beru2, int turn)
    {
        //�C���f�b�N�X�O�̏ꍇ�͏��������Ȃ�
        int[] maxTurn = new int[2] { setTurn(plataberus[beru1]), setTurn(plataberus[beru2]) };
        Debug.Log($"{maxTurn[0]} , {maxTurn[1]} , {turn} , {maxTurn[0] < turn}");
        if (maxTurn[0] < turn) return;

        //�R�}���h�ɉ������A�j���V������ݒ�
        characterImg[beru1].CharacterAnimation
            = plataberus[beru1].BattleCommand.SelectedCommand[turn - 1] + 4;

        //�K�[�h�̗L��
        bool hadGuard = maxTurn[1] < turn ? false : plataberus[beru2].BattleCommand.SelectedCommand[turn - 1] == 1;

        //��e���[�V�����𑊎�ɐݒ肷��
        if (plataberus[beru1].BattleCommand.SelectedCommand[turn - 1] == 0 && hadGuard)
            characterImg[beru2].CharacterAnimation = 5;
        endAnimation = false;
    }

    //�f�o�b�O�p
    private void EnemyCommandSet()
    {
        int num = Random.Range(0, 4);
        for (int i = 0;i < num;i++)
        {
            plataberus[1].BattleCommand.SelectedCommand.Add(Random.Range(0, 3));
        }
    }

    private void setHpbar(int num)
    {
        hpBar[num].GetComponent<HPbar_n>().MaxValue = plataberus[num].ActualStatus.HP;
        hpBar[num].GetComponent<HPbar_n>().NowValue = displayHP[num] <= 0 ? 0 : displayHP[num];
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
    }
}