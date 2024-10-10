using GameCharacterManagement;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

//XMLに変換する
namespace XmlConverting
{
    public static class ConvertorXML
    {
        //引数に与えられたPlataberu型のオブジェクトをXMLファイルのデータに変換
        public static void ConvertXML(Plataberu beru, string path)
        {
            path += ".xml";
            if (!FileExists(path)) File.Create(path);

            //Plataberu型を変換
            PlataberuData data = new PlataberuData(beru);

            XmlSerializer serialData = new XmlSerializer(typeof(PlataberuData));

            using (StreamWriter sw = new StreamWriter(path, false, new System.Text.UTF8Encoding(false)))
            {
                serialData.Serialize(sw, data);
            }
        }

        //XMLファイルのデータをPlataberu型に変換
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

        //XMLファイルにバトルデータアルファを保存
        public static void SerializeBattleDataAlpha(Plataberu beru, string path)
        {
            path += ".xml";
            if (!FileExists(path)) File.Create(path);

            //Plataberu型をBattleD
            BattleDataAlpha battleData = new BattleDataAlpha(beru);
            XmlSerializer serialData = new XmlSerializer(typeof(BattleDataAlpha));

            using (StreamWriter sw = new StreamWriter(path, false, new System.Text.UTF8Encoding(false)))
            {
                serialData.Serialize(sw, battleData);
            }
        }
        //XMLファイルにバトルデータベータを保存
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

        //XMLファイルのデータをBattleDataAlpha型に変換
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

        //XMLファイルのデータをBattleDataBeta型に変換
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
        //プラスチックを格納
        public float[] Plastics { get; set; } = new float[3];
        //アイテムを格納
        public int[] Items { get; set; }
        //固有番号
        public int[] IdentificationNumbers { get; set; } = new int[3];

        //シリアライズ・デシリアライズ用
        public PlataberuData() { }

        public PlataberuData(Plataberu beru)
        {
            this.SetData(beru);
        }

        //Plataberu型をPlataberuData型に変換
        public void SetData(Plataberu beru)
        {
            //各データを格納
            this.ID = beru.ID;
            this.NickName = beru.NickName;
            this.Grp = beru.TotalGrp - (beru.Plastics.ATK + beru.Plastics.DEF + beru.Plastics.HP);
            this.Plastics = beru.Plastics.ToArray();
            this.IdentificationNumbers = beru.IdentificationNumbers;

            //アイテムを変換
            this.Items = new int[beru.ItemSlot.Length];
            int index = 0;
            foreach (var item in beru.ItemSlot)
            {
                this.Items[index] = item == null ? -1 : item.ID;
                index++;
            }
        }

        //PlataberuData型をPlataberu型に変換
        public Plataberu ToPlataberu()
        {
            //IDからPlataberu型を取得
            Plataberu beru = PlataberuManager.GetPlataberu(this.ID);
            beru.NickName = this.NickName;
            beru.AddGrp(this.Grp);
            beru.GetPlastic(Status.IntoStatus(this.Plastics));
            beru.LevelUp();

            //アイテムを取得
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
        //選択したコマンド
        public List<int> SelectedCommand { get; set; }
        //被ダメージ
        public List<float> DamagesSuffered { get; set; } = new List<float>();
        //与ダメージ
        public List<float> DamagesInflicted { get; set; } = new List<float>();
        //バトルステータスを送信
        public float[] BattleStatus { get; set; } = new float[3];

        public BattleDataAlpha() { }

        public BattleDataAlpha(Plataberu beru)
        {
            this.SelectedCommand = beru.BattleCommand.SelectedCommand;
            this.DamagesSuffered = beru.DamagesSuffered;
            this.DamagesInflicted = beru.DamagesInflicted;
            this.BattleStatus = beru.BattleStatus.ToArray();
        }
    }

    public class BattleDataBeta
    {
        //選択したコマンド
        public List<int> SelectedCommand { get; set; }

        public BattleDataBeta() { }
        public BattleDataBeta(Plataberu beru)
        {
            this.SelectedCommand = beru.BattleCommand.SelectedCommand;
        }
    }
}