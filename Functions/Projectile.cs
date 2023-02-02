using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DALib.Functions;

public static class ProjectileFunctions {
#region ProjectileID
	static Dictionary<int, string> projectileTypeIDCache = new();

	static string GetVanillaProjectileName(int type) {
		string typeID = type.ToString();
		if (!projectileTypeIDCache.TryGetValue(type, out typeID)) {
			typeID = FieldResolver.FindConstantName<short>(typeof(ProjectileID), (short)type) ?? typeID;
			projectileTypeIDCache.TryAdd(type, typeID);
		}
		return typeID;
	}

	static string GetProjectileID(int type)
		=> String.Format("{0}.{1}", GetProjectileMod(type), GetProjectileName(type));

	static string GetProjectileMod(int type) 
		=> ProjectileLoader.GetProjectile(type)?.Mod.Name ?? "Terraria";

	static string GetProjectileName(int type)
		=> ProjectileLoader.GetProjectile(type)?.Name ?? GetVanillaProjectileName(type);

	public static string GetProjectileID(this Projectile projectile)
		=> GetProjectileID(projectile.type);

	public static string GetProjectileName(this Projectile projectile)
		=> GetProjectileName(projectile.type);

	public static string GetProjectileMod(this Projectile projectile)
		=> GetProjectileMod(projectile.type);
#endregion
#region ProjectileAIStyleID
	static Dictionary<int, string> projectileAIStyleIDCache = new();
	static string GetProjectileAIStyleID(int aiStyle) {
		string aiStyleID = aiStyle.ToString();
		if (!projectileAIStyleIDCache.TryGetValue(aiStyle, out aiStyleID)) {
			aiStyleID = FieldResolver.FindConstantName<short>(typeof(ProjAIStyleID), (short)aiStyle) ?? aiStyleID;
			projectileAIStyleIDCache.TryAdd(aiStyle, aiStyleID);
		}
		return String.IsNullOrEmpty(aiStyleID) ? aiStyle.ToString() : aiStyleID;
	}

	public static string GetProjectileAIStyleID(this Projectile projectile)
		=> GetProjectileAIStyleID(projectile.aiStyle);
#endregion
}
