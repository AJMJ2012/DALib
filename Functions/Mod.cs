using Terraria.ModLoader;

namespace DALib.Functions;

public static class ModFunctions {
	public static string GetCurrentLoadingMod()
		=> Reflect.GetPropValue<Mod>(typeof(ModContent), "LoadingMod").Name;
}
