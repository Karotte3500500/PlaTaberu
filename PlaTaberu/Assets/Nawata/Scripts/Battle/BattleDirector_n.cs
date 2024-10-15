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

    /*プレイヤーが操作するプラタベル*/
    [SerializeField]
    private GameObject character;
    /*UI*/
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject[] hpBar;
    [SerializeField]
    private Button decide;

    /*メニューが表示されているか*/
    public bool OpeningMenu = false;

    //終了したかどうか
    public bool EndBattle = false;

    /*表示されるHP*/
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

            /*デバッグ用*/
            beru.AddGrp(10000);
            beru.LevelUp();

            //ほんぺ
            beru.BattleStatusReset();
            displayHP[i] = beru.ActualStatus.HP;
        }
        menu.SetActive(false);
        file = FindObjectOfType<FileControl>();
        commands = FindObjectOfType<Commands_n>();

        characters = new GameObject[2] { friendImg, enemyImg };

        //CharacterManagerのコンポーネントを取得
        characterImg = new CharacterManager_n[2]
        {
            friendImg.GetComponent<CharacterManager_n>(),
            enemyImg.GetComponent<CharacterManager_n>(),
        };

        //味方のアバターを設定
        characterImg[0].Layer = 2;
        characterImg[0].ID = plataberus[0].ID;
        characterImg[0].back = true;
        //敵のアバターを設定
        characterImg[1].Layer = 1;
        characterImg[1].ID = plataberus[1].ID;
    }

    private void Update()
    {
        /*メニューが開いているなら処理を中止する*/
        OpeningMenu = menu.activeSelf || EndBattle;
        decide.interactable = !OpeningMenu;
        if (OpeningMenu) return;

        /*戦闘の処理*/
        if (!commands.choicing) 
        {
            MoveBattle();
        }

        //HPバーの処理
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
            //戦闘モーションの判別
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
            //お互いが待機モーションかガード時のみカウントする
            if (!isBattleMotion)
            {
                count++;

                //表示するHPの計算
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
                //アニメーションを割り当てる
                RunBattleAnimation(num % 2, 1 - (num % 2), turnCount);
            }

            if (count >= 100 * (setTurn(plataberus[0]) + setTurn(plataberus[1])))
            {
                //コマンド選択へ遷移
                turnCount = 1;
                count = 0;
                commands.choicing = true;
                BattleRunned = false;
                gottenBeta = false;
                foreach (var beru in plataberus)
                    beru.WaveReset();

                //HPを設定　　※上の処理と統合しないこと
                for (int i = 0; i < 2; i++)
                {
                    displayHP[i] = plataberus[i].BattleStatus.HP;

                    //表示するHPが0になった場合
                    if ((int)displayHP[i] <= 0)
                    {
                        displayHP[i] = 0;
                        //勝敗判定をする（お互いに負けた場合、後方が負ける）
                        EndBattle = true;
                        GlobalValue._Victory = 1 - i;
                    }
                }
            }
        }
        else
        {
            /*デバッグ用*/
            //EnemyCommandSet();
            Debug.Log(plataberus[0].DebugString());
            Debug.Log(plataberus[1].DebugString());
            //betaファイルが届いたなら
            if (gottenBeta)
            {
                ////****ここの処理は不安なので要確認****////
                //ファイルの送受信状態を確認
                switch (file.SendProgress)
                {
                    //正常に送受信できた場合
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
                    //まだ送受信されていない場合
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
                    //送受信でエラーが発生した場合
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
            //betaファイルが届いていないなら
            else
            {
                //ファイルの送受信状態を確認
                switch (file.SendProgress)
                {
                    //正常に送受信できた場合
                    case 1:
                        if (ServerCommunication.alpha)
                            plataberus[1]
                                = ConvertorXML.DeserializeBattleDataBeta(Application.persistentDataPath + $"/BattleData_Beta").WriteData(plataberus[1]);
                        gottenBeta = true;
                        file.SendProgress = -1;
                        break;
                    //まだ送受信されていない場合
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
                    //送受信でエラーが発生した場合
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
    //戦闘処理をする
    private void RunBattle()
    {
        //デバッグ用
        int len = setTurn(plataberus[0]) > setTurn(plataberus[1]) ? setTurn(plataberus[0]) : setTurn(plataberus[1]);
        Debug.Log(len);
        for (int i = 0; i < len; i++)
        {
            action(0, i);
            action(1, i);
        }
    }

    //アニメーションを実行する
    private void RunBattleAnimation(int beru1, int beru2, int turn)
    {
        //インデックス外の場合は処理をしない
        int[] maxTurn = new int[2] { setTurn(plataberus[beru1]), setTurn(plataberus[beru2]) };
        Debug.Log($"{maxTurn[0]} , {maxTurn[1]} , {turn} , {maxTurn[0] < turn}");
        if (maxTurn[0] < turn) return;

        //コマンドに応じたアニメションを設定
        characterImg[beru1].CharacterAnimation
            = plataberus[beru1].BattleCommand.SelectedCommand[turn - 1] + 4;

        //ガードの有無
        bool hadGuard = maxTurn[1] < turn ? false : plataberus[beru2].BattleCommand.SelectedCommand[turn - 1] == 1;

        //被弾モーションを相手に設定する
        if (plataberus[beru1].BattleCommand.SelectedCommand[turn - 1] == 0 && hadGuard)
            characterImg[beru2].CharacterAnimation = 5;
        endAnimation = false;
    }

    //デバッグ用
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