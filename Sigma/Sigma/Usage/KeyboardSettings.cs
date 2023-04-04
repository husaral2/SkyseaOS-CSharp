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

        public static void ChangeKeyboard(string Layout)
        {
            switch (Layout) {
                case "TR_StandardQ":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.TR_StandardQ());
                    KeyboardLayout = "TR_StandardQ";
                    break;
                case "US_Standard":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.US_Standard());
                    KeyboardLayout = "US_Standard";
                    break;
                case "DE_Standard":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.DE_Standard());
                    KeyboardLayout = "DE_Standard";
                    break;
                case "FR_Standard":
                    Cosmos.System.KeyboardManager.SetKeyLayout(new Cosmos.System.ScanMaps.FR_Standard());
                    KeyboardLayout = "FR_Standard";
                    break;
                default:
                    Console.WriteLine("The map hasn't been implemented yet or doesn't exist.");
                    break;
            }
        }
    }
}
