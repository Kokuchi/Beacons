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
                            foreach (VRRig rig in vrRigs)
                            {
                                if (!rig.isOfflineVRRig && !rig.isMyPlayer && !rig.photonView.IsMine)
                                {
                                    GameObject beacon = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                                    GameObject.Destroy(beacon.GetComponent<BoxCollider>());
                                    GameObject.Destroy(beacon.GetComponent<Rigidbody>());
                                    GameObject.Destroy(beacon.GetComponent<Collider>());
                                    beacon.transform.rotation = Quaternion.identity;
                                    beacon.transform.localScale = new Vector3(0.002f, 200f, 0.02f);
                                    beacon.transform.position = rig.transform.position;
                                    beacon.GetComponent<MeshRenderer>().material = rig.mainSkin.material;
                                    GameObject.Destroy(beacon, Time.deltaTime);
                                }
                            }
                        }
                    }
                }
            }