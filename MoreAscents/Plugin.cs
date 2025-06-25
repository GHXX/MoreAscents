using System;
using BepInEx;
using BepInEx.Logging;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using HarmonyLib;
using MoreAscents.Patches;
using Zorro.Core;
using Logger = UnityEngine.Logger;

namespace MoreAscents
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private void Awake()
        {
            var ascentData = AscentData.Instance;
            List<AscentData.AscentInstanceData> ascents = ascentData.ascents;
            List<AscentData.AscentInstanceData> newAscents = new List<AscentData.AscentInstanceData>();

            foreach (AscentData.AscentInstanceData data in ascents) {
                newAscents.Add(data);
            }

            // custom ones
            AscentGimmickHandler.RegisterAscent<FallDamageGimmick>(newAscents);
            AscentGimmickHandler.RegisterAscent<AfflictionGimmick>(newAscents);
            AscentGimmickHandler.RegisterAscent<LuggageGimmick>(newAscents);
            AscentGimmickHandler.RegisterAscent<HelpingIsBadGimmick>(newAscents);
            AscentGimmickHandler.RegisterAscent<OccultStatueGimmick>(newAscents);
            
            // chaos ones
            AscentGimmickHandler.RegisterAscent<SkeletonGimmick>(newAscents);
            AscentGimmickHandler.RegisterAscent<CampfireGimmick>(newAscents);
            AscentGimmickHandler.RegisterAscent<SunHotGimmick>(newAscents);
            AscentGimmickHandler.RegisterAscent<BingBongGimmick>(newAscents);

            // cant get this working
            //AscentGimmickHandler.RegisterAscent<FogGimmick>(newAscents);

            ascentData.ascents = newAscents;

            // Plugin startup logic
            Logger = base.Logger;
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }

        private void Update() {
            GUIManagerPatches.Grasp.SinceLastGrab += Time.deltaTime;

            BingBongPatches.Update.BingBongUnheldFor += Time.deltaTime;
            
            foreach (AscentGimmick gimmick in AscentGimmickHandler.gimmicks) {
                if (!gimmick.active) {
                    continue;
                }
                gimmick.Update();
            }
        }
    }

    [HarmonyPatch(typeof(BoardingPass), "UpdateAscent")]
    public static class boarding_initilize_patch
    {
        public static void Prefix(BoardingPass __instance)
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
