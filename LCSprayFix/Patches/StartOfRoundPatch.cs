using HarmonyLib;
using UnityEngine;


namespace LCSprayFix.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    public class StartOfRoundPatch
    {
        [HarmonyPrefix]
        //[HarmonyPatch(nameof(StartOfRound.EndOfGame))]
        [HarmonyPatch("EndOfGame")]
        private static void SprayPaintPatch()
        {
            LCSprayFix.Instance.Logger.LogInfo("Removing spray decals.");
            RemoveAllPaint();
            LCSprayFix.Instance.Logger.LogInfo("Removed spray decals.");
        }

        private static void RemoveAllPaint()
        {
            foreach (GameObject decal in SprayPaintItem.sprayPaintDecals)
            {
                decal.SetActive(false);
            }
        }
    }
}
