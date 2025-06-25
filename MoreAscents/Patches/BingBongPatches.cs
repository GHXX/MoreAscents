using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace MoreAscents.Patches;

public class BingBongPatches
{
    [HarmonyPatch(typeof(BingBongsVisuals), "Update")]
    public static class Update {
        public static float BingBongUnheldFor = 0;
        
        [HarmonyPrefix]
        public static void Prefix(BingBongsVisuals __instance) {
            Item item = __instance.GetComponent<Item>();
            if (item.itemState == ItemState.Held) {
                BingBongUnheldFor = 0;
            }
        }
    }
}