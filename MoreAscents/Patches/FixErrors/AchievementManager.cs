using HarmonyLib;

namespace MoreAscents.Patches;

internal static class AchievementManagerPatches
{
    [HarmonyPatch(typeof(AchievementManager), nameof(AchievementManager.GetMaxAscent))]
    internal static class GetMaxAscent
    {
        [HarmonyPostfix]
        internal static void Postfix(ref int __result) {
            if (__result > AscentData.Instance.ascents.Count-1) {
                __result = AscentData.Instance.ascents.Count-1;
            }
        }
    }
}