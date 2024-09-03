using GameCharacterManagement;
using GameCharacterManagement.Battle;
using System;
using System.Collections.Generic;

namespace GameCharacterManagement
{
    //�x���̐ݒ�***************************************************************************************
    public class Belu : Plataberu
    {
        public override int ID => 1;
        public override string Name => "�x��";
        public override string Explanation => "�v���^�x���̎q���A�v���X�`�b�N��H�ׂĈ�� ";
        public override int Tier => 1;
        public override Ratio GrowthRatio => new Ratio(1.0f, 1.0f, 1.0f);
        public override Command BattleCommand { get; set; } = new Command(4, 4, 0);
        public override int NextLevel => 15;

        public override Item[] ItemSlot { get; set; } = new Item[3];

        public override string SkillName => "�Ȃɂ����Ȃ�";
        public override string SkillExplanation => "�Ȃɂ����Ȃ�";

        public override void Skill(Plataberu enemy)
        {
            base.Skill(enemy);
        }
    }

    //�P�C�̐ݒ�***************************************************************************************
    public class Kei : Plataberu
    {
        public override int ID => 2;
        public override string Name => "�P�C";
        public override string Explanation => "���R�̒��ň�����v���^�x���B�Q��ŕ�炷�B";
        public override int Tier => 2;
        public override Ratio GrowthRatio => new Ratio(2.5f, 1.0f, 1.5f);
        public override Command BattleCommand { get; set; } = new Command(3, 4, 2);
        public override int NextLevel => 30;

        public override Item[] ItemSlot { get; set; } = new Item[3];

        public override string SkillName => "�ق���";
        public override string SkillExplanation => "����̖h��͂�������";

        public override void Skill(Plataberu enemy)
        {
            Status cof = enemy.BattleCoefficient;
            cof.DEF *= 0.8f;
            enemy.BattleCoefficient = cof;
        }
    }

    //�o�n���[�g�̐ݒ�***************************************************************************************
    public class Vaha : Plataberu
    {
        public override int ID => 4;
        public override string Name => "�o�n���[�g";
        public override string Explanation => "���ŋ���сA�΂𐁂��B";
        public override int Tier => 3;
        public override Ratio GrowthRatio => new Ratio(3.5f, 2.0f, 1.5f);
        public override Command BattleCommand { get; set; } = new Command(2, 3, 10);
        public override int NextLevel => 1000;

        public override Item[] ItemSlot { get; set; } = new Item[1];

        public override string SkillName => "�J�^�X�g���t";
        public override string SkillExplanation => "�h��ђʃ_���[�W";

        public override void Skill(Plataberu enemy)
        {
            Status st = enemy.BattleStatus;
            st.HP -= this.AttackThrough(enemy, this.BattleCritical) * 1.0f;
            enemy.BattleStatus = st;
        }
    }

    //�R�i�̐ݒ�***************************************************************************************
    public class Cona : Plataberu
    {
        public override int ID => 5;
        public override string Name => "�R�i";
        public override string Explanation => "������̖�����\�m���邱�Ƃ��ł���B ";
        public override int Tier => 3;
        public override Ratio GrowthRatio => new Ratio(1f, 2f, 1.5f);
        public override Command BattleCommand { get; set; } = new Command(2, 3, 3);
        public override int NextLevel => 1000;

        public override Item[] ItemSlot { get; set; } = new Item[3];

        public override int BaseCritical => 20;
        public override string SkillName => "�^�o�Ë�";
        public override string SkillExplanation => "���̍U�����K���N���e�B�J���ɂȂ�A�G�̖h��͂�1�^�[����_�E��";

        public override void Skill(Plataberu enemy)
        {
            this.BattleCritical = 100;
            Status tCof = enemy.TemporaryCoefficient;
            tCof.DEF *= 0.5f;
            enemy.TemporaryCoefficient = tCof;
        }
    }

    //�����[�̐ݒ�***************************************************************************************
    public class Lily : Plataberu
    {
        public override int ID => 6;
        public override string Name => "�����[";
        public override string Explanation => "�l�����������i�B�G��Ƃӂ�ӂ킵�Ă���B";
        public override int Tier => 3;
        public override Ratio GrowthRatio => new Ratio(1f, 1.5f, 4f);
        public override Command BattleCommand { get; set; } = new Command(3, 4, 5);
        public override int NextLevel => 1000;

        public override Item[] ItemSlot { get; set; } = new Item[2];

        public override string SkillName => "�_�C�������h�����[";
        public override string SkillExplanation => "�G�̎��̍U���𔽎�";

        public override void Skill(Plataberu enemy)
        {
            //�ق�イ
            enemy.Skill(enemy);
        }
    }

    //�j�i�̐ݒ�***************************************************************************************
    public class Nina : Plataberu
    {
        public override int ID => 3;
        public override string Name => "�j�i";
        public override string Explanation => "�l�ƈ�����v���^�x���B���t��b�����Ƃ��ł���B ";
        public override int Tier => 2;
        public override Ratio GrowthRatio => new Ratio(1.5f, 1.5f, 2f);
        public override Command BattleCommand { get; set; } = new Command(4, 3, 5);
        public override int NextLevel => 30;

        public override Item[] ItemSlot { get; set; } = new Item[3];

        public override string SkillName => "�x��";
        public override string SkillExplanation => "HP����bHP�̈ꊄ��";

        public override void Skill(Plataberu enemy)
        {
            Status mySt = this.BattleStatus;
            mySt.HP += this.BaseStatus.HP / 10.0f;
            this.BattleStatus = mySt;
        }
    }

    //�f�������_���̐ݒ�***************************************************************************************
    public class Dhura : Plataberu
    {
        public override int ID => 7;
        public override string Name => "�f�������_��";
        public override string Explanation => "�u�����v�̓�������v���^�x���B���`�����ӂ�鐫�i�B";
        public override int Tier => 3;
        public override Ratio GrowthRatio => new Ratio(2.5f, 2.5f, 2f);
        public override Command BattleCommand { get; set; } = new Command(2, 3, 6);
        public override int NextLevel => 1000;

        public override Item[] ItemSlot { get; set; } = new Item[2];

        public override string SkillName => "���[�����E�I�������h";
        public override string SkillExplanation => "�G�ɍU�����A�h��͂��㏸";

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

    //�G���U�x�[�g�̐ݒ�***************************************************************************************
    public class Eri : Plataberu
    {
        public override int ID => 8;
        public override string Name => "�G���U�x�[�g";
        public override string Explanation => "�Ԃ��v���X�`�b�N���D���ȃv���^�x���B���z�����B�C���h�A�h�B";
        public override int Tier => 3;
        public override Ratio GrowthRatio => new Ratio(2.5f, 1.0f, 3f);
        public override Command BattleCommand { get; set; } = new Command(2, 2, 4);
        public override int NextLevel => 1000;

        public override Item[] ItemSlot { get; set; } = new Item[2];

        public override string SkillName => "�������E�J�[�~��";
        public override string SkillExplanation => "�G�Ƀ_���[�W��^���A�_���[�W�ɔ�Ⴕ�Ď��g�i���M�j���񕜂���";

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

    //�I�[�f�B���̐ݒ�***************************************************************************************
    public class Odin : Plataberu
    {
        public override int ID => 9;
        public override string Name => "�I�[�f�B��";
        public override string Explanation => "�n�ɏ��A��n���삯�A���œG���h���т��B";
        public override int Tier => 3;
        public override Ratio GrowthRatio { get { return new Ratio(3.0f, 2.5f, 2f); } }
        public override Command BattleCommand { get; set; } = new Command(2, 3, 4);
        public override int NextLevel => 1000;

        public override Item[] ItemSlot { get; set; } = new Item[1];

        public override string SkillName => "���H�[�^���E�O���O�j��";
        public override string SkillExplanation => "�U�����A�G�̖h����_�E��";

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

    //�v���^�x���̒�`***************************************************************************************
    public class Plataberu
    {
        public virtual int ID { get { return 0; } }
        //�v���^�x���̖��O
        public virtual string Name { get { return "�x��"; } }
        //�v���^�x���̐���
        public virtual string Explanation { get { return "�v���X�`�b�N��H�ׂĐ�������s�v�c�Ȑ�����"; } }
        //�v���^�x���̕]��(1~3)�ʏ��tier�ƈقȂ萔�l���傫���قǋ���
        public virtual int Tier { get { return 1; } }
        //�v���^�x���̑������l - �ύX�֎~
        public float TotalGrp { get; private set; }
        //�v���^�x���̐�������
        public virtual Ratio GrowthRatio { get { return new Ratio(1, 1, 1); } }
        //�v���^�x���̐����^�C�v - �ύX�֎~
        public string GrowthType { get { return GrowthRatio.Type; } }

        /*���܂œ����v���X�`�b�N�̎��(ATK�F��,DEF�F��,HP�F��)*/
        public Status Plastics { get; private set; } = Status.Zero;

        //���݂̐����l - �ύX�֎~
        public float Grp { get; private set; }
        //���̃��x���܂łɕK�v�Ȑ����l - �ύX�֎~
        public float TargetGrp { get { return 100 * (((float)this.Level / 10) + 1); } }
        //���݂̐����l�̊����iGRP�o�[�p�j
        public float GrpRatio { get { return Grp / TargetGrp; } }
        //�O�̃��x��
        public int OldLevel { get; private set; }
        //���݂̃��x�� - �ύX�֎~
        public int Level { get; private set; }
        //�����ɕK�v�ȃ��x��
        public virtual int NextLevel { get { return 15; } }

        //�A�C�e���X���b�g
        public virtual Item[] ItemSlot { get; set; } = new Item[0];

        //��{�X�e�[�^�X - �ύX�֎~
        public Status BaseStatus { get; private set; } = Status.Zero;
        //���ۂ̃X�e�[�^�X
        public Status ActualStatus { get; private set; } = Status.Zero;
        //�퓬���ɕω�������X�e�[�^�X
        public Status BattleStatus { get; set; } = Status.Zero;
        //�i���I�ȃo�t�E�f�o�t�W��
        public Status BattleCoefficient { get; set; } = Status.One;
        //1�^�[���̂݌��ʓI�ȃo�t�E�f�o�t�W��
        public Status TemporaryCoefficient { get; set; } = Status.One;

        //��b�N���e�B�J����
        public virtual int BaseCritical { get { return 10; } }
        //�퓬���N���e�B�J����
        public int BattleCritical { get; set; }

        //�퓬���̍U��
        public virtual Command BattleCommand { get; set; } = new Command(1, 1, 1);
        //��_���[�W
        public List<float> DamagesSuffered { get; set; } = new List<float>();
        //��_���[�W
        public List<float> DamagesInflicted { get; set; } = new List<float>();

        //�X�L��
        public virtual void Skill(Plataberu enemy) { }
        //�X�L����
        public virtual string SkillName { get { return "-"; } }
        //�X�L���̐���
        public virtual string SkillExplanation { get { return "-"; } }

        //�퓬���̃X�e�[�^�X������
        public void BattleStatusReset()
        {
            this.BattleStatus = this.ActualStatus;
            this.BattleCoefficient = Status.One;
            this.BattleCritical = this.BaseCritical;
            this.BattleCommand.AllReset();
        }

        //��{�X�e�[�^�X�ɉ��Z
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

        //�������l�����Z
        public void AddGrp(float grp)
        {
            this.TotalGrp += Math.Abs(grp);
        }

        //���x���ƌo���l�̏���������B�オ�������x����Ԃ�
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

        //�퓬���̓���
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

        //�ʏ�U��
        public float Attack(Plataberu defenser, int critical)
        {
            System.Random random = new System.Random();
            int randomNum = random.Next(0, 100);

            return
                ((this.BattleStatus.ATK * this.BattleCoefficient.ATK) /
                ((defenser.BattleStatus.DEF * defenser.BattleCoefficient.DEF) / 30 + 1)) *
                83 * (randomNum < SetCritical(critical) ? 1.5f : 1) * (defenser.BattleCommand.isDfensing ? 0.5f : 1.0f) * 2;
        }
        //�ђʍU��
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

        //�����𐶐�
        public Plataberu Copy()
        {
            Plataberu copy = new Plataberu();

            copy.AddGrp(this.TotalGrp);
            copy.Plastics = this.Plastics;
            copy.BattleStatus = this.BattleStatus;
            copy.LevelUp();

            return copy;
        }

        //���𕶎���ɂ���
        public string DebugString()
        {

            string damages = "�^�_���[�W�F";
            foreach (var dama in this.DamagesInflicted)
                damages += $" {dama:0.00} ,";
            damages += "\n��_���[�W�F";
            foreach (var dama in this.DamagesSuffered)
                damages += $" {dama:0.00} ,";

            string items = "�A�C�e���F";
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
                $"���O�F{this.Name}�@�@���x���F{this.Level}lv    Tier�F{this.Tier}\n" +
                $"�����^�C�v�F{this.GrowthType}�@�@��������{this.GrowthRatio.DebugString()}\n" +
                $"{this.Explanation}\n" +
                $"�������l�F{this.TotalGrp:###0.00}grp�@�@�����l�F{this.Grp:##0.00}grp�@�@" +
                $"�ڕW�����l�F{this.TargetGrp:##0.00}grp\n" +
                $"��������v���X�`�b�N�F{this.Plastics.DebugString()}\n" +
                $"��{�X�e�[�^�X�F{this.BaseStatus.DebugString()}\n" +
                $"�����X�e�[�^�X�F{this.ActualStatus.DebugString()}\n" +
                $"�퓬�X�e�[�^�X�F{this.BattleStatus.DebugString()}\n" +
                $"�X�e�[�^�X�W���F{this.BattleCoefficient.DebugString()}\n" +
                $"\n[�퓬�R�}���h]\n{this.BattleCommand.DebugString()}\n" +
                items + "\n" +
                $"�X�L�����F�u{this.SkillName}�v\n" + damages;
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

        public Status Add(Status status)
        {
            this.ATK += status.ATK;
            this.DEF += status.DEF;
            this.HP += status.HP;
            return this;
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

        //�����𐶐�
        public Ratio Copy()
        {
            Ratio copy = new Ratio(this.ATK, this.DEF, this.HP);
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
        public int AttackCost { get; private set; }
        //�h�䎞�ɏ����R�X�g
        public int DefenseCost { get; private set; }
        //�X�L���������ɏ����R�X�g
        public int SkillCost { get; private set; }
        //�h�䂵�����ǂ���
        public bool isDfensing { get; set; } = false;

        //�J�[�h��5���I����
        public int[] PopCommand { get { return new int[5] { cSet(), cSet(), cSet(), cSet(), cSet() }; } }

        //���^�[�����Z�����R�X�g
        public int MaxCost { get { return 5; } }
        //���݂̎����R�X�g
        public int Cost { get; set; } = 0;

        //���^�[���A�v���C���[���I�������J�[�h���i�[���郊�X�g
        public List<int> SelectedCommand { get; set; } = new List<int>();

        public Command(int attackCost, int defenseCost, int skillCost)
        {
            this.AttackCost = attackCost; this.DefenseCost = defenseCost; this.SkillCost = skillCost;
        }

        //���ꂼ��̃J�[�h�̃R�X�g��������
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

        //�^�[���I������Command�����Z�b�g���郁�\�b�h
        public void Reset()
        {
            this.Cost += MaxCost;
            this.SelectedCommand.Clear();
            this.isDfensing = false;
        }

        //�R�X�g������������
        public void AllReset()
        {
            this.Cost = MaxCost;
            this.SelectedCommand.Clear();
            this.isDfensing = false;
        }

        //�����𐶐�
        public Command Copy()
        {
            Command copy = new Command(this.AttackCost, this.DefenseCost, this.SkillCost);

            return copy;
        }

        public string DebugString()
        {
            return
                $"�U���R�X�g�F{this.AttackCost}  �h��R�X�g�F{this.DefenseCost}  �X�L���R�X�g�F{this.SkillCost}\n" +
                $"Max�R�X�g{this.MaxCost}  ���݂̃R�X�g�F{this.Cost}";
        }

    }


    /*�A�C�e��*/
    public class Item
    {
        public virtual string Name { get { return "�A�C�e��"; } }
        public virtual int ID { get { return 0; } }
        public virtual string Explanation { get { return "���낢�낷�������"; } }
    }

    public class Shield : Item
    {
        public override string Name => "�V�[���h";
        public override int ID => 1;
        public override string Explanation => "�U����h����";
    }

    public class Glasses : Item
    {
        public override string Name => "�ዾ";
        public override int ID => 2;
        public override string Explanation => "�N���e�B�J�������グ��ዾ";
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