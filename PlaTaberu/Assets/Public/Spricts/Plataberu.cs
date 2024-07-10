using System;
using System.Collections.Generic;

namespace GameCharacterManagement
{
    public class Plataberu
    {
        private int lv = 0;
        private float grp = 0;
        private float totalGrp = 0;
        private Status baseStatusWritter = Status.Zero;

        //プラタベルの名前
        public string Name { get; set; } = "ベル";
        //プラタベルの説明
        public string Explanation { get; set; } = "プラスチックを食べて成長する不思議な生き物";
        //プラタベルの評価(1~3)通常のtierと異なり数値が大きいほど強い
        public int Tier { get; set; }
        //プラタベルの総成長値 - 読み取り専用
        public float TotalGrp { get { return totalGrp; } }
        //プラタベルの成長割合
        public GrowthRatio Ratio { get; set; } = new GrowthRatio();
        //プラタベルの成長タイプ - 読み取り専用
        public string GrowthType { get { return Ratio.Type; } }

        //現在の成長値 - 読み取り専用
        public float Grp { get { return this.grp; } }
        //次のレベルまでに必要な成長値 - 読み取り専用
        public float TargetGrp { get { return 100 * (((float)this.lv / 10) + 1); } }
        //現在のレベル - 読み取り専用
        public int Level { get { return this.lv; } }

        //基本ステータス - 読み取り専用
        public Status BaseStatus { get { return baseStatusWritter; } }
        //戦闘時に変化させるステータス
        public Status BattleStatus { get; set; } = Status.Zero;
        //バフ・デバフ係数
        public Status BattleCoefficient { get; set; } = Status.One;

        //戦闘時の攻撃
        public Command BattleCommand { get; set; } = new Command();

        //スキル
        public Action<Plataberu> Skill { get; set; } = (p) => { };
        //スキルコスト
        public int Cost { get; set; } = 0;

        //戦闘時のステータス初期化
        public void BattleStatusReset()
        {
            this.BattleStatus = this.BaseStatus;
            this.BattleCoefficient = Status.One;
            this.BattleCommand.AllReset();
        }

        //基本ステータスに加算
        public void GrowStatus()
        {
            float coefficient = 1.3f;

            this.baseStatusWritter.ATK += ((coefficient * this.Tier) + this.Level) * (this.Ratio.ATK + 1);
            this.baseStatusWritter.DEF += ((coefficient * this.Tier) + this.Level) * (this.Ratio.DEF + 1);
            this.baseStatusWritter.HP += ((coefficient * this.Tier) + this.Level) * (this.Ratio.HP + 1) * 15;
        }

        //相成長値を加算
        public void AddGrp(float grp)
        {
            totalGrp += Math.Abs(grp);
        }

        //レベルと経験値の処理をする。レベルが上がった場合にfalseを返す
        public int LevelUp()
        {
            int oldLevel = this.lv;
            this.grp = this.TotalGrp;

            for (this.lv = 0; this.grp >= this.TargetGrp; this.lv++)
            {
                this.grp -= this.TargetGrp;

                if (oldLevel < this.lv)
                    GrowStatus();
            }

            return this.lv - oldLevel;
        }

        float comDefense = -1;
        public int BattleMove(Plataberu own, Plataberu enemy, int turn)
        {
            int coms = this.BattleCommand.SelectedCommand[turn];
            Status enemyStatus = enemy.BattleStatus;
            Status enemyCoefficient = enemy.BattleCoefficient;
            Status ownStatus = own.BattleStatus;
            Status ownCoefficient = own.BattleCoefficient;

            if (coms == 0)
            {
                enemyStatus.HP -= Battle.BattleOperater.Attack(own, enemy, 10);
            }
            else if (coms == 1)
            {
                ownCoefficient.DEF *= 1.2f;
                comDefense = ownCoefficient.DEF;
            }
            else if (coms == 2)
            {
                this.Skill(enemy);
            }
            else
            {
                return -1;
            }

            enemy.BattleStatus = enemyStatus;
            enemy.BattleCoefficient = enemyCoefficient;
            own.BattleStatus = ownStatus;
            own.BattleCoefficient = ownCoefficient;

            return coms;
        }

        //複製を生成
        public Plataberu Copy()
        {
            Plataberu copy = new Plataberu();
            copy.Name = this.Name;
            copy.Explanation = this.Explanation;
            copy.Tier = this.Tier;
            copy.AddGrp(this.TotalGrp);
            copy.Ratio = this.Ratio.Copy();
            copy.BattleStatus = this.BattleStatus;
            copy.LevelUp();
            copy.Skill = this.Skill;

            return copy;
        }

        //情報を文字列にする
        public string DebugString()
        {
            return
                $"名前：{this.Name}　　レベル：{this.Level}lv    Tier：{this.Tier}\n" +
                $"成長タイプ：{this.GrowthType}　　成長割合{this.Ratio.DebugString()}\n" +
                $"{this.Explanation}\n" +
                $"総成長値：{this.TotalGrp:###0.00}grp　　成長値：{this.Grp:##0.00}grp　　" +
                $"目標成長値：{this.TargetGrp:##0.00}grp\n" +
                $"基本ステータス：{this.BaseStatus.DebugString()}\n" +
                $"戦闘ステータス：{this.BattleStatus.DebugString()}\n" +
                $"ステータス係数：{this.BattleCoefficient.DebugString()}\n" +
                $"\n[戦闘コマンド]\n{this.BattleCommand.DebugString()}";
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
    public class GrowthRatio
    {
        private Status writeStatus = Status.Zero;

        public float ATK { get { return writeStatus.ATK; } }
        public float DEF { get { return writeStatus.DEF; } }
        public float HP { get { return writeStatus.HP; } }

        public string Type
        {
            get
            {
                return
                    Math.Abs(writeStatus.ATK - writeStatus.DEF) < 2 ? (writeStatus.HP < 5 ? "General" : "Supporter") :
                    (writeStatus.ATK > writeStatus.DEF ? "Attacker" : "Defenser");
            }
        }

        //成長割合を設定する
        public void SetRatio(float atk, float def, float hp)
        {
            float sum = atk + def + hp;

            writeStatus.ATK = (atk / sum) * 10;
            writeStatus.DEF = (def / sum) * 10;
            writeStatus.HP = (hp / sum) * 10;
        }

        //複製を生成
        public GrowthRatio Copy()
        {
            GrowthRatio copy = new GrowthRatio();
            copy.SetRatio(this.ATK, this.DEF, this.HP);
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
        private int atkCost = 0;
        public int AttackCost { get { return atkCost; } }
        //防御時に消費するコスト
        private int defCost = 0;
        public int DefenseCost { get { return defCost; } }
        //スキル発動時に消費するコスト
        private int sklCost = 0;
        public int SkillCost { get { return sklCost; } }

        //カードを5つ抽選する
        public int[] PopCommand { get { return new int[5] { cSet(), cSet(), cSet(), cSet(), cSet() }; } }

        //毎ターン加算されるコスト
        public int MaxCost { get { return 5; } }
        //現在の持ちコスト
        public int Cost { get; set; } = 0;
        
        //毎ターン、プレイヤーが選択したカードを格納するリスト
        public List<int> SelectedCommand { get; set; } = new List<int> ();

        //それぞれのカードのコストを初期化
        public void CostSet(int attackCost, int defenseCost, int skillCost)
        {
            atkCost = attackCost; defCost = defenseCost; sklCost = skillCost;
        }

        private int beforeRandomNum = -1;
        private int cSet()
        {
            var tCost = atkCost + defCost + sklCost;
            var atk = tCost - atkCost;
            var def = tCost - defCost;
            var skl = (tCost - sklCost) / 2;

            var totalCost = atk + def + skl;

            int randNum;
            do
            {
                int[] costs = new int[3] { atkCost, defCost, sklCost };
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
        }

        //コストも初期化する
        public void AllReset()
        {
            this.Cost = MaxCost;
            this.SelectedCommand.Clear();
        }

        //複製を生成
        public Command Copy()
        {
            Command copy = new Command();
            CostSet(this.AttackCost, this.DefenseCost, this.SkillCost);

            return copy;
        }

        public string DebugString()
        {
            return
                $"攻撃コスト：{this.AttackCost}  防御コスト：{this.DefenseCost}  スキルコスト：{this.SkillCost}\n" +
                $"Maxコスト{this.MaxCost}  現在のコスト：{this.Cost}";
        }

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
                    83 * (randomNum < critical ? 1.5f : 1);
            }
        }
    }
}