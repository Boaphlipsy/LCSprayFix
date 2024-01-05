using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using LCSprayFix.Patches;

namespace LCSprayFix
{
    [BepInPlugin(PluginData.Guid, PluginData.Name, PluginData.Version)]
    public class LCSprayFix : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginData.Guid);

        public static LCSprayFix Instance { get; private set; }

        public new ManualLogSource Logger { get; private set; }
        public new PluginConfig Config { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            Logger = base.Logger;
            Config = new PluginConfig(base.Config);

            harmony.PatchAll(typeof(LCSprayFix));
            harmony.PatchAll(typeof(StartMatchLeverPatch));
            harmony.PatchAll(typeof(StartOfRoundPatch));
            harmony.PatchAll(typeof(SprayPaintItemPatch));

            Logger.LogInfo($"I have Loaded.");
        }
    }
}
