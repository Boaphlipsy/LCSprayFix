using HarmonyLib;
using UnityEngine;


namespace LCSprayFix.Patches
{
    [HarmonyPatch(typeof(StartMatchLever))]
    public class StartMatchLeverPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(StartMatchLever.EndGame))]
        private static void SprayPaintPatch()
        {
            SprayCanFix.RemoveAllPaint();
        }
    }

    [HarmonyPatch(typeof(StartOfRound))]
    public class StartOfRoundPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(StartOfRound.ShipLeaveAutomatically))]
        private static void ShipLeaveAutomaticallyPatch()
        {
            SprayCanFix.RemoveAllPaint();
        }
    }

    public class SprayCanFix
    {
        public static void RemoveAllPaint()
        {
            foreach (GameObject decal in SprayPaintItem.sprayPaintDecals)
            {
                if (decal == null) continue;

                decal.SetActive(false);
                Object.Destroy(decal);
            }
        }
    }
}
