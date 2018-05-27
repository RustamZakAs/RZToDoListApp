using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace RZToDoListApp
{
    interface IRZSaveLoad
    {
        void Save(object RZData, string filename);
        object Load(string filename, int type);
    }
    class RZJson : IRZSaveLoad
    {
        public void Save(object RZData, string filename)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(RZData.GetType()/*typeof(List<RZAccount>)*/);
            using (FileStream fs = new FileStream($"{filename}.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, RZData);
            }
        }
        public object Load(string filename, int type)
        {
            object RZData = null;
            if (type == 1)
            {
                RZData = new List<RZAccount>();
            }
            if (type == 2)
            {
                RZData = new List<RZTasks>();
            }
            using (FileStream fs = new FileStream($"{filename}.json", FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(RZData.GetType()/*typeof(List<RZAccount>)*/);
                if (fs.Length != 0) RZData = jsonFormatter.ReadObject(fs);
            }
            return RZData;
        }
        //public static void RZShowList(ref List<RZAccount> RZAccountsList)
        //{
        //    foreach (RZAccount p in RZAccountsList)
        //    {
        //        Console.WriteLine("Имя: {0} --- \n" +
        //            "Фамилия: {1} --- \n" +
        //            "Возраст: {2}", p.RZName, p.RZSurname, p.RZAge);
        //    }
        //}
    }
    class BinarySaver //: IRZSaveLoad
    {
        public void Save(object data, string filename)
        {
            FileStream fs = new FileStream($"{filename}.bin", FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, data);
            fs.Close();
        }

        public object Load(string filename)
        {
            FileStream fs = new FileStream($"{filename}.bin", FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            var data = bf.Deserialize(fs);
            fs.Close();
            return data;
        }
    }

    class XmlSaver// : IRZSaveLoad
    {
        public void Save(object data, string filename)
        {
            FileStream fs = new FileStream($"{filename}.xml", FileMode.OpenOrCreate);
            XmlSerializer xs = new XmlSerializer(data.GetType());
            xs.Serialize(fs, data);
            fs.Close();
        }

        public object Load(string filename)
        {
            FileStream fs = new FileStream($"{filename}.xml", FileMode.OpenOrCreate);
            XmlSerializer xs = new XmlSerializer(typeof(object));
            var data = xs.Deserialize(fs);
            fs.Close();
            return data;
        }
    }
}
