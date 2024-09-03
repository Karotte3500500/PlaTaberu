using GameCharacterManagement;
using GameCharacterManagement.Battle;
using System;
using System.Collections.Generic;

namespace GameCharacterManagement
{
    //ベルの設定***************************************************************************************
    public class Belu : Plataberu
    {
        public override int ID => 1;
        public override string Name => "ベル";
        public override string Explanation => "プラタベルの子供、プラスチックを食べて育つ ";
        public override int Tier => 1;
        public override Ratio GrowthRatio => new Ratio(1.0f, 1.0f, 1.0f);
        public override Command BattleCommand { get; set; } = new Command(4, 4, 0);
        public override int NextLevel => 15;

        public override Item[] ItemSlot { get; set; } = new Item[3];

        public override string SkillName => "なにもしない";
        public override string SkillExplanation => "なにもしない";

        public override void Skill(Plataberu enemy)
        {
            base.Skill(enemy);
        }
    }

    //ケイの設定***************************************************************************************
    public class Kei : Plataberu
    {
        public override int ID => 2;
        public override string Name => "ケイ";
        public override string Explanation => "自然の中で育ったプラタベル。群れで暮らす。";
        public override int Tier => 2;
        public override Ratio GrowthRatio => new Ratio(2.5f, 1.0f, 1.5f);
        public override Command BattleCommand { get; set; } = new Command(3, 4, 2);
        public override int NextLevel => 30;

        public override Item[] ItemSlot { get; set; } = new Item[3];

        public override string SkillName => "ほえる";
        public override string SkillExplanation => "相手の防御力を下げる";

        public override void Skill(Plataberu enemy)
        {
            Status cof = enemy.BattleCoefficient;
            cof.DEF *= 0.8f;
            enemy.BattleCoefficient = cof;
        }
    }

    //バハムートの設定***************************************************************************************
    public class Vaha : Plataberu
    {
        public override int ID => 4;
        public override string Name => "バハムート";
        public override string Explanation => "翼で空を飛び、火を吹く。";
        public override int Tier => 3;
        public override Ratio GrowthRatio => new Ratio(3.5f, 2.0f, 1.5f);
        public override Command BattleCommand { get; set; } = new Command(2, 3, 10);
        public override int NextLevel => 1000;

        public override Item[] ItemSlot { get; set; } = new Item[1];

        public override string SkillName => "カタストロフ";
        public override string SkillExplanation => "防御貫通ダメージ";

        public override void Skill(Plataberu enemy)
        {
            Status st = enemy.BattleStatus;
            st.HP -= this.AttackThrough(enemy, this.BattleCritical) * 1.0f;
            enemy.BattleStatus = st;
        }
    }

    //コナの設定***************************************************************************************
    public class Cona : Plataberu
    {
        public override int ID => 5;
        public override string Name => "コナ";
        public override string Explanation => "少し先の未来を予知することができる。 ";
        public override int Tier => 3;
        public override Ratio GrowthRatio => new Ratio(1f, 2f, 1.5f);
        public override Command BattleCommand { get; set; } = new Command(2, 3, 3);
        public override int NextLevel => 1000;

        public override Item[] ItemSlot { get; set; } = new Item[3];

        public override int BaseCritical => 20;
        public override string SkillName => "真経津鏡";
        public override string SkillExplanation => "次の攻撃が必ずクリティカルになり、敵の防御力を1ターン大ダウン";

        public override void Skill(Plataberu enemy)
        {
            this.BattleCritical = 100;
            Status tCof = enemy.TemporaryCoefficient;
            tCof.DEF *= 0.5f;
            enemy.TemporaryCoefficient = tCof;
        }
    }

    //リリーの設定***************************************************************************************
    public class Lily : Plataberu
    {
        public override int ID => 6;
        public override string Name => "リリー";
        public override string Explanation => "人懐っこい性格。触るとふわふわしている。";
        public override int Tier => 3;
        public override Ratio GrowthRatio => new Ratio(1f, 1.5f, 4f);
        public override Command BattleCommand { get; set; } = new Command(3, 4, 5);
        public override int NextLevel => 1000;

        public override Item[] ItemSlot { get; set; } = new Item[2];

        public override string SkillName => "ダイヤモンドリリー";
        public override string SkillExplanation => "敵の次の攻撃を反射";

        public override void Skill(Plataberu enemy)
        {
            //ほりゅう
            enemy.Skill(enemy);
        }
    }

    //ニナの設定***************************************************************************************
    public class Nina : Plataberu
    {
        public override int ID => 3;
        public override string Name => "ニナ";
        public override string Explanation => "人と育ったプラタベル。言葉を話すことができる。 ";
        public override int Tier => 2;
        public override Ratio GrowthRatio => new Ratio(1.5f, 1.5f, 2f);
        public override Command BattleCommand { get; set; } = new Command(4, 3, 5);
        public override int NextLevel => 30;

        public override Item[] ItemSlot { get; set; } = new Item[3];

        public override string SkillName => "休む";
        public override string SkillExplanation => "HPが基礎HPの一割回復";

        public override void Skill(Plataberu enemy)
        {
            Status mySt = this.BattleStatus;
            mySt.HP += this.BaseStatus.HP / 10.0f;
            this.BattleStatus = mySt;
        }
    }

    //デュランダルの設定***************************************************************************************
    public class Dhura : Plataberu
    {
        public override int ID => 7;
        public override string Name => "デュランダル";
        public override string Explanation => "「聖剣」の二つ名を持つプラタベル。正義感あふれる性格。";
        public override int Tier => 3;
        public override Ratio GrowthRatio => new Ratio(2.5f, 2.5f, 2f);
        public override Command BattleCommand { get; set; } = new Command(2, 3, 6);
        public override int NextLevel => 1000;

        public override Item[] ItemSlot { get; set; } = new Item[2];

        public override string SkillName => "ローラン・オルランド";
        public override string SkillExplanation => "敵に攻撃し、防御力を上昇";

        public override void Skill(Plataberu enemy)
        {
            Status st = enemy.BattleStatus;
            st.HP -= Attack(enemy, this.BattleCritical) * 1.2f;
            enemy.BattleStatus = st;

            Status myCof = this.BattleCoefficient;
            myCof.DEF *= 1.2f;
            this.BattleCoefficient = myCof;
        }
    }

    //エリザベートの設定***************************************************************************************
    public class Eri : Plataberu
    {
        public override int ID => 8;
        public override string Name => "エリザベート";
        public override string Explanation => "赤いプラスチックが好きなプラタベル。太陽が苦手。インドア派。";
        public override int Tier => 3;
        public override Ratio GrowthRatio => new Ratio(2.5f, 1.0f, 3f);
        public override Command BattleCommand { get; set; } = new Command(2, 2, 4);
        public override int NextLevel => 1000;

        public override Item[] ItemSlot { get; set; } = new Item[2];

        public override string SkillName => "ヴラム・カーミラ";
        public override string SkillExplanation => "敵にダメージを与え、ダメージに比例して自身（自信）を回復する";

        public override void Skill(Plataberu enemy)
        {

            Status st = enemy.BattleStatus;
            float damage = Attack(enemy, this.BattleCritical) * 1.2f;
            st.HP -= damage;
            enemy.BattleStatus = st;

            Status mySt = this.BattleStatus;
            mySt.HP += damage * (2.0f / 5.0f);
            this.BattleStatus = mySt;
        }
    }

    //オーディンの設定***************************************************************************************
    public class Odin : Plataberu
    {
        public override int ID => 9;
        public override string Name => "オーディン";
        public override string Explanation => "馬に乗り、大地を駆け、槍で敵を刺し貫く。";
        public override int Tier => 3;
        public override Ratio GrowthRatio { get { return new Ratio(3.0f, 2.5f, 2f); } }
        public override Command BattleCommand { get; set; } = new Command(2, 3, 4);
        public override int NextLevel => 1000;

        public override Item[] ItemSlot { get; set; } = new Item[1];

        public override string SkillName => "ヴォータン・グングニル";
        public override string SkillExplanation => "攻撃し、敵の防御をダウン";

        public override void Skill(Plataberu enemy)
        {
            Status st = enemy.BattleStatus;
            st.HP -= Attack(enemy, this.BattleCritical) * 1.2f;
            enemy.BattleStatus = st;
            Status cof = enemy.BattleCoefficient;
            cof.DEF *= 0.8f;
            enemy.BattleCoefficient = cof;
        }
    }

    //プラタベルの定義***************************************************************************************
    public class Plataberu
    {
        public virtual int ID { get { return 0; } }
        //プラタベルの名前
        public virtual string Name { get { return "ベル"; } }
        //プラタベルの説明
        public virtual string Explanation { get { return "プラスチックを食べて成長する不思議な生き物"; } }
        //プラタベルの評価(1~3)通常のtierと異なり数値が大きいほど強い
        public virtual int Tier { get { return 1; } }
        //プラタベルの総成長値 - 変更禁止
        public float TotalGrp { get; private set; }
        //プラタベルの成長割合
        public virtual Ratio GrowthRatio { get { return new Ratio(1, 1, 1); } }
        //プラタベルの成長タイプ - 変更禁止
        public string GrowthType { get { return GrowthRatio.Type; } }

        /*今まで得たプラスチックの種類(ATK：赤,DEF：青,HP：緑)*/
        public Status Plastics { get; private set; } = Status.Zero;

        //現在の成長値 - 変更禁止
        public float Grp { get; private set; }
        //次のレベルまでに必要な成長値 - 変更禁止
        public float TargetGrp { get { return 100 * (((float)this.Level / 10) + 1); } }
        //現在の成長値の割合（GRPバー用）
        public float GrpRatio { get { return Grp / TargetGrp; } }
        //前のレベル
        public int OldLevel { get; private set; }
        //現在のレベル - 変更禁止
        public int Level { get; private set; }
        //成長に必要なレベル
        public virtual int NextLevel { get { return 15; } }

        //アイテムスロット
        public virtual Item[] ItemSlot { get; set; } = new Item[0];

        //基本ステータス - 変更禁止
        public Status BaseStatus { get; private set; } = Status.Zero;
        //実際のステータス
        public Status ActualStatus { get; private set; } = Status.Zero;
        //戦闘時に変化させるステータス
        public Status BattleStatus { get; set; } = Status.Zero;
        //永続的なバフ・デバフ係数
        public Status BattleCoefficient { get; set; } = Status.One;
        //1ターンのみ効果的なバフ・デバフ係数
        public Status TemporaryCoefficient { get; set; } = Status.One;

        //基礎クリティカル率
        public virtual int BaseCritical { get { return 10; } }
        //戦闘時クリティカル率
        public int BattleCritical { get; set; }

        //戦闘時の攻撃
        public virtual Command BattleCommand { get; set; } = new Command(1, 1, 1);
        //被ダメージ
        public List<float> DamagesSuffered { get; set; } = new List<float>();
        //受ダメージ
        public List<float> DamagesInflicted { get; set; } = new List<float>();

        //スキル
        public virtual void Skill(Plataberu enemy) { }
        //スキル名
        public virtual string SkillName { get { return "-"; } }
        //スキルの説明
        public virtual string SkillExplanation { get { return "-"; } }

        //戦闘時のステータス初期化
        public void BattleStatusReset()
        {
            this.BattleStatus = this.ActualStatus;
            this.BattleCoefficient = Status.One;
            this.BattleCritical = this.BaseCritical;
            this.BattleCommand.AllReset();
        }

        //基本ステータスに加算
        public void GrowStatus()
        {
            float coefficient = 1.3f;
            Status baseStatus = this.BaseStatus;
            baseStatus.ATK += ((coefficient * this.Tier) + this.Level) * (this.GrowthRatio.ATK + 1);
            baseStatus.DEF += ((coefficient * this.Tier) + this.Level) * (this.GrowthRatio.DEF + 1);
            baseStatus.HP += ((coefficient * this.Tier) + this.Level) * (this.GrowthRatio.HP + 1) * 15;

            this.BaseStatus = baseStatus;

            SetActualStatus();
        }

        private void SetActualStatus()
        {
            this.ActualStatus = this.BaseStatus;
            Status baseSta = this.BaseStatus;
            Status pla = this.Plastics;
            float plaSum = pla.ATK + pla.DEF + pla.HP;
            if (plaSum != 0)
            {
                baseSta.ATK *= pla.ATK / plaSum;
                baseSta.DEF *= pla.DEF / plaSum;
                baseSta.HP *= pla.HP / plaSum;
            }

            this.ActualStatus = this.ActualStatus.Add(baseSta);
        }

        //相成長値を加算
        public void AddGrp(float grp)
        {
            this.TotalGrp += Math.Abs(grp);
        }

        //レベルと経験値の処理をする。上がったレベルを返す
        public int LevelUp()
        {
            this.OldLevel = this.Level;
            this.Grp = this.TotalGrp;

            for (this.Level = 0; this.Grp >= this.TargetGrp; this.Level++)
            {
                this.Grp -= this.TargetGrp;

                if (this.OldLevel <= this.Level)
                    GrowStatus();
            }

            return this.Level - this.OldLevel;
        }

        public void GetPlastic(float red, float green, float blue)
        {
            this.AddGrp(red + blue + green);

            Status pla = Plastics;
            pla.ATK += red;
            pla.DEF += blue;
            pla.HP += green;

            Plastics = pla;
        }
        public void GetPlastic(Status plastics)
        {
            this.AddGrp(plastics.ATK + plastics.DEF + plastics.HP);

            Status pla = Plastics;
            pla.Add(plastics);

            Plastics = pla;
        }

        //戦闘時の動き
        public int BattleMove(Plataberu own, Plataberu enemy, int turn)
        {
            int coms = this.BattleCommand.SelectedCommand[turn];
            float hp = enemy.BattleStatus.HP;

            if (coms == 0)
            {
                Status enemyStatus = enemy.BattleStatus;
                enemyStatus.HP -= this.Attack(enemy, this.BattleCritical);
                enemy.BattleStatus = enemyStatus;
            }
            else if (coms == 1)
            {
                own.BattleCommand.isDfensing = true;
            }
            else if (coms == 2)
            {
                this.Skill(enemy);
            }
            else
            {
                return -1;
            }

            float dam = hp - enemy.BattleStatus.HP;

            own.DamagesInflicted.Add(dam);
            enemy.DamagesSuffered.Add(dam);

            return coms;
        }

        //通常攻撃
        public float Attack(Plataberu defenser, int critical)
        {
            System.Random random = new System.Random();
            int randomNum = random.Next(0, 100);

            return
                ((this.BattleStatus.ATK * this.BattleCoefficient.ATK) /
                ((defenser.BattleStatus.DEF * defenser.BattleCoefficient.DEF) / 30 + 1)) *
                83 * (randomNum < SetCritical(critical) ? 1.5f : 1) * (defenser.BattleCommand.isDfensing ? 0.5f : 1.0f) * 2;
        }
        //貫通攻撃
        public float AttackThrough(Plataberu defenser, int critical)
        {
            System.Random random = new System.Random();
            int randomNum = random.Next(0, 100);

            return
                ((this.BattleStatus.ATK * this.BattleCoefficient.ATK) /
                (((defenser.BattleStatus.DEF * defenser.BattleCoefficient.DEF) / 30 + 1) / 2)) *
                83 * (randomNum < SetCritical(critical) ? 1.5f : 1) * (defenser.BattleCommand.isDfensing ? 0.5f : 1.0f) * 2;
        }

        public int SetCritical(int critiacl)
        {
            float num = 1;
            foreach (var item in ItemSlot)
            {
                if (item == null)
                    continue;
                if (item.ID == 2)
                    num += 0.5f;
            }
            int crit = (int)((float)critiacl * num);

            return crit >= 100 ? 100 : crit;
        }

        public void WaveReset()
        {
            this.BattleCommand.Reset();
            this.TemporaryCoefficient = Status.One;
            this.BattleCritical = this.BaseCritical;
            this.DamagesInflicted.Clear();
            this.DamagesSuffered.Clear();
        }

        //複製を生成
        public Plataberu Copy()
        {
            Plataberu copy = new Plataberu();

            copy.AddGrp(this.TotalGrp);
            copy.Plastics = this.Plastics;
            copy.BattleStatus = this.BattleStatus;
            copy.LevelUp();

            return copy;
        }

        //情報を文字列にする
        public string DebugString()
        {

            string damages = "与ダメージ：";
            foreach (var dama in this.DamagesInflicted)
                damages += $" {dama:0.00} ,";
            damages += "\n被ダメージ：";
            foreach (var dama in this.DamagesSuffered)
                damages += $" {dama:0.00} ,";

            string items = "アイテム：";
            foreach (var item in this.ItemSlot)
            {
                if (item == null)
                {
                    items += "[Empty]";
                    continue;
                }
                items += $"[{item.Name}]";
            }

            return
                $"名前：{this.Name}　　レベル：{this.Level}lv    Tier：{this.Tier}\n" +
                $"成長タイプ：{this.GrowthType}　　成長割合{this.GrowthRatio.DebugString()}\n" +
                $"{this.Explanation}\n" +
                $"総成長値：{this.TotalGrp:###0.00}grp　　成長値：{this.Grp:##0.00}grp　　" +
                $"目標成長値：{this.TargetGrp:##0.00}grp\n" +
                $"回収したプラスチック：{this.Plastics.DebugString()}\n" +
                $"基本ステータス：{this.BaseStatus.DebugString()}\n" +
                $"実質ステータス：{this.ActualStatus.DebugString()}\n" +
                $"戦闘ステータス：{this.BattleStatus.DebugString()}\n" +
                $"ステータス係数：{this.BattleCoefficient.DebugString()}\n" +
                $"\n[戦闘コマンド]\n{this.BattleCommand.DebugString()}\n" +
                items + "\n" +
                $"スキル名：「{this.SkillName}」\n" + damages;
        }
    }

    //攻撃力・防御力・体力を格納する構造体
    public struct Status
    {
        public float ATK { get; set; }
        public float DEF { get; set; }
        public float HP { get; set; }

        public Status(float atk, float def, float hp)
        {
            this.ATK = atk;
            this.DEF = def;
            this.HP = hp;
        }

        public Status Add(Status status)
        {
            this.ATK += status.ATK;
            this.DEF += status.DEF;
            this.HP += status.HP;
            return this;
        }

        public string DebugString()
        {
            return $"ATK：{this.ATK:##0.00}　　DEF：{this.DEF:##0.00}　　HP：{this.HP:##0.00}";
        }

        //ゼロクリア用
        public static Status Zero { get { return new Status(0, 0, 0); } }
        //ワンクリア用
        public static Status One { get { return new Status(1, 1, 1); } }
    }

    //ステータスの成長割合を管理するクラス
    public class Ratio
    {
        public float ATK { get; private set; }
        public float DEF { get; private set; }
        public float HP { get; private set; }

        public Ratio(float atk, float def, float hp)
        {
            float sum = atk + def + hp;

            this.ATK = (atk / sum) * 10;
            this.DEF = (def / sum) * 10;
            this.HP = (hp / sum) * 10;
        }

        public string Type
        {
            get
            {
                return
                    Math.Abs(this.ATK - this.DEF) < 2 ? (this.HP < 5 ? "General" : "Supporter") :
                    (this.ATK > this.DEF ? "Attacker" : "Defenser");
            }
        }

        //複製を生成
        public Ratio Copy()
        {
            Ratio copy = new Ratio(this.ATK, this.DEF, this.HP);
            return copy;
        }

        //情報を文字列にする
        public string DebugString()
        {
            return $"A：{this.ATK} D：{this.DEF} H：{this.HP}";
        }
    }

    public class Command
    {
        //攻撃時に消費するコスト
        public int AttackCost { get; private set; }
        //防御時に消費するコスト
        public int DefenseCost { get; private set; }
        //スキル発動時に消費するコスト
        public int SkillCost { get; private set; }
        //防御したかどうか
        public bool isDfensing { get; set; } = false;

        //カードを5つ抽選する
        public int[] PopCommand { get { return new int[5] { cSet(), cSet(), cSet(), cSet(), cSet() }; } }

        //毎ターン加算されるコスト
        public int MaxCost { get { return 5; } }
        //現在の持ちコスト
        public int Cost { get; set; } = 0;

        //毎ターン、プレイヤーが選択したカードを格納するリスト
        public List<int> SelectedCommand { get; set; } = new List<int>();

        public Command(int attackCost, int defenseCost, int skillCost)
        {
            this.AttackCost = attackCost; this.DefenseCost = defenseCost; this.SkillCost = skillCost;
        }

        //それぞれのカードのコストを初期化
        public void CostSet(int attackCost, int defenseCost, int skillCost)
        {
            this.AttackCost = attackCost; this.DefenseCost = defenseCost; this.SkillCost = skillCost;
        }

        private int beforeRandomNum = -1;
        private int cSet()
        {
            var tCost = this.AttackCost + this.DefenseCost + this.SkillCost;
            var atk = tCost - this.AttackCost;
            var def = tCost - this.DefenseCost;
            var skl = (tCost - this.SkillCost) / 2;

            var totalCost = atk + def + skl;

            int randNum;
            do
            {
                int[] costs = new int[3] { this.AttackCost, this.DefenseCost, this.SkillCost };
                System.Random random = new System.Random();
                randNum = random.Next(0, totalCost + 1);
            } while (randNum == beforeRandomNum);
            beforeRandomNum = randNum;

            return randNum < atk ? 0 : (randNum < atk + def ? 1 : 2);
        }

        //ターン終了時にCommandをリセットするメソッド
        public void Reset()
        {
            this.Cost += MaxCost;
            this.SelectedCommand.Clear();
            this.isDfensing = false;
        }

        //コストも初期化する
        public void AllReset()
        {
            this.Cost = MaxCost;
            this.SelectedCommand.Clear();
            this.isDfensing = false;
        }

        //複製を生成
        public Command Copy()
        {
            Command copy = new Command(this.AttackCost, this.DefenseCost, this.SkillCost);

            return copy;
        }

        public string DebugString()
        {
            return
                $"攻撃コスト：{this.AttackCost}  防御コスト：{this.DefenseCost}  スキルコスト：{this.SkillCost}\n" +
                $"Maxコスト{this.MaxCost}  現在のコスト：{this.Cost}";
        }

    }


    /*アイテム*/
    public class Item
    {
        public virtual string Name { get { return "アイテム"; } }
        public virtual int ID { get { return 0; } }
        public virtual string Explanation { get { return "いろいろすごいやつ"; } }
    }

    public class Shield : Item
    {
        public override string Name => "シールド";
        public override int ID => 1;
        public override string Explanation => "攻撃を防ぐ盾";
    }

    public class Glasses : Item
    {
        public override string Name => "眼鏡";
        public override int ID => 2;
        public override string Explanation => "クリティカル率を上げる眼鏡";
    }

    namespace Battle
    {
        static class BattleOperater
        {
            //クリティカル
            //防御

            static public float Attack(Plataberu attacker, Plataberu defenser, int critical)
            {
                System.Random random = new System.Random();
                int randomNum = random.Next(0, 100);

                return
                    ((attacker.BattleStatus.ATK * attacker.BattleCoefficient.ATK) /
                    ((defenser.BattleStatus.DEF * defenser.BattleCoefficient.DEF) / 30 + 1)) *
                    83 * (randomNum < critical ? 1.5f : 1) * (defenser.BattleCommand.isDfensing ? 0.5f : 1.0f);
            }

            static public float AttackThrough(Plataberu attacker, Plataberu defenser, int critical)
            {
                System.Random random = new System.Random();
                int randomNum = random.Next(0, 100);

                return
                    ((attacker.BattleStatus.ATK * attacker.BattleCoefficient.ATK) *
                    83 * (randomNum < critical ? 1.5f : 1)) * (defenser.BattleCommand.isDfensing ? 0.5f : 1.0f);
            }
        }
    }
}