using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DALib.Functions;

public static class TileFunctions {
#region TileID
	static Dictionary<int, string> tileTypeIDCache = new();

	static string GetVanillaTileName(int type) {
		string typeID = type.ToString();
		if (!tileTypeIDCache.TryGetValue(type, out typeID)) {
			typeID = FieldResolver.FindConstantName<short>(typeof(TileID), (short)type) ?? typeID;
			tileTypeIDCache.TryAdd(type, typeID);
		}
		return typeID;
	}

	static string GetTileID(int type)
		=> String.Format("{0}.{1}", GetTileMod(type), GetTileName(type));

	static string GetTileMod(int type) 
		=> TileLoader.GetTile(type).Mod.Name ?? "Terraria";

	static string GetTileName(int type)
		=> TileLoader.GetTile(type).Name ?? GetVanillaTileName(type);

	public static string GetTileID(this Tile tile)
		=> GetTileID(tile.TileType);

	public static string GetTileName(this Tile tile)
		=> GetTileName(tile.TileType);

	public static string GetTileMod(this Tile tile)
		=> GetTileMod(tile.TileType);
#endregion
}
