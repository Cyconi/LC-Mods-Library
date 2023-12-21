using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using BepInEx;
using MelonLoader;

[assembly: MelonInfo(typeof(Traversal_Emotes.Mod), "Traversal Emotes", "0.0.1", "Cyconi")]
[assembly: MelonGame(null, "Lethal Company")]
[assembly: MelonColor(System.ConsoleColor.DarkRed)]

namespace Traversal_Emotes
{
    public class Mod : MelonMod
    {
        public override void OnApplicationStart()
        {
            CLog.Melon = true;
            CLog.L("Updated for Lethal Company v45");
            CLog.L("MelonLoader Detected! \n\n\t\t\t - Cyconi \n");
            EmotePatch.StartPatch();
        }
    }

    [BepInPlugin("cyconi.traversal_emotes", "Traversal Emotes", "0.0.1")]
    internal class Plugin : BaseUnityPlugin
    {
        internal static Plugin instance;
        internal static ManualLogSource Log;
        public void Awake()
        {
            CLog.Bepin = true;
            instance = this;
            Log = Logger;
            CLog.L("Updated for Lethal Company v45");
            CLog.L("BepInEx Detected! \n\n\t\t\t - Cyconi \n");
            EmotePatch.StartPatch();
        }
    }
}
