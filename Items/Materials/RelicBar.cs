using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class RelicBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Relic Bar");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.rare = 2;
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "VikingRelic", 2); //example of how to craft with a modded item
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}