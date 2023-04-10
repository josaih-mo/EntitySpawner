using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using GorillaNetworking;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR;
using static OVRPlugin;
using Utilla;

namespace EntitySpawner
{
    [BepInPlugin("Josiah.EntitySpawner", "Josiah's EntitySpawner", "1.0.0")]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [ModdedGamemode]
    public class EntitySpawner : BaseUnityPlugin
    {
        bool inAllowedRoom = false;
        private bool toggled;
        private List<InputDevice> list = new List<InputDevice>();
        public void Update()
        {
            if (inAllowedRoom) { 
                InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, list);
                list[0].TryGetFeatureValue(CommonUsages.triggerButton, out toggled);
                if (toggled)
                {
                    GameObject entityGO = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    UnityEngine.Object.Destroy(entityGO.GetComponent<BoxCollider>());
                    entityGO.transform.position = GorillaTagger.Instance.myVRRig.gameObject.transform.position;
                }
            }
        }
        [ModdedGamemodeJoin]
        private void RoomJoined(string gamemode)
        {
            // The room is modded. Enable mod stuff.
            inAllowedRoom = true;
        }

        [ModdedGamemodeLeave]
        private void RoomLeft(string gamemode)
        {
            // The room was left. Disable mod stuff.
            inAllowedRoom = false;
        }
    }
}