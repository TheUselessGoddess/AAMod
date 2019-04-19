using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class TrueNightaxe : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 90;
            item.melee = true;
            item.width = 40;
            item.height = 40;

            item.useTime = 15;
            item.useAnimation = 20;
            item.pick = 205;
            item.useStyle = 1;
            item.knockBack = 1;
            item.value = 10;
            item.rare = 5;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Nightaxe");
        }

        public override void AddRecipes() //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "Nightaxe");
            recipe.AddIngredient(mod, "CorruptionCrystal");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}