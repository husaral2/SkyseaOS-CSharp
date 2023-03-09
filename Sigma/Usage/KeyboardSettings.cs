using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Usage
{
    internal class KeyboardSettings
    {
        public static string Language = "en-US";
        public static string KeyboardLayout = "US-Standard";

        public static bool LoadPreferences(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
                return false;
            }
            string[] AllFileText = File.ReadAllText(fileName).Split(' ');
            Language = AllFileText[0];
            KeyboardLayout = AllFileText[1];
            ChangeKeyboard(KeyboardLayout);
            return true;
        }

        public static bool SavePreferences(string fileName)
        {
            if (File.Exists(fileName))
                return false;
            try
            {
                StreamWriter sr = new StreamWriter(fileName);
                sr.Write(Language + " " + KeyboardLayout);
                sr.Close();
                sr.Dispose();
                return true;
            }
            catch (Exception excp)
            {
                Console.WriteLine("An excepton has ocurred: " + excp.Message);
                return false;
            }
        }

        public static void ChangeKeyboard(string Layout)
        {
            switch (Layout) {
                case "TR_StandardQ":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.TR_StandardQ());
                    break;
                case "US_Standard":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.US_Standard());
                    break;
                case "DE_Standard":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.DE_Standard());
                    break;
                case "FR_Standard":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.FR_Standard());
                    break;
                default:
                    Console.WriteLine("The map hasn't been implemented yet or doesn't exist.");
                    break;
            }
        }
    }
}
