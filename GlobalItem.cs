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
			if (Config.Client.Debug) {
				TooltipLine tooltip;

				Item ammo = new Item();
				ammo.SetDefaults(item.useAmmo);
				Projectile projectile = new Projectile();
				projectile.SetDefaults(item.shoot);
				Projectile ammoProjectile = new Projectile();
				ammoProjectile.SetDefaults(ammo.shoot);

				tooltip = new TooltipLine(Mod, "ItemID", "Item ID: " + item.GetItemID());
				tooltip.OverrideColor = Colors.RarityTrash;
				tooltips.Add(tooltip);

				tooltip = new TooltipLine(Mod, "ItemUseStyle", "Item Use Style ID: " + item.GetItemUseStyleID());
				tooltip.OverrideColor = Colors.RarityTrash;
				tooltips.Add(tooltip);

				if (ammo.type > 0) {
					tooltip = new TooltipLine(Mod, "ProjectileID", "Ammo ID: " + ammo.GetAmmoID());
					tooltip.OverrideColor = Colors.RarityTrash;
					tooltips.Add(tooltip);

					if (ammoProjectile.type > 0) {
						tooltip = new TooltipLine(Mod, "ProjectileID", "Ammo Projectile ID: " + ammoProjectile.GetProjectileID());
						tooltip.OverrideColor = Colors.RarityTrash;
						tooltips.Add(tooltip);

						tooltip = new TooltipLine(Mod, "ProjectileAIStyle", "Ammo Projectile AI Style ID: " + ammoProjectile.GetProjectileAIStyleID());
						tooltip.OverrideColor = Colors.RarityTrash;
						tooltips.Add(tooltip);
					}
				}

				if (projectile.type > 0 && ammoProjectile.type == 0) {

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
