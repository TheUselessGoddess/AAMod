﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Madness
{
    [AutoloadEquip(EquipType.Body)]
    public class MadnessPlate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Plate");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 18000;
            item.rare = 1;
            item.defense = 6;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MadnessFragment", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}