using KitchenData;
using KitchenLib;
using KitchenMods;
using PreferenceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Collections;

namespace CustomerCostumeMod
{
    public class Main : BaseMod
    {
        public const string MOD_GUID = "MrSnowShark.PlateUp.CustomerCostumeMod";
        public const string MOD_NAME = "CustomerCostumeMod";
        public const string MOD_VERSION = "0.0.1";
        public const string MOD_AUTHOR = "MrSnowShark";
        public const string MOD_GAMEVERSION = ">=1.1.4";

        public const string COSTUME_HATS_ENABLED_ID = "costumeHatsEnabled";
        public const string COSTUME_OUTFITS_ENABLED_ID = "costumeOutfitsEnabled";

        internal static PreferenceSystemManager PrefManager;

        public Main() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        public virtual HashSet<string> RequiredModNames => new HashSet<string>()
        {
            "PreferenceSystem"
        };

        protected sealed override void OnPostActivate(Mod mod)
        {
            List<string> missingModNames = new List<string>();
            IEnumerable<string> loadedModNames = ModPreload.Mods.Select(x => x.Name.ToLowerInvariant());
            foreach (string name in RequiredModNames)
            {
                if (loadedModNames.Contains(name.ToLowerInvariant()))
                    continue;
                missingModNames.Add(name);
            }
            if (missingModNames.Count > 0)
            {
                throw new ModPackLoadException($"Error! Missing dependencies. {MOD_NAME} requires that you subscribe to {(String.Join(", ", missingModNames))}.");
            }

            PrefManager = new PreferenceSystemManager(MOD_GUID, MOD_NAME);
            CreatePreferences();

        }

        private void CreatePreferences()
        {
            PrefManager
                .AddLabel("Customer Costume Mod")
                .AddSpacer()
                .AddInfo("Changing \"Customer Costumes\" only takes effect upon game restart.")
                .AddSpacer()
                .AddLabel("Customer Hats")
                .AddOption<bool>(
                    COSTUME_HATS_ENABLED_ID,
                        false,
                        new bool[] { false, true },
                        new string[] { "Disabled", "Enabled" })
                .AddSpacer()
                .AddLabel("Customer Outfits")
                    .AddOption<bool>(
                        COSTUME_OUTFITS_ENABLED_ID,
                            false,
                            new bool[] { false, true },
                            new string[] { "Disabled", "Enabled" })
                 .AddSpacer();

            PrefManager.RegisterMenu(PreferenceSystemManager.MenuType.MainMenu);
            PrefManager.RegisterMenu(PreferenceSystemManager.MenuType.PauseMenu);

        }
    }
}