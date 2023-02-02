using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DALib.Functions;

public static class ItemFunctions {
#region ItemID
	static Dictionary<int, string> itemTypeIDCache = new();

	static string GetVanillaItemName(int type) {
		string typeID = type.ToString();
		if (!itemTypeIDCache.TryGetValue(type, out typeID)) {
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

	public static string GetItemID(this Item item)
		=> GetItemID(item.type);

	public static string GetItemName(this Item item)
		=> GetItemName(item.type);

	public static string GetItemMod(this Item item)
		=> GetItemMod(item.type);
#endregion
#region ItemUseStyleID
	static Dictionary<int, string> itemUseStyleIDCache = new();

	static string GetItemUseStyleID(int useStyle) {
		string useStyleID = useStyle.ToString();
		if (!itemUseStyleIDCache.TryGetValue(useStyle, out useStyleID)) {
			useStyleID = FieldResolver.FindConstantName<int>(typeof(ItemUseStyleID), (int)useStyle) ?? useStyleID;
			itemUseStyleIDCache.TryAdd(useStyle, useStyleID);
		}
		return String.IsNullOrEmpty(useStyleID) ? useStyle.ToString() : useStyleID;
	}

	public static string GetItemUseStyleID(this Item item)
		=> GetItemUseStyleID(item.useStyle);
#endregion
#region AmmoID
	static Dictionary<int, string> ammoTypeIDCache = new();

	static string GetVanillaAmmoName(int type) {
		string typeID = type.ToString();
		if (!ammoTypeIDCache.TryGetValue(type, out typeID)) {
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
}