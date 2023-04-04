
using Cosmos.HAL.BlockDevice;
using System;
using System.Runtime.CompilerServices;

namespace Sigma.FileSystem
{
        internal static class Controller
    {
        public static Cosmos.System.FileSystem.CosmosVFS fs;
        static string CurrentDirectory = "0:";
        static bool IsInitialized = false;
        public static readonly char[] invalidCharacters = {'\\','<','>','"','?','|','/','*' };

        public static bool InitializeFilesystem()
        {
            if (IsInitialized)
                return false;
            try
            {
                fs = new Cosmos.System.FileSystem.CosmosVFS();
                Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(fs);
                fs.Initialize(true);
                IsInitialized = true;
                return true;
            }catch(Exception excp)
            {
                Kernel.PrintDebug(excp.Message);
                return false;
            }

        }

        //Will get the current directory without the path tag
        public static string GetCurrentDirectory()
        {
            return CurrentDirectory;
        }

        public static void SetCurrentDirectory(string dir)
        {
            CurrentDirectory = dir;
        }

        public static void GoBack()
        {
            string[] splitted = CurrentDirectory.Split('\\');
            if (splitted.Length == 1)
                return;
            CurrentDirectory = CurrentDirectory.Remove(CurrentDirectory.LastIndexOf('\\'));

        }
    }
}
