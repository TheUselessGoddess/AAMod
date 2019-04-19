﻿using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Boss
{
    public class EXArmor : ModItem
    {
        internal static int type;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EX Armor Fragment");
            Tooltip.SetDefault("Used to make EX armor");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 333333;
            item.rare = 11;
            item.expert = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Main.DiscoColor; //GConstants.COLOR_RARITYN1;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Main.DiscoColor.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this, 3);
            recipe.AddRecipe();
        }
    }
}