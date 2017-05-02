using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public IEnumerable<string> GetAllFiles()
        {
            DirectoryInfo directory = new DirectoryInfo(@"C:\Testing");
            var files = directory.GetFiles().Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden)).Select(f => f.Name).ToArray();
            return files;
        }

        public string GetByName(string fileName)
        {
            string path = Path.Combine(@"C:\Testing", fileName);
            StreamReader sr = File.OpenText(path);
            string text = sr.ReadToEnd();
            sr.Close();
            return text;
        }

        public void DeleteFile(string fileName)
        {
            string[] files = Directory.GetFiles(@"C:\Testing");
            foreach (var file in files)
            {
                if (fileName==file)
                {
                    File.Delete(file);
                }
            }            

        }

        public void Post(string name, string content)
        {
            string uri = @"C:\Testing";
            string path = Path.Combine(uri, name);
            File.Create(path);
            StreamWriter sw = new StreamWriter(path);
            sw.Write(content);

        }

        public void Put(string name, string content)
        {
            string uri = @"C:\Testing";
            string path = Path.Combine(uri, name);
            StreamWriter sw = new StreamWriter(path);
            sw.Write(content);
            sw.Close();

        }
    }
}
