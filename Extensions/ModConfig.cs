using System;
using Terraria.ModLoader.Config;

namespace DALib.Extensions;

public static class ModConfigExtensions
{
	public static string GetName(this ModConfig config)
		=> string.IsNullOrEmpty(config.DisplayName.Value?.Trim()) || config.DisplayName.Value.StartsWith(string.Format("Mods.{0}", config.Mod.Name)) ? config.Name : config.DisplayName.Value;

	[Obsolete("mod.ConfigName is deprecated, please use mod.ModCleanName instead.")]
	public static string ConfigName(this ModConfig config)
		=> GetName(config);
}

