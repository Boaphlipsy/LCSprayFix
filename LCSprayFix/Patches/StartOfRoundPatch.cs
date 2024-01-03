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
            RemoveAllPaint();
        }

        private static void RemoveAllPaint()
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
