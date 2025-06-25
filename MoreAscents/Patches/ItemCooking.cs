﻿using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace MoreAscents.Patches;

public class ItemCookingPatches
{
    [HarmonyPatch(typeof(ItemCooking), "FinishCookingRPC")]
    public static class FinishCookingRPC {
        [HarmonyPrefix]
        public static void Prefix(ItemCooking __instance) {
            foreach (AscentGimmick gimmick in AscentGimmickHandler.gimmicks) {
                if (!gimmick.active)
                    continue;
                gimmick.OnFinishCooking(__instance);
            }
        }
    }
}