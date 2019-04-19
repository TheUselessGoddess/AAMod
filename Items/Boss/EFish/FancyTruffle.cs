using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Boss.EFish
{
    public class FancyTruffle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fancy Truffle");
            Tooltip.SetDefault("Attracts a royal creature which flourishes in water & combat");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.BlessedApple);
            item.width = 32;
            item.height = 30;
            item.value = 5000000;
            item.rare = 11;
            item.mountType = mod.MountType("PrinceFishron");
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShrimpyTruffle);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}