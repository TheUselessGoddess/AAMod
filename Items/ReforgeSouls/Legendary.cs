using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.ReforgeSouls
{
    public class Legendary : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 6;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Legendary Soul");
            Tooltip.SetDefault(
                @"Gives a weapon the ''Legendary'' prefix if it is possible
Right-click weapon with the soul to set prefix
Consumes in process");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(Terraria.ID.ItemID.FragmentSolar, 15);
            recipe.AddTile(Terraria.ID.TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}