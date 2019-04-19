using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Boss.Shen
{
    public class ShenTerratool_Axe : ModItem
    {
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 60;
            item.height = 54;
            item.useStyle = 1;
            item.useTime = 4;
            item.useAnimation = 16;
            item.tileBoost += 25;
            item.knockBack = 3;
            item.value = 1000000;
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 120;
            item.axe = 200;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terratool");
            Tooltip.SetDefault("Right Click to change tool types");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Shen;
                }
            }
        }

        public override bool CanRightClick()
        {
            return true;
        }


        public override void RightClick(Player player)
        {
            byte pre = item.prefix;
            item.TurnToAir();
            int itemID = Item.NewItem((int) player.position.X, (int) player.position.Y, player.width, player.height,
                mod.ItemType("ShenTerratool_Hammer"), 1, false, pre, false, false);
            if (Main.netMode == 1)
            {
                NetMessage.SendData(21, -1, -1, null, itemID, 1f, 0f, 0f, 0, 0, 0);
            }
        }
    }
}