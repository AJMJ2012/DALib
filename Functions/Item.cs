using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DALib.Functions;

public static class ItemFunctions
{
	#region ItemID
	static Dictionary<int, string> itemTypeIDCache = [];

	static string GetVanillaItemName(int type)
	{
		string typeID = type.ToString();
		if (!itemTypeIDCache.TryGetValue(type, out typeID))
		{
			typeID = FieldResolver.FindConstantName<short>(typeof(ItemID), (short)type) ?? typeID;
			itemTypeIDCache.TryAdd(type, typeID);
		}
		return typeID;
	}

	static string GetItemID(int type)
		=> String.Format("{0}.{1}", GetItemMod(type), GetItemName(type));

	static string GetItemMod(int type)
		=> ItemLoader.GetItem(type)?.Mod.Name ?? "Terraria";

	static string GetItemName(int type)
		=> ItemLoader.GetItem(type)?.Name ?? GetVanillaItemName(type);

	static string GetItemClass(int type)
		=> ItemLoader.GetItem(type)?.GetType().ToString() ?? "Terraria.Item";

	public static string GetItemID(this Item item)
		=> GetItemID(item.type);

	public static string GetItemMod(this Item item)
		=> GetItemMod(item.type);

	public static string GetItemName(this Item item)
		=> GetItemName(item.type);

	public static string GetItemClass(this Item item)
		=> GetItemClass(item.type);
	#endregion
	#region ItemUseStyleID
	static Dictionary<int, string> itemUseStyleIDCache = [];

	static string GetItemUseStyleID(int useStyle)
	{
		string useStyleID = useStyle.ToString();
		if (!itemUseStyleIDCache.TryGetValue(useStyle, out useStyleID))
		{
			useStyleID = FieldResolver.FindConstantName<int>(typeof(ItemUseStyleID), (int)useStyle) ?? useStyleID;
			itemUseStyleIDCache.TryAdd(useStyle, useStyleID);
		}
		return String.IsNullOrEmpty(useStyleID) ? useStyle.ToString() : useStyleID;
	}

	public static string GetItemUseStyleID(this Item item)
		=> GetItemUseStyleID(item.useStyle);
	#endregion
	#region AmmoID
	static Dictionary<int, string> ammoTypeIDCache = [];

	static string GetVanillaAmmoName(int type)
	{
		string typeID = type.ToString();
		if (!ammoTypeIDCache.TryGetValue(type, out typeID))
		{
			typeID = FieldResolver.FindConstantName<short>(typeof(AmmoID), (short)type) ?? GetVanillaItemName(type);
			ammoTypeIDCache.TryAdd(type, typeID);
		}
		return typeID;
	}

	static string GetAmmoID(int type)
		=> String.Format("{0}.{1}", GetAmmoMod(type), GetAmmoName(type));

	static string GetAmmoMod(int type)
		=> ItemLoader.GetItem(type)?.Mod.Name ?? "Terraria";

	static string GetAmmoName(int type)
		=> ItemLoader.GetItem(type)?.Name ?? GetVanillaAmmoName(type);

	public static string GetAmmoID(this Item item)
		=> GetAmmoID(item.type);

	public static string GetAmmoName(this Item item)
		=> GetAmmoName(item.type);

	public static string GetAmmoMod(this Item item)
		=> GetAmmoMod(item.type);
	#endregion

	public static int GetDamage(this Item item, Player player, bool calculated = true)
	{
		int damage = item.damage;
		if (calculated)
			damage = player.GetWeaponDamage(item, true);
		return damage;
	}

	public static int GetCrit(this Item item, Player player, bool calculated = true)
	{
		int crit = item.crit;
		if (calculated)
			crit = player.GetWeaponCrit(item);
		return crit;
	}

	public static int GetUseTime(this Item item, Player player, bool calculated = true)
	{
		int useTime = item.useTime;
		if (calculated)
			useTime = CombinedHooks.TotalUseTime(item.useTime, player, item);
		return useTime;
	}

	public static int GetUseAnimation(this Item item, Player player, bool calculated = true)
	{
		int useAnimation = item.useAnimation;
		if (calculated)
			useAnimation = CombinedHooks.TotalAnimationTime(item.useAnimation, player, item);
		return useAnimation;
	}

	public static int GetReuseDelay(this Item item, Player player, bool calculated = true)
	{
		int reuseDelay = item.reuseDelay;
		return reuseDelay;
	}

	public static float GetKnockBack(this Item item, Player player, bool calculated = true)
	{
		float knockBack = item.knockBack;
		if (calculated)
			knockBack = player.GetWeaponKnockback(item, item.knockBack);
		return knockBack;
	}

	public static int GetMana(this Item item, Player player, bool calculated = true)
	{
		int mana = item.mana;
		if (calculated)
			mana = (int)(item.mana * player.manaCost);
		return mana;
	}

	public static Item GetAmmo(this Item item, Player player) => player.ChooseAmmo(item) ?? new Item();

	public static int GetAmmoDamage(this Item item, Player player, bool calculated = true) => item.useAmmo > 0 ? GetDamage(GetAmmo(item, player), player, calculated) : 0;

	// TODO: Get projectile count of items.
	public static int GetProjectileCount(this Item item)
	{
		return 1;
	}

	public static int GetBurstCount(this Item item, Player player, bool calculated = true)
	{
		int useTime = GetUseTime(item, player, calculated);
		int useAnimation = GetUseAnimation(item, player, calculated);
		return !item.channel ? System.Math.Max(useAnimation / useTime, 0) : 0;
	}

	public static int GetRechargeTime(this Item item, Player player, bool calculated = true)
	{
		int useTime = GetUseTime(item, player, calculated);
		int useAnimation = GetUseAnimation(item, player, calculated);
		return !item.channel ? System.Math.Max(useTime - useAnimation, 0) : 0;
	}

	public static Projectile GetProjectile(this Item item)
	{
		Projectile projectile = new();
		projectile.SetDefaults(item.shoot);
		return projectile;
	}

	public static Item GetBait(this Item item, Player player)
	{
		Item bait = null;
		object[] args = [item];
		player.GetType().GetMethod("Fishing_GetBait", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(player, args);
		bait = (Item)args[0];
		return bait ?? new Item();
	}
}