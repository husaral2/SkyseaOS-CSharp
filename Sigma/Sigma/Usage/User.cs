using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Usage
{
    enum PrivilageLevel { Guest = 0, Normal = 1, Administrator = 2, Root = -1 }
    internal class User
    {
        private static int UserCount = 0;
        private int Level = (int) PrivilageLevel.Guest; //is not used
        private string ConfigFilePath;
        private string Name;

        public User(string ConfigFile, string Name)
        {
            ++UserCount;
            this.Name = Name;
            ConfigFilePath = ConfigFile;
        }

        public void LoadPreferences()
        {
            System.IO.FileStream fs = new System.IO.FileStream(ConfigFilePath, System.IO.FileMode.OpenOrCreate);
            System.IO.StreamReader sr = new System.IO.StreamReader(fs);
            KeyboardSettings.ChangeKeyboard(sr.ReadLine());
            sr.Close();
            sr.Dispose();
            fs.Close();
            fs.Dispose();
        }

        public void SavePreferences()
        {
            System.IO.FileStream fs = new System.IO.FileStream(ConfigFilePath, System.IO.FileMode.OpenOrCreate);
            System.IO.StreamWriter sr = new System.IO.StreamWriter(fs);
            sr.WriteLine(KeyboardSettings.KeyboardLayout);
            sr.Close();
            sr.Dispose();
            fs.Close();
            fs.Dispose();
        }
    }
}
