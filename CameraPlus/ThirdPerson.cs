using CameraPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace CameraPlus
{
    internal class ThirdPerson
    {
        public static bool InThird = false;
        private static GameObject MainCamera;
        private static GameObject BackCamera;
        private static GameObject FrontCamera;
        private static int LastCamera;
        internal static bool isUpdating;
        internal static float fov = 66f;

        public static float proxThresh = 1.3f;
        public static float proxThreshF = 0.4f;
        public static float proxThreshB = 1f;
        public static void On3rdPersonStart()
        {
            if (BackCamera != null || FrontCamera != null)
                return;

            if (MainCamera == null)
                MainCamera = GameNetworkManager.Instance.localPlayerController.gameplayCamera.gameObject;

            BackCamera = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(BackCamera.GetComponent<MeshRenderer>());
            FrontCamera = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(FrontCamera.GetComponent<MeshRenderer>());             

            BackCamera.transform.localScale = MainCamera.transform.localScale;
            Rigidbody rigidbody = BackCamera.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            if (BackCamera.GetComponent<Collider>()) BackCamera.GetComponent<Collider>().enabled = false;
            BackCamera.GetComponent<Renderer>().enabled = false;
            BackCamera.AddComponent<Camera>();
            BackCamera.transform.parent = MainCamera.transform;
            BackCamera.transform.rotation = MainCamera.transform.rotation;
            BackCamera.transform.position = MainCamera.transform.position;
            BackCamera.transform.position -= BackCamera.transform.forward * 4f;
            MainCamera.GetComponent<Camera>().enabled = false;
            BackCamera.GetComponent<Camera>().fieldOfView = fov;

            FrontCamera.transform.localScale = MainCamera.transform.localScale;
            Rigidbody rigidbody2 = FrontCamera.AddComponent<Rigidbody>();
            rigidbody2.isKinematic = true;
            rigidbody2.useGravity = false;
            if (FrontCamera.GetComponent<Collider>()) FrontCamera.GetComponent<Collider>().enabled = false;
            FrontCamera.GetComponent<Renderer>().enabled = false;
            FrontCamera.AddComponent<Camera>();
            FrontCamera.transform.parent = MainCamera.transform;
            FrontCamera.transform.rotation = MainCamera.transform.rotation;
            FrontCamera.transform.Rotate(0f, 180f, 0f);
            FrontCamera.transform.position = MainCamera.transform.position;
            FrontCamera.transform.position += -FrontCamera.transform.forward * 4f;
            MainCamera.GetComponent<Camera>().enabled = false;
            FrontCamera.GetComponent<Camera>().fieldOfView = fov;

            BackCamera.GetComponent<Camera>().enabled = false;
            FrontCamera.GetComponent<Camera>().enabled = false;
            GameNetworkManager.Instance.localPlayerController.gameplayCamera.GetComponent<Camera>().enabled = true;

            isUpdating = true;
        }

        public static void Switch()
        {
            switch (LastCamera)
            {
                case 0:
                    LastCamera = 1;
                    MainCamera.GetComponent<Camera>().enabled = false;
                    BackCamera.GetComponent<Camera>().enabled = true;
                    FrontCamera.GetComponent<Camera>().enabled = false;
                    InThird = true;
                    ColliderToggles();
                    break;

                case 1:
                    LastCamera = 2;
                    MainCamera.GetComponent<Camera>().enabled = false;
                    BackCamera.GetComponent<Camera>().enabled = false;
                    FrontCamera.GetComponent<Camera>().enabled = true;
                    InThird = true;
                    ColliderToggles();
                    break;

                case 2:
                    LastCamera = 0;
                    MainCamera.GetComponent<Camera>().enabled = true;
                    BackCamera.GetComponent<Camera>().enabled = false;
                    FrontCamera.GetComponent<Camera>().enabled = false;
                    InThird = false;
                    ColliderToggles();
                    break;
            }
        }
        internal static void ColliderToggles()
        {
            try
            {
                if (InThird)
                {
                    GameObject.Find("Environment/HangarShip/Player/Misc/MapDot")?.SetActive(false);
                    GameObject.Find("PlayersContainer/Player/Misc/MapDot")?.SetActive(false);
                    GameObject.Find("PlayersContainer/Player (1)/Misc/MapDot")?.SetActive(false);
                    GameObject.Find("PlayersContainer/Player (2)/Misc/MapDot")?.SetActive(false);
                    GameObject.Find("PlayersContainer/Player (3)/Misc/MapDot")?.SetActive(false);

                    GameObject.Find("Environment/HangarShip/Player/ScavengerModel/metarig/ScavengerModelArmsOnly")?.SetActive(false);
                    GameObject.Find("PlayersContainer/Player/ScavengerModel/metarig/ScavengerModelArmsOnly")?.SetActive(false);
                    GameObject.Find("PlayersContainer/Player (1)/ScavengerModel/metarig/ScavengerModelArmsOnly")?.SetActive(false);
                    GameObject.Find("PlayersContainer/Player (2)/ScavengerModel/metarig/ScavengerModelArmsOnly")?.SetActive(false);
                    GameObject.Find("PlayersContainer/Player (3)/ScavengerModel/metarig/ScavengerModelArmsOnly")?.SetActive(false);
                }
                else
                {
                    GameObject.Find("Environment/HangarShip/Player/Misc/MapDot")?.SetActive(true);
                    GameObject.Find("PlayersContainer/Player/Misc/MapDot")?.SetActive(true);
                    GameObject.Find("PlayersContainer/Player (1)/Misc/MapDot")?.SetActive(true);
                    GameObject.Find("PlayersContainer/Player (2)/Misc/MapDot")?.SetActive(true);
                    GameObject.Find("PlayersContainer/Player (3)/Misc/MapDot")?.SetActive(true);

                    GameObject.Find("Environment/HangarShip/Player/ScavengerModel/metarig/ScavengerModelArmsOnly")?.SetActive(true);
                    GameObject.Find("PlayersContainer/Player/ScavengerModel/metarig/ScavengerModelArmsOnly")?.SetActive(true);
                    GameObject.Find("PlayersContainer/Player (1)/ScavengerModel/metarig/ScavengerModelArmsOnly")?.SetActive(true);
                    GameObject.Find("PlayersContainer/Player (2)/ScavengerModel/metarig/ScavengerModelArmsOnly")?.SetActive(true);
                    GameObject.Find("PlayersContainer/Player (3)/ScavengerModel/metarig/ScavengerModelArmsOnly")?.SetActive(true);
                }
            }
            catch (Exception e) { CLog.L($"{e}"); }
        }


        public static void Camera3rdUpdate()
        {
            if (MainCamera == null) return;

            var mouse = Mouse.current;
            var keyboard = Keyboard.current;
            try
            {
                FrontCamera.GetComponent<Camera>().fieldOfView = fov;
                FrontCamera.GetComponent<Camera>().nearClipPlane = 0.01f;
                BackCamera.GetComponent<Camera>().fieldOfView = fov;
                BackCamera.GetComponent<Camera>().nearClipPlane = 0.01f;

                if (BackCamera != null && FrontCamera != null && InThird)
                {
                    if (LastCamera != 0)
                    {
                        if (mouse.middleButton.isPressed)
                        {
                            BackCamera.transform.position = MainCamera.transform.position;
                            BackCamera.transform.position -= BackCamera.transform.forward * 4f;
                            FrontCamera.transform.position = MainCamera.transform.position;
                            FrontCamera.transform.position += -FrontCamera.transform.forward * 4f;
                        }

                        float axis = mouse.scroll.ReadValue().y;
                        float distance = Vector3.Distance(FrontCamera.transform.position, BackCamera.transform.position);
                        float distanceF = Vector3.Distance(FrontCamera.transform.position, MainCamera.transform.position);
                        float distanceB = Vector3.Distance(BackCamera.transform.position, MainCamera.transform.position);

                        if (axis > 0f && distance > proxThreshB)
                        {
                            BackCamera.transform.position += BackCamera.transform.forward * 0.1f;
                            FrontCamera.transform.position -= BackCamera.transform.forward * 0.1f;
                        }
                        else if (axis < 0f)
                        {
                            BackCamera.transform.position -= BackCamera.transform.forward * 0.1f;
                            FrontCamera.transform.position += BackCamera.transform.forward * 0.1f;
                        }
                    }
                }
                if (keyboard.leftCtrlKey.isPressed && keyboard.tKey.wasPressedThisFrame)
                    Switch();
            }
            catch (Exception e) { CLog.L("Error\n" + e); }

            if (mouse.middleButton.wasPressedThisFrame)
                fov = 66f;

            if (keyboard.upArrowKey.wasPressedThisFrame)
            {
                fov += 1f;
                //CLog.L($"fov {fov}");
                return;
            }
            if (keyboard.downArrowKey.wasPressedThisFrame)
            {
                fov -= 1f;
                //CLog.L($"fov {fov}");
                return;
            }
        }
    }
}



