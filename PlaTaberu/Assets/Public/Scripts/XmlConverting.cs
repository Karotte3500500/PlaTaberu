using GameCharacterManagement;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System;

//XML�ɕϊ�����
namespace XmlConverting
{
    public static class ConvertorXML
    {
        //�����ɗ^����ꂽPlataberu�^�̃I�u�W�F�N�g��XML�t�@�C���̃f�[�^�ɕϊ�
        public static void ConvertXML(Plataberu beru, string path)
        {
            path += ".xml";
            if (!FileExists(path)) File.Create(path);

            //Plataberu�^��ϊ�
            PlataberuData data = new PlataberuData(beru);

            XmlSerializer serialData = new XmlSerializer(typeof(PlataberuData));

            using (StreamWriter sw = new StreamWriter(path, false, new System.Text.UTF8Encoding(false)))
            {
                serialData.Serialize(sw, data);
            }
        }

        //XML�t�@�C���̃f�[�^��Plataberu�^�ɕϊ�
        public static Plataberu ConvertPlataberu(string path)
        {
            path += ".xml";
            if (!FileExists(path)) File.Create(path);

            PlataberuData data;

            XmlSerializer serialData = new XmlSerializer(typeof(PlataberuData));

            using (StreamReader sr = new StreamReader(path))
            {
                data = (PlataberuData)serialData.Deserialize(sr);
            }
            return data.ToPlataberu();
        }

        //XML�t�@�C���Ƀo�g���f�[�^�A���t�@��ۑ�
        public static void SerializeBattleDataAlpha(Plataberu friend, Plataberu enemy, string path)
        {
            path += ".xml";
            if (!FileExists(path)) File.Create(path);

            //Plataberu�^��BattleDataAlpha�^�֕ϊ�
            BattleDataAlpha battleData = new BattleDataAlpha(friend, enemy);
            XmlSerializer serialData = new XmlSerializer(typeof(BattleDataAlpha));

            using (StreamWriter sw = new StreamWriter(path, false, new System.Text.UTF8Encoding(false)))
            {
                serialData.Serialize(sw, battleData);
            }
        }
        //XML�t�@�C���Ƀo�g���f�[�^�x�[�^��ۑ�
        public static void SerializeBattleDataBeta(Plataberu beru, string path)
        {
            path += ".xml";
            if (!FileExists(path)) File.Create(path);

            BattleDataBeta battleData = new BattleDataBeta(beru);
            XmlSerializer serialData = new XmlSerializer(typeof(BattleDataBeta));

            using (StreamWriter sw = new StreamWriter(path, false, new System.Text.UTF8Encoding(false)))
            {
                serialData.Serialize(sw, battleData);
            }
        }

        //XML�t�@�C���̃f�[�^��BattleDataAlpha�^�ɕϊ�
        public static BattleDataAlpha DeserializeBattleDataAlpha(string path)
        {
            path += ".xml";
            if (!FileExists(path)) return null;

            BattleDataAlpha data;

            XmlSerializer serialData = new XmlSerializer(typeof(BattleDataAlpha));

            using (StreamReader sr = new StreamReader(path))
            {
                data = (BattleDataAlpha)serialData.Deserialize(sr);
            }
            return data;
        }

        //XML�t�@�C���̃f�[�^��BattleDataBeta�^�ɕϊ�
        public static BattleDataBeta DeserializeBattleDataBeta(string path)
        {
            path += ".xml";
            if (!FileExists(path)) return null;

            BattleDataBeta data;

            XmlSerializer serialData = new XmlSerializer(typeof(BattleDataBeta));

            using (StreamReader sr = new StreamReader(path))
            {
                data = (BattleDataBeta)serialData.Deserialize(sr);
            }
            return data;
        }

        public static Status ReadPlasticsData(string path)
        {
            Status plas = new Status();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load((path + ".xml"));

            XmlNode pictureInfoNode = xmlDoc.SelectSingleNode("Pictureinformation");

            XmlNode redObjectsNode = pictureInfoNode["RedObjects"];
            XmlNode blueObjectsNode = pictureInfoNode["BlueObjects"];
            XmlNode greenObjectsNode = pictureInfoNode["GreenObjects"];

            plas.ATK = int.Parse(redObjectsNode.InnerText);
            plas.DEF = int.Parse(blueObjectsNode.InnerText);
            plas.HP = int.Parse(greenObjectsNode.InnerText);

            return plas;
        }

        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);

        }
    }

    public class PlataberuData
    {
        public int ID { get; set; }
        public string NickName { get; set; }
        public float Grp { get; set; }
        //�v���X�`�b�N���i�[
        public float[] Plastics { get; set; } = new float[3];
        //�A�C�e�����i�[
        public int[] Items { get; set; }
        //�ŗL�ԍ�
        public int[] IdentificationNumbers { get; set; } = new int[3];

        //�V���A���C�Y�E�f�V���A���C�Y�p
        public PlataberuData() { }

        public PlataberuData(Plataberu beru)
        {
            this.SetData(beru);
        }

        //Plataberu�^��PlataberuData�^�ɕϊ�
        public void SetData(Plataberu beru)
        {
            //�e�f�[�^���i�[
            this.ID = beru.ID;
            this.NickName = beru.NickName;
            this.Grp = beru.TotalGrp - (beru.Plastics.ATK + beru.Plastics.DEF + beru.Plastics.HP);
            this.Plastics = beru.Plastics.ToArray();
            this.IdentificationNumbers = beru.IdentificationNumbers;

            //�A�C�e����ϊ�
            this.Items = new int[beru.ItemSlot.Length];
            int index = 0;
            foreach (var item in beru.ItemSlot)
            {
                this.Items[index] = item == null ? -1 : item.ID;
                index++;
            }
        }

        //PlataberuData�^��Plataberu�^�ɕϊ�
        public Plataberu ToPlataberu()
        {
            //ID����Plataberu�^���擾
            Plataberu beru = PlataberuManager.GetPlataberu(this.ID);
            beru.NickName = this.NickName;
            beru.AddGrp(this.Grp);
            beru.GetPlastic(Status.IntoStatus(this.Plastics));
            beru.LevelUp();

            //�A�C�e�����擾
            int len = beru.ItemSlot.Length;
            for (int i = 0; i < len; i++)
            {
                beru.ItemSlot[i] = this.Items[i] == -1 ? null : PlataberuManager.GetItem(this.Items[i]);
            }

            return beru;
        }
    }

    public class BattleDataAlpha
    {
        // �I�������R�}���h
        public List<int> SelectedCommand { get; set; } = new List<int>();
        // ��_���[�W      ��Enemy,Friend�͓ǂݎ��ۂ̎��_�Ŗ���
        public List<float> FriendsDamagesSuffered { get; set; }
        public List<float> EnemysDamagesSuffered { get; set; }
        // �^�_���[�W      ��Enemy,Friend�͓ǂݎ��ۂ̎��_�Ŗ���
        public List<float> FriendsDamagesInflicted { get; set; }
        public List<float> EnemysDamagesInflicted { get; set; }
        // �o�g���X�e�[�^�X�𑗐M      ��Enemy,Friend�͓ǂݎ��ۂ̎��_�Ŗ���
        public float[] FriendsBattleStatus { get; set; } = new float[3];
        public float[] EnemysBattleStatus { get; set; } = new float[3];

        public BattleDataAlpha() { }

        public BattleDataAlpha(Plataberu friend, Plataberu enemy)
        {
            //Enemy,Friend�͓ǂݎ��ۂɂ��킹�Ċi�[����
            this.SelectedCommand = friend.BattleCommand.SelectedCommand;

            this.FriendsDamagesSuffered = enemy.DamagesSuffered;
            this.EnemysDamagesSuffered = friend.DamagesSuffered;

            this.FriendsDamagesInflicted = enemy.DamagesInflicted;
            this.EnemysDamagesInflicted = friend.DamagesInflicted;

            this.FriendsBattleStatus = enemy.BattleStatus.ToArray();
            this.EnemysBattleStatus = friend.BattleStatus.ToArray();
        }

        public (Plataberu Friend, Plataberu Enemy) WriteData(Plataberu friend, Plataberu enemy)
        {
            enemy.BattleCommand.SelectedCommand = this.SelectedCommand;

            friend.DamagesSuffered = this.FriendsDamagesSuffered;
            friend.DamagesInflicted = this.FriendsDamagesInflicted;
            friend.BattleStatus = Status.IntoStatus(this.FriendsBattleStatus);

            enemy.DamagesSuffered = this.EnemysDamagesSuffered;
            enemy.DamagesInflicted = this.EnemysDamagesInflicted;
            enemy.BattleStatus = Status.IntoStatus(this.EnemysBattleStatus);

            return (friend, enemy);
        }
    }

    public class BattleDataBeta
    {
        //�I�������R�}���h
        public List<int> SelectedCommand { get; set; }

        public BattleDataBeta() { }
        public BattleDataBeta(Plataberu beru)
        {
            this.SelectedCommand = beru.BattleCommand.SelectedCommand;
        }

        public Plataberu WriteData(Plataberu beru)
        {
            beru.BattleCommand.SelectedCommand = this.SelectedCommand;
            return beru;
        }
    }
}