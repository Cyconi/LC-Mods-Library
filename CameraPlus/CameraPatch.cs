using GameNetcodeStuff;
using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static MelonLoader.MelonLogger;
using UnityEngine.InputSystem;
using UnityEngine;

namespace CameraPlus
{
    internal class CameraPatch
    {
        public static HarmonyLib.Harmony instance = new("Cyconi");
        public static void StartPatch() 
        {
            try
            {
                instance.Patch(typeof(PlayerControllerB).GetMethod("Awake", AccessTools.all),
                    postfix: new HarmonyMethod(typeof(CameraPatch).GetMethod(nameof(Awake), BindingFlags.NonPublic | BindingFlags.Static)));

                CLog.L("Awake Patched!");
            }
            catch (Exception) { CLog.L("Failed to Patch Awake"); }
            try
            {
                instance.Patch(typeof(PlayerControllerB).GetMethod("Update", AccessTools.all),
                    prefix: new HarmonyMethod(typeof(CameraPatch).GetMethod(nameof(Prefix), BindingFlags.NonPublic | BindingFlags.Static)),
                    postfix: new HarmonyMethod(typeof(CameraPatch).GetMethod(nameof(Postfix), BindingFlags.NonPublic | BindingFlags.Static)));

                CLog.L("Update Patched!");
            }
            catch (Exception) { CLog.L("Failed to Patch Update"); }
        }

        public static float zoomFov = 30f;
        static float originalFov = 0f;
        static Vector3 visorScale;
        private static void Awake() // runs x the amount of times there are players
        {
            visorScale = new Vector3(0f, 0f, 0f);           
        }
        private static void Prefix(PlayerControllerB __instance)
        {
            originalFov = __instance.gameplayCamera.fieldOfView;
            __instance.localVisor.localScale = visorScale;
            var k = Keyboard.current;
            if (k.leftCtrlKey.isPressed && k.tKey.wasPressedThisFrame || k.upArrowKey.wasPressedThisFrame || k.downArrowKey.wasPressedThisFrame)
                ThirdPerson.On3rdPersonStart();

            ThirdPerson.Camera3rdUpdate();
        }
        internal static void Postfix(PlayerControllerB __instance)
        {
            var mouse = Mouse.current;

            if (mouse.middleButton.isPressed || mouse.forwardButton.wasPressedThisFrame)
                zoomFov = 30f;

            try
            {
                if (mouse.forwardButton.isPressed)
                {
                    float mouseWheel = mouse.scroll.ReadValue().y;
                    if (mouseWheel > 0f && zoomFov > 10f)
                        zoomFov -= 5f;
                    else if (mouseWheel < 0f && zoomFov < 140f && zoomFov >= 10f)
                        zoomFov += 5f;
                    else if (mouseWheel > 0f && zoomFov <= 10f && zoomFov > 1f)
                        zoomFov -= 1f;
                    else if (mouseWheel < 0f && zoomFov <= 10f)
                        zoomFov += 1f;

                    __instance.gameplayCamera.fieldOfView = Mathf.Lerp(originalFov, zoomFov, Time.deltaTime * 10f);
                    return;
                }

                if (!mouse.forwardButton.isPressed)
                {
                    if (__instance.inTerminalMenu)
                        zoomFov = 60f;
                    else if (__instance.IsInspectingItem)
                        zoomFov = 46f;
                    //else if (__instance.isSprinting)
                    //zoomFov *= 1.03f;
                    else
                        zoomFov = ThirdPerson.fov;

                    __instance.gameplayCamera.fieldOfView = Mathf.Lerp(originalFov, zoomFov, Time.deltaTime * 10f);
                }
            }
            catch { }
        }
    }
}
