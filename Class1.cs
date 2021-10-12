using System;
using System.Collections.Generic;
using HarmonyLib;
using BepInEx;
using UnityEngine;
using System.Reflection;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.UI;
using System.IO;
using System.Net;
using Photon.Realtime;
using UnityEngine.Rendering;

namespace Beacons
{
    [BepInPlugin("org.kokuchi.monkeytag.beacons", "Beacons!", "1.0.0")]
    public class MyMenuPatcher : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = new Harmony("com.kokuchi.monkeytag.beacons");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Update", MethodType.Normal)]
    class Beacons
    {
        static void Postfix(GorillaLocomotion.Player __instance)
        {
                if (!PhotonNetwork.CurrentRoom.IsVisible || !PhotonNetwork.InRoom)
                {
                            VRRig[] vrRigs = (VRRig[])GameObject.FindObjectsOfType(typeof(VRRig));
                            foreach (VRRig riggggg in vrRigs)
                            {
                                if (!riggggg.isOfflineVRRig && !riggggg.isMyPlayer && !riggggg.photonView.IsMine)
                                {
                                    GameObject broitsmybeacon = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                    GameObject.Destroy(broitsmybeacon.GetComponent<BoxCollider>());
                                    GameObject.Destroy(broitsmybeacon.GetComponent<Rigidbody>());
                                    GameObject.Destroy(broitsmybeacon.GetComponent<Collider>());
                                    broitsmybeacon.transform.rotation = Quaternion.identity;
                                    broitsmybeacon.transform.localScale = new Vector3(0.002f, 200f, 0.02f);
                                    broitsmybeacon.transform.position = riggggg.transform.position;
                                    broitsmybeacon.GetComponent<MeshRenderer>().material = riggggg.mainSkin.material;
                                    GameObject.Destroy(broitsmybeacon, Time.deltaTime);
                                }
                            }
                        }
                    }
                }
            }
