using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Banners
{
	public class ThixxieBanner : ModItem
	{
		// The tooltip for this item is automatically assigned from .lang files
		public override void SetDefaults() {
			item.width = 32;
			item.height = 32;
			item.maxStack = 9999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.rare = 1;
			item.value = Item.buyPrice(1, 0, 0, 0);
			item.createTile = mod.TileType("ThixxieBanner");
			item.placeStyle = 0;
		}
	    public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FatPixieBanner", 20);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}