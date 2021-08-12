using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SaintSender.Core.Models;

namespace SaintSender.Core.Tests
{
    [TestFixture]
    class SerializerTests
    {
        private Serializer serializer;
        private string XMLSavePath;
        private string XMLBackupPath;
        private List<Email> testBackup;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            serializer = new Serializer(new Credentials("bedecsi2ndtw1test@gmail.com", "IDontActuallyHateWPF", ""));
            XMLSavePath = Environment.CurrentDirectory + "\\" + serializer.credentials.EmailAddress + ".xml";
            XMLBackupPath = Environment.CurrentDirectory + "\\Backup\\backup.xml";
            testBackup = new List<Email>() { new Email("Adam Sender", "Test subject", DateTime.Now, "This is a test body.") };
        }

        [Test, Order(1)]
        public void XMLsave_CreatesXMLfile_IsFileCreated()
        {
            serializer.XMLsave();
            Assert.IsTrue(File.Exists(XMLSavePath));
        }

        [Test]
        public void XMLbackup_CreatesBackupFile_IsFileCreated()
        {
            serializer.XMLbackup(testBackup);
            Assert.IsTrue(File.Exists(XMLBackupPath));
        }

        [Test]
        public void ReadXML_ReadsFromXML_ReturnsCorrectCredential()
        {
            serializer.XMLsave();
            var credentials = serializer.ReadXML(new FileInfo(XMLSavePath));
            Assert.AreEqual(serializer.credentials.EmailAddress, credentials.EmailAddress);
        }

        [Test]
        public void ReadXMLbackup_ReadsFromXML_ReturnsCorrectEmails()
        {
            serializer.XMLbackup(testBackup);
            var emails = serializer.ReadXMLbackup();
            Assert.AreEqual(testBackup[0].Sender, emails[0].Sender);
        }

        [Test]
        public void GetXMLfiles_SearchForXMLFiles_ReturnsCorrectFileInfoArray()
        {
            serializer.XMLsave();
            var files = serializer.GetXMLfiles();
            Assert.AreEqual("bedecsi2ndtw1test@gmail.com.xml", files[0].Name);
        }

        [Test]
        public void DeleteXMLfiles_DeletesXMLfiles_FilesAreDeleted()
        {
            serializer.XMLsave();
            serializer.DeleteXMLfiles();
            Assert.IsTrue(!File.Exists(XMLSavePath));
        }
    }
}
