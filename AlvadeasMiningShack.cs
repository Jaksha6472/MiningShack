using System;
using StardewModdingAPI;
using StardewValley;
using HarmonyLib;
using StardewValley.Network;

namespace AlvadeasMiningShack
{
    public class AlvadeasMiningShack : Mod
    {
        internal static AlvadeasMiningShack Instance { get; private set; }

        public override void Entry(IModHelper helper)
        {
            Instance = this;
            // Apply Harmony Patches
            var harmony = new Harmony(this.ModManifest.UniqueID);
            FarmPatches.Apply(harmony);

            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
        }

        // Code from https://stardewvalleywiki.com/Modding:Maps
        // Load the Shack Interior as new map
        private void OnSaveLoaded(object sender, EventArgs e)
        {
            if (Game1.whichFarm != 3)
                return;

            // Fix the Shack, if it's already repaired
            if (NetWorldState.checkAnywhereForWorldStateID("miningShackRepaired"))
                FarmPatches.fixShack();
        }
    }
}