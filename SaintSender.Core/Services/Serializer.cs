using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SaintSender.Core.Models
{
    public class Serializer
    {
        public Credentials credentials { get; set; }
        private readonly string backupPath = Environment.CurrentDirectory + "\\Backup\\backup.xml";

        public Serializer(Credentials Credentials)
        {
            credentials = Credentials;
        }

        public Serializer()
        {

        }

        public void XMLsave()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Credentials));
            using (TextWriter textWriter = new StreamWriter(Environment.CurrentDirectory + "\\" + credentials.EmailAddress + ".xml"))
            {
                serializer.Serialize(textWriter, credentials);
            }
        }

        public void XMLbackup(List<Email> emails)
        {
            Directory.CreateDirectory(Environment.CurrentDirectory + "\\Backup\\");
            XmlSerializer serializer = new XmlSerializer(typeof(List<Email>), new XmlRootAttribute("Emails"));
            using (TextWriter textWriter = new StreamWriter(backupPath))
            {
                serializer.Serialize(textWriter, emails);
            }
        }

        public List<Email> ReadXMLbackup()
        {
            var root = new XmlRootAttribute();
            root.ElementName = "Emails";
            root.IsNullable = true;
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Email>), root);
            using (TextReader textReader = new StreamReader(backupPath))
            {
                object obj = deserializer.Deserialize(textReader);
                List<Email> xmlObject = (List<Email>)obj;
                return xmlObject;
            }
        }

        public FileInfo[] GetXMLfiles()
        {
            DirectoryInfo searchDir = new DirectoryInfo(Environment.CurrentDirectory);
            FileInfo[] xmlFiles = searchDir.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
            return xmlFiles;
        }

        public Credentials ReadXML(FileInfo file)
        {
            XmlSerializer deseliarizer = new XmlSerializer(typeof(Credentials));
            using (TextReader textReader = new StreamReader(file.FullName))
            {
                object obj = deseliarizer.Deserialize(textReader);
                Credentials xmlObject = (Credentials)obj;
                return xmlObject;
            }
        }

        public void DeleteXMLfiles()
        {
            FileInfo[] XMLfiles = GetXMLfiles();
            foreach (FileInfo file in XMLfiles)
            {
                if (file.Name.Equals(credentials.EmailAddress + ".xml"))
                {
                    file.Delete();

                }

            }
        }
    }
}
