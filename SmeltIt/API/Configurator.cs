using System.Reflection;
using SmeltIt.Decorators;
using StardewModdingAPI;

namespace SmeltIt.API
{
    /// <summary>
    /// API for managing the StardewValley configuration settings.
    /// </summary>
    internal static class Configurator
    {
        /// <summary>
        /// Returns a configuration value for the given rule.
        /// </summary>
        /// <param name="config">The Stardew Valley ModConfig.</param>
        /// <param name="ruleId">The rule associated with the config value.</param>
        /// <returns>Returns true or false.</returns>
        internal static bool GetConfigValue(ModConfig? config, string ruleId)
        {
            var propertyInfo = GetConfigPropertyInfo(ruleId);
            return (bool)(propertyInfo?.GetValue(config) ?? false);
        }

        /// <summary>
        /// Sets a configuration value for the given rule.
        /// </summary>
        /// <param name="config">The Stardew Valley ModConfig.</param>
        /// <param name="ruleId">The rule associated with the config value.</param>
        /// <param name="value">The new config value for the rule.</param>
        internal static void SetConfigValue(ModConfig? config, string ruleId, bool value)
        {
            var propertyInfo = GetConfigPropertyInfo(ruleId);
            propertyInfo?.SetValue(config, value);
        }

        /// <summary>
        /// Resets the configuration to its default values.
        /// </summary>
        /// <param name="config">A reference to the Stardew Valley ModConfig.</param>
        internal static void ResetConfig(ref ModConfig? config)
        {
            config = new ModConfig();
        }

        /// <summary>
        /// Saves the modified configuration.
        ///
        /// Note: This gets called immediately after a config reset to persist the changes.
        /// </summary>
        /// <param name="config">The Stardew Valley ModConfig.</param>
        /// <param name="helper">The Stardew Valley Helper class.</param>
        /// <param name="onSave">The callback for handling save functionality.</param>
        internal static void SaveConfig(
            ModConfig? config,
            IModHelper helper,
            Action<ModConfig> onSave
        )
        {
            helper.WriteConfig(GetConfig(config));
            onSave.Invoke(GetConfig(config));
        }

        /// <summary>
        /// Registers the Stardew Valley ModConfig with Generic Mod Config Menu's API to allow users to access their
        /// config.json settings from the in-game UI.
        /// </summary>
        /// <param name="config">The Stardew Valley ModConfig.</param>
        /// <param name="helper">The Stardew Valley Helper class.</param>
        /// <param name="manifest">The Stardew Valley manifeset class.</param>
        /// <param name="onSave">The callback for handling save functionality.</param>
        internal static void Register(
            ModConfig? config,
            IModHelper helper,
            IManifest manifest,
            Action<ModConfig> onSave
        )
        {
            // get Generic Mod Config Menu's API (if it's installed)
            var configMenu = helper.ModRegistry.GetApi<IGenericModConfigMenuApi>(
                "spacechase0.GenericModConfigMenu"
            );

            if (configMenu is null)
                return;

            // register mod
            configMenu.Register(
                mod: manifest,
                reset: () => ResetConfig(ref config),
                save: () => SaveConfig(config, helper, onSave)
            );

            // add options
            foreach (ConfigOption option in GetConfigOptions())
            {
                configMenu.AddBoolOption(
                    mod: manifest,
                    name: () => option.Name,
                    tooltip: () => option.Tooltip,
                    getValue: () => GetConfigValue(config, option.FieldId),
                    setValue: value => SetConfigValue(config, option.FieldId, value)
                );
            }
        }

        /*********
        ** Helpers
        *********/

        /// <summary>
        /// Returns the ConfigOption for the given rule.
        /// </summary>
        /// <param name="ruleId">The rule associated with the ConfigOption.</param>
        /// <returns>Returns PropertyInfo for the ConfigOption attribute.</returns>
        private static PropertyInfo? GetConfigPropertyInfo(string ruleId)
        {
            return typeof(ModConfig)
                .GetProperties()
                .FirstOrDefault(propertyInfo =>
                    propertyInfo.GetCustomAttribute<ConfigOption>()?.FieldId == ruleId
                );
        }

        /// <summary>
        /// Returns a list of ConfigOptions.
        /// </summary>
        /// <returns>Returns a list of ConfigOptions.</returns>
        private static List<ConfigOption> GetConfigOptions()
        {
            return typeof(ModConfig)
                .GetProperties()
                .Select(propertyInfo => propertyInfo.GetCustomAttribute<ConfigOption>())
                .OfType<ConfigOption>()
                .ToList();
        }

        /// <summary>
        /// Returns the original ModConfig or a new ModConfig when null.
        /// </summary>
        /// <param name="config">The Stardew Valley ModConfig.</param>
        /// <returns>Returns a ModConfig.</returns>
        private static ModConfig GetConfig(ModConfig? config)
        {
            return config ?? new ModConfig();
        }
    }
}
