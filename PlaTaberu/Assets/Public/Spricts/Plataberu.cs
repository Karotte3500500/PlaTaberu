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

        //�v���^�x���̖��O
        public string Name { get; set; } = "�x��";
        //�v���^�x���̐���
        public string Explanation { get; set; } = "�v���X�`�b�N��H�ׂĐ�������s�v�c�Ȑ�����";
        //�v���^�x���̕]��(1~3)�ʏ��tier�ƈقȂ萔�l���傫���قǋ���
        public int Tier { get; set; }
        //�v���^�x���̑������l - �ǂݎ���p
        public float TotalGrp { get { return totalGrp; } }
        //�v���^�x���̐�������
        public GrowthRatio Ratio { get; set; } = new GrowthRatio();
        //�v���^�x���̐����^�C�v - �ǂݎ���p
        public string GrowthType { get { return Ratio.Type; } }

        //���݂̐����l - �ǂݎ���p
        public float Grp { get { return this.grp; } }
        //���̃��x���܂łɕK�v�Ȑ����l - �ǂݎ���p
        public float TargetGrp { get { return 100 * (((float)this.lv / 10) + 1); } }
        //���݂̃��x�� - �ǂݎ���p
        public int Level { get { return this.lv; } }

        //��{�X�e�[�^�X - �ǂݎ���p
        public Status BaseStatus { get { return baseStatusWritter; } }
        //�퓬���ɕω�������X�e�[�^�X
        public Status BattleStatus { get; set; } = Status.Zero;
        //�o�t�E�f�o�t�W��
        public Status BattleCoefficient { get; set; } = Status.One;

        //�퓬���̍U��
        public Command BattleCommand { get; set; } = new Command();

        //�X�L��
        public Action<Plataberu> Skill { get; set; } = (p) => { };
        //�X�L���R�X�g
        public int Cost { get; set; } = 0;

        //�퓬���̃X�e�[�^�X������
        public void BattleStatusReset()
        {
            this.BattleStatus = this.BaseStatus;
            this.BattleCoefficient = Status.One;
            this.BattleCommand.AllReset();
        }

        //��{�X�e�[�^�X�ɉ��Z
        public void GrowStatus()
        {
            float coefficient = 1.3f;

            this.baseStatusWritter.ATK += ((coefficient * this.Tier) + this.Level) * (this.Ratio.ATK + 1);
            this.baseStatusWritter.DEF += ((coefficient * this.Tier) + this.Level) * (this.Ratio.DEF + 1);
            this.baseStatusWritter.HP += ((coefficient * this.Tier) + this.Level) * (this.Ratio.HP + 1) * 15;
        }

        //�������l�����Z
        public void AddGrp(float grp)
        {
            totalGrp += Math.Abs(grp);
        }

        //���x���ƌo���l�̏���������B���x�����オ�����ꍇ��false��Ԃ�
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

        //�����𐶐�
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

        //���𕶎���ɂ���
        public string DebugString()
        {
            return
                $"���O�F{this.Name}�@�@���x���F{this.Level}lv    Tier�F{this.Tier}\n" +
                $"�����^�C�v�F{this.GrowthType}�@�@��������{this.Ratio.DebugString()}\n" +
                $"{this.Explanation}\n" +
                $"�������l�F{this.TotalGrp:###0.00}grp�@�@�����l�F{this.Grp:##0.00}grp�@�@" +
                $"�ڕW�����l�F{this.TargetGrp:##0.00}grp\n" +
                $"��{�X�e�[�^�X�F{this.BaseStatus.DebugString()}\n" +
                $"�퓬�X�e�[�^�X�F{this.BattleStatus.DebugString()}\n" +
                $"�X�e�[�^�X�W���F{this.BattleCoefficient.DebugString()}\n" +
                $"\n[�퓬�R�}���h]\n{this.BattleCommand.DebugString()}";
        }
    }

    //�U���́E�h��́E�̗͂��i�[����\����
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
            return $"ATK�F{this.ATK:##0.00}�@�@DEF�F{this.DEF:##0.00}�@�@HP�F{this.HP:##0.00}";
        }

        //�[���N���A�p
        public static Status Zero { get { return new Status(0, 0, 0); } }
        //�����N���A�p
        public static Status One { get { return new Status(1, 1, 1); } }
    }

    //�X�e�[�^�X�̐����������Ǘ�����N���X
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

        //����������ݒ肷��
        public void SetRatio(float atk, float def, float hp)
        {
            float sum = atk + def + hp;

            writeStatus.ATK = (atk / sum) * 10;
            writeStatus.DEF = (def / sum) * 10;
            writeStatus.HP = (hp / sum) * 10;
        }

        //�����𐶐�
        public GrowthRatio Copy()
        {
            GrowthRatio copy = new GrowthRatio();
            copy.SetRatio(this.ATK, this.DEF, this.HP);
            return copy;
        }

        //���𕶎���ɂ���
        public string DebugString()
        {
            return $"A�F{this.ATK} D�F{this.DEF} H�F{this.HP}";
        }
    }

    public class Command
    {
        //�U�����ɏ����R�X�g
        private int atkCost = 0;
        public int AttackCost { get { return atkCost; } }
        //�h�䎞�ɏ����R�X�g
        private int defCost = 0;
        public int DefenseCost { get { return defCost; } }
        //�X�L���������ɏ����R�X�g
        private int sklCost = 0;
        public int SkillCost { get { return sklCost; } }

        //�J�[�h��5���I����
        public int[] PopCommand { get { return new int[5] { cSet(), cSet(), cSet(), cSet(), cSet() }; } }

        //���^�[�����Z�����R�X�g
        public int MaxCost { get { return 5; } }
        //���݂̎����R�X�g
        public int Cost { get; set; } = 0;
        
        //���^�[���A�v���C���[���I�������J�[�h���i�[���郊�X�g
        public List<int> SelectedCommand { get; set; } = new List<int> ();

        //���ꂼ��̃J�[�h�̃R�X�g��������
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

        //�^�[���I������Command�����Z�b�g���郁�\�b�h
        public void Reset()
        {
            this.Cost += MaxCost;
            this.SelectedCommand.Clear();
        }

        //�R�X�g������������
        public void AllReset()
        {
            this.Cost = MaxCost;
            this.SelectedCommand.Clear();
        }

        //�����𐶐�
        public Command Copy()
        {
            Command copy = new Command();
            CostSet(this.AttackCost, this.DefenseCost, this.SkillCost);

            return copy;
        }

        public string DebugString()
        {
            return
                $"�U���R�X�g�F{this.AttackCost}  �h��R�X�g�F{this.DefenseCost}  �X�L���R�X�g�F{this.SkillCost}\n" +
                $"Max�R�X�g{this.MaxCost}  ���݂̃R�X�g�F{this.Cost}";
        }

    }

    namespace Battle
    {
        static class BattleOperater
        {
            //�N���e�B�J��
            //�h��

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