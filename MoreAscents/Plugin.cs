using System;
using BepInEx;
using BepInEx.Logging;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using MoreAscents.API;
using MoreAscents.Patches;
using Zorro.Core;
using Logger = UnityEngine.Logger;

namespace MoreAscents
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    internal class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private void Awake()
        {
            Logger = base.Logger;
            
            // custom ones
            AscentGimmickHandler.RegisterAscent<FallDamageGimmick>();
            AscentGimmickHandler.RegisterAscent<AfflictionGimmick>();
            AscentGimmickHandler.RegisterAscent<LuggageGimmick>();
            AscentGimmickHandler.RegisterAscent<HelpingIsBadGimmick>();
            AscentGimmickHandler.RegisterAscent<OccultStatueGimmick>();
            
            // chaos ones
            AscentGimmickHandler.RegisterAscent<SkeletonGimmick>();
            AscentGimmickHandler.RegisterAscent<CampfireGimmick>();
            AscentGimmickHandler.RegisterAscent<SunHotGimmick>();
            AscentGimmickHandler.RegisterAscent<BingBongGimmick>();
            
            AscentGimmickHandler.Initialize();
            
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            
            Harmony harmony = new(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }

        private void Update() {
            GUIManagerPatches.Grasp.SinceLastGrab += Time.deltaTime;

            BingBongMechanics.BingBongUnheldFor += Time.deltaTime;
            
            foreach (AscentGimmick gimmick in AscentGimmickHandler.gimmicks) {
                if (!gimmick.active) {
                    continue;
                }
                gimmick.Update();
            }
        }
    }

    [HarmonyPatch(typeof(BoardingPass), "UpdateAscent")]
    internal static class boarding_initilize_patch
    {
        internal static void Prefix(BoardingPass __instance)
        {
            if (Input.GetKey(KeyCode.L)) {
                Singleton<AchievementManager>.Instance.SetSteamStat(STEAMSTATTYPE.MaxAscent, 155);
            }
            
            FieldInfo info = __instance.GetType().GetField("maxAscent",BindingFlags.Instance | BindingFlags.NonPublic);
            info.SetValue(__instance, AscentData.Instance.ascents.Count - 2);
            Plugin.Logger.LogInfo($"Ascents capped {info.GetValue(__instance)}");
        }
    }
}
