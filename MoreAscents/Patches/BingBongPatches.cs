using HarmonyLib;
using System.Collections.Generic;
using MoreAscents.API;
using UnityEngine;

namespace MoreAscents.Patches;

internal class BingBongPatches
{
    [HarmonyPatch(typeof(BingBongsVisuals), "Update")]
    internal static class Update {
        [HarmonyPrefix]
        internal static void Prefix(BingBongsVisuals __instance) {
            Item item = __instance.GetComponent<Item>();
            if (item.itemState == ItemState.Held) {
                BingBongMechanics.BingBongUnheldFor = 0;
            }
        }
    }
}