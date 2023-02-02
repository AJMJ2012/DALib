using Terraria;
using Terraria.ID;

namespace DALib.Checks;

public static class ItemChecks {
	public static bool IsAmmo(this Item item)
		=> (item.ammo != AmmoID.None);

	public static bool IsNormalAmmo(this Item item)
		=> IsAmmo(item) && !item.notAmmo;

	public static bool IsNonUseableAmmo(this Item item)
		=> IsAmmo(item) && item.useStyle == 0;

	public static bool IsUseableAmmo(this Item item)
		=> IsAmmo(item) && item.useStyle > 0;

	public static bool IsDestructiveTool(this Item item)
		=> (item.pick > 0 || item.hammer > 0 || item.axe > 0);

	public static bool IsTool(this Item item)
		=> (IsDestructiveTool(item) || item.fishingPole > 0 || item.tileWand > 0);
}
