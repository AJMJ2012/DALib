using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

using DALib.Functions;

namespace DALib;

public class GItem : GlobalItem
{
	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
	{
		if (item.type == ItemID.None) { return; }
		if (Config.Client.TooltipDebug)
		{
			TooltipLine tooltip;

			Item ammo = new();
			ammo.SetDefaults(item.useAmmo);

            tooltip = new TooltipLine(Mod, "ItemID", "Item ID: " + item.GetItemID())
            {
                OverrideColor = Colors.RarityTrash
            };
            tooltips.Add(tooltip);

            tooltip = new TooltipLine(Mod, "ItemClass", "Item Class: " + item.GetItemClass())
            {
                OverrideColor = Colors.RarityTrash
            };
            tooltips.Add(tooltip);

            tooltip = new TooltipLine(Mod, "ItemUseStyle", "Item Use Style ID: " + item.GetItemUseStyleID())
            {
                OverrideColor = Colors.RarityTrash
            };
            tooltips.Add(tooltip);

			if (ammo.type > ItemID.None)
			{
                tooltip = new TooltipLine(Mod, "AmmoID", "Ammo ID: " + ammo.GetAmmoID())
                {
                    OverrideColor = Colors.RarityTrash
                };
                tooltips.Add(tooltip);

				Projectile ammoProjectile = new();
				ammoProjectile.SetDefaults(ammo.shoot);
				if (ammoProjectile.type > ProjectileID.None)
				{
                    tooltip = new TooltipLine(Mod, "AmmoProjectileID", "Ammo Projectile ID: " + ammoProjectile.GetProjectileID())
                    {
                        OverrideColor = Colors.RarityTrash
                    };
                    tooltips.Add(tooltip);

                    tooltip = new TooltipLine(Mod, "AmmoProjectileAIStyle", "Ammo Projectile AI Style ID: " + ammoProjectile.GetProjectileAIStyleID())
                    {
                        OverrideColor = Colors.RarityTrash
                    };
                    tooltips.Add(tooltip);
				}
			}

			Projectile projectile = new();
			projectile.SetDefaults(item.shoot);
			if (projectile.type > ProjectileID.None)
			{
                tooltip = new TooltipLine(Mod, "ProjectileID", "Projectile ID: " + projectile.GetProjectileID())
                {
                    OverrideColor = Colors.RarityTrash
                };
                tooltips.Add(tooltip);

                tooltip = new TooltipLine(Mod, "ProjectileAIStyle", "Projectile AI Style ID: " + projectile.GetProjectileAIStyleID())
                {
                    OverrideColor = Colors.RarityTrash
                };
                tooltips.Add(tooltip);
			}
		}
	}
}
