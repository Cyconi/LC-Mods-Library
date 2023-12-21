using BepInEx.Logging;
using BepInEx;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Diagnostics;

[assembly: MelonInfo(typeof(CameraPlus.Mod), "Camera Plus", "0.0.1", "Cyconi")]
[assembly: MelonGame(null, "Lethal Company")]

namespace CameraPlus
{
    public class Mod : MelonMod
    {
        public static bool isPlayerInit = false;
        public override void OnApplicationStart()
        {
            CLog.Melon = true;
            CLog.L("Updated for Lethal Company v45");
            CLog.L("MelonLoader Detected! \n\n\t\t\t - Cyconi \n");
            CameraPatch.StartPatch();
            StartMsg.Msg();
        }
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (isPlayerInit)
                ThirdPerson.On3rdPersonStart();
        }
    }

    [BepInPlugin("cyconi.cameraplus", "Camera Plus", "0.0.1")]
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
            CameraPatch.StartPatch();
            StartMsg.Msg();
        }
    }
}
