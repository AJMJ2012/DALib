using System;
using Terraria.ModLoader;

namespace DALib.Extensions;

public static class ModExtensions
{
	public static string GetName(this Mod mod)
		=> string.IsNullOrEmpty(mod.DisplayName?.Trim()) ? mod.Name : mod.DisplayName;
	public static string GetCleanName(this Mod mod)
		=> string.IsNullOrEmpty(mod.DisplayNameClean?.Trim()) ? mod.Name : mod.DisplayNameClean;

	[Obsolete("mod.ModName is deprecated, please use mod.GetName instead.")]
	static string ModName(this Mod mod)
		=> GetName(mod);
	[Obsolete("mod.ModCleanName is deprecated, please use mod.GetCleanName instead.")]
	static string ModCleanName(this Mod mod)
		=> GetCleanName(mod);
}