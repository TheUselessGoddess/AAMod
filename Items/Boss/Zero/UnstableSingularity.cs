﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class UnstableSingularity : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unstable Singularity");
            Tooltip.SetDefault("Barely stable enough to hold");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 18));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Zero;
                }
            }
        }


        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Oblivion;
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.maxStack = 999;
            item.value = Item.buyPrice(1, 0, 0, 0);
        }

        // The following 2 methods are purely to show off these 2 hooks. Don't use them in your own code.


        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Oblivion.ToVector3() * 0.55f * Main.essScale);
        }
    }
}