using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class NewLog
    {
        private readonly string _path;

        public NewLog()
        {
            this._path = GetPath();
        }

        public void Add(string message)
        {

            CreateDirectory();
            string nameFile = GetNameFile();
            string stringLog = "";

            stringLog += DateTime.Now + " - " + message + Environment.NewLine;

            StreamWriter streamWriter = new StreamWriter(_path + "/" + nameFile, true);
            streamWriter.Write(stringLog);
            streamWriter.Close();

        }

        private string GetNameFile()
        {
            string nameFile;

            nameFile = "log_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt";

            return nameFile;
        }

        private void CreateDirectory()
        {
            try
            {
                if (!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DirectoryNotFoundException(ex.Message);
            }
        }

        private string GetPath()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string direction = ConfigurationManager.AppSettings.Get("Log");
            DirectoryInfo directory = new DirectoryInfo(
                Path.GetFullPath(Path.Combine(currentDirectory, direction)));

            return directory.ToString();
        }
    }
}
