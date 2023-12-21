using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Traversal_Emotes
{
    internal class EmotePatch
    {
        public static HarmonyLib.Harmony instance = new("Cyconi");

        public static void StartPatch()
        {
            try
            {
                instance.Patch(typeof(PlayerControllerB).GetMethod("CheckConditionsForEmote", AccessTools.all),
                    postfix: new HarmonyMethod(typeof(EmotePatch).GetMethod(nameof(Postfix), BindingFlags.NonPublic | BindingFlags.Static)));

                CLog.L("Emote Conditions Patched!");
            } catch { CLog.L("Emote Conditions Patched!"); }
        }
        private static void Postfix(ref bool __result)
        {
            if (!GameNetworkManager.Instance.localPlayerController.isCrouching)
                __result = true;
        }
    }
}
