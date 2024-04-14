using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

using DALib.Functions;

namespace DALib
{
	public class GItem : GlobalItem {
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
			if (item.type == 0) { return; }
			if (Config.Client.TooltipDebug) {
				TooltipLine tooltip;

				Item ammo = new Item();
				ammo.SetDefaults(item.useAmmo);

				tooltip = new TooltipLine(Mod, "ItemID", "Item ID: " + item.GetItemID());
				tooltip.OverrideColor = Colors.RarityTrash;
				tooltips.Add(tooltip);

				tooltip = new TooltipLine(Mod, "ItemUseStyle", "Item Use Style ID: " + item.GetItemUseStyleID());
				tooltip.OverrideColor = Colors.RarityTrash;
				tooltips.Add(tooltip);

				if (ammo.type > 0) {
					tooltip = new TooltipLine(Mod, "AmmoID", "Ammo ID: " + ammo.GetAmmoID());
					tooltip.OverrideColor = Colors.RarityTrash;
					tooltips.Add(tooltip);

					Projectile ammoProjectile = new Projectile();
					ammoProjectile.SetDefaults(ammo.shoot);
					if (ammoProjectile.type > 0) {
						tooltip = new TooltipLine(Mod, "AmmoProjectileID", "Ammo Projectile ID: " + ammoProjectile.GetProjectileID());
						tooltip.OverrideColor = Colors.RarityTrash;
						tooltips.Add(tooltip);

						tooltip = new TooltipLine(Mod, "AmmoProjectileAIStyle", "Ammo Projectile AI Style ID: " + ammoProjectile.GetProjectileAIStyleID());
						tooltip.OverrideColor = Colors.RarityTrash;
						tooltips.Add(tooltip);
					}
				}

				Projectile projectile = new Projectile();
				projectile.SetDefaults(item.shoot);
				if (projectile.type > 0) {
					tooltip = new TooltipLine(Mod, "ProjectileID", "Projectile ID: " + projectile.GetProjectileID());
					tooltip.OverrideColor = Colors.RarityTrash;
					tooltips.Add(tooltip);

					tooltip = new TooltipLine(Mod, "ProjectileAIStyle", "Projectile AI Style ID: " + projectile.GetProjectileAIStyleID());
					tooltip.OverrideColor = Colors.RarityTrash;
					tooltips.Add(tooltip);
				}
			}
		}
	}
}
