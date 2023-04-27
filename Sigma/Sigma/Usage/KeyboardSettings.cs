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
        public static string Language = "en_US";
        public static string KeyboardLayout = "US_Standard";

        public static bool ChangeKeyboard(string Layout)
        {
            switch (Layout) {
                case "TR_StandardQ":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.TRStandardLayout());
                    KeyboardLayout = "TR_StandardQ";
                    return true;
                case "US_Standard":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.USStandardLayout());
                    KeyboardLayout = "US_Standard";
                    return true;
                case "DE_Standard":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.DEStandardLayout());
                    KeyboardLayout = "DE_Standard";
                    return true;
                case "FR_Standard":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.FRStandardLayout());
                    KeyboardLayout = "FR_Standard";
                    return true;
                case "ES_Standard":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.ESStandardLayout());
                    KeyboardLayout = "ES_Standard";
                    return true;
                default:
                    Console.WriteLine("The map hasn't been implemented yet or doesn't exist.");
                    return false;
            }
        }
    }
}
