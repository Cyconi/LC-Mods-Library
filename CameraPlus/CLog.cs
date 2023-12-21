using BepInEx.Logging;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraPlus
{
    internal class CLog
    {
        internal static bool Melon = false;
        internal static bool Bepin = false;
        internal static void L(string MessageToLog)
        {
            if (Melon)
                MelonLogger.Msg(ConsoleColor.White, "[~>] " + MessageToLog);
            if (Bepin)
                Plugin.Log.Log(LogLevel.All, "[~>] " + MessageToLog);
        }
    }
}
