﻿using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Potions
{
    public class GrandManaPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grand Mana Potion");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 34;
            item.useTurn = true;
            item.maxStack = 30;
            item.healMana = 400;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useStyle = 2;
            item.UseSound = SoundID.Item3;
            item.consumable = true;
            item.potion = true;
            item.value = 50000;
            item.rare = 11;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SuperManaPotion);
            recipe.AddRecipeGroup("AAMod:AncientMaterials");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}