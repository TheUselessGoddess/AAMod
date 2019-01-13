using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Armor.Dev.Alphakip
{
    [AutoloadEquip(EquipType.Head)]
	public class FishDiverMaskA : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Fish Diver's Mask");
            Tooltip.SetDefault(@"So I heard you like mudkips
20% increased Melee/Ranged damage & critical strike chance
13% increased damage resistance and melee speed
Allows for underwater breathing");

        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(39, 115, 189);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.rare = 9;
            item.vanity = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.breath = player.breathMax;
            player.meleeDamage *= 1.2f;
            player.endurance *= 1.13f;
            player.meleeSpeed *= 1.13f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("FishDiverJacketA") && legs.type == mod.ItemType("FishDiverBootsA");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"'Hosing time.'
All of your attacks inflict wet to non-boss enemies
Grants uninhibited liquid movement
The Infinity Gauntlet is now at it's max potential
You gain a fishy companion";
            player.GetModPlayer<AAPlayer>(mod).Alpha = true;
            player.GetModPlayer<AAPlayer>(mod).Mudkip = true;
            player.ignoreWater = true;
        }
    }
}