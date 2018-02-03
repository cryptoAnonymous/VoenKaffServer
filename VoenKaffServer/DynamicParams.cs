using System;
using System.IO;
using System.Xml.Serialization;

namespace VoenKaffServer
{
    public class DynamicParams
    {
        string Path = "settingsServer.ini"; //Имя файла.

        // С помощью конструктора записываем пусть до файла и его имя.
        public DynamicParams()
        {
            if (!File.Exists(Path))
            {
                File.Create(Path);
            }
        }

        public class Settings
        {
            public string TestPath { get; set; } = "";
            public int Port { get; set; } = 0;
            public string IpAdress { get; set; } = "127.0.0.1";
            public string ResultsPath { get; set; } = "";
        }


        //Читаем ini-файл и возвращаем значение указного ключа из заданной секции.
        public Settings Get()
        {
            return ReadFile();
        }

        private Settings ReadFile()
        {
            try
            {
                using (var stream = new FileStream(Path, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    var iniSet = (Settings)serializer.Deserialize(stream);
                    return iniSet;
                }
            }
            catch (Exception)
            {
                return new Settings();
            }
        }
        //Записываем в ini-файл. Запись происходит в выбранную секцию в выбранный ключ.
        public void SetTestPath(string value)
        {
            var iniFile = ReadFile();
            iniFile.TestPath = value;
            using (var stream = new FileStream(Path, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                serializer.Serialize(stream, iniFile);
            }
        }
        public void SetResultsPath(string value)
        {
            var iniFile = ReadFile();
            iniFile.ResultsPath = value;
            using (var stream = new FileStream(Path, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                serializer.Serialize(stream, iniFile);
            }
        }

        public void SetIp(string value)
        {
            var iniFile = ReadFile();
            iniFile.IpAdress = value;
            using (var stream = new FileStream(Path, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                serializer.Serialize(stream, iniFile);
            }
        }

        public void SetPort(int value)
        {
            try
            {
                var iniFile = ReadFile();
                iniFile.Port = value;
                using (var stream = new FileStream(Path, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    serializer.Serialize(stream, iniFile);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

    }
}
