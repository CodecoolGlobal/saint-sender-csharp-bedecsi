﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SaintSender.Core.Models
{
    public class Serializer
    {
        public Credentials credentials { get; set; }

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
            using (TextWriter textWriter = new StreamWriter(Environment.CurrentDirectory + "\\" + credentials.EmailAddress +".xml"))
            {
                serializer.Serialize(textWriter, credentials);
            }
        }

        public void XMLbackup(List<Email> emails)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Email>), new XmlRootAttribute("Emails"));
            using (TextWriter textWriter = new StreamWriter(Environment.CurrentDirectory + "\\Backup\\backup.xml"))
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
            TextReader textReader = new StreamReader(Environment.CurrentDirectory + "\\Backup\\backup.xml");
            object obj = deserializer.Deserialize(textReader);
            List<Email> xmlObject = (List<Email>)obj;
            return xmlObject;
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
            TextReader textReader = new StreamReader(file.FullName);
            object obj = deseliarizer.Deserialize(textReader);
            Credentials xmlObject = (Credentials)obj;
            return xmlObject;
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