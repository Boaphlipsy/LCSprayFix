using System.Collections.Generic;
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

    [HarmonyPatch(typeof(SprayPaintItem))]
    public class SprayPaintItemPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("AddSprayPaintLocal")]
        private static void AddSprayPaintLocalPatch()
        {
            SprayCanFix.UpdatePaintToRemove();
        }
    }

    [HarmonyPatch(typeof(GameNetworkManager))]
    public class GameNetworkManagerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(GameNetworkManager.Disconnect))]
        private static void DisconnectPatch()
        {
            SprayCanFix.ClearPaintToRemove();
        }
    }

    public class SprayCanFix
    {
        private static readonly HashSet<GameObject> allSprayDecals = new HashSet<GameObject>();

        public static void UpdatePaintToRemove()
        {
            if (SprayPaintItem.sprayPaintDecals.Count <= 0) return;

            allSprayDecals.Add(SprayPaintItem.sprayPaintDecals[SprayPaintItem.sprayPaintDecalsIndex]);
        }

        public static void ClearPaintToRemove()
        {
            allSprayDecals.Clear();
        }

        public static void RemoveAllPaint()
        {
            if (LCSprayFix.Instance.Config.DEV_MODE)
                LCSprayFix.Instance.Logger.LogInfo($"Removing all spray");

            int skippedNull = 0;
            int removed = 0;
            int wasActive = 0;
            foreach(var decal in allSprayDecals)
            {
                if (decal == null)
                {
                    skippedNull++;
                    continue;
                }

                if (decal.activeSelf)
                {
                    wasActive++;
                    decal.SetActive(false);
                }

                removed++;
                Object.Destroy(decal);
            }

            if (LCSprayFix.Instance.Config.DEV_MODE)
            {
                LCSprayFix.Instance.Logger.LogInfo($"Total spray to delete: {allSprayDecals.Count}");
                LCSprayFix.Instance.Logger.LogInfo($"Total spray removed: {removed}");
                LCSprayFix.Instance.Logger.LogInfo($"Total null spray skipped: {skippedNull}");
                LCSprayFix.Instance.Logger.LogInfo($"Total spray was active: {wasActive}");
            }
        }
    }
}
