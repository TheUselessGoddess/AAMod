using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class DuskBringer : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 39;
            item.melee = true;
            item.width = 124;
            item.height = 124;
            item.scale = 1.1f;
            item.maxStack = 1;
            item.useTime = 20;
            item.useAnimation = 20;
            item.knockBack = 4.5f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useTurn = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.value = Item.sellPrice(0, 6, 0, 0);
            item.rare = 3;
            item.shoot = mod.ProjectileType("DBP"); //put your Spear projectile name
            item.shootSpeed = 5f;
        }

        public override bool CanUseItem(Player player)
        {
            return
                player.ownedProjectileCounts[item.shoot] <
                1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dusk Bringer");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes() //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MoltenLance", 1);
            recipe.AddIngredient(null, "AncientPoker", 1);
            recipe.AddIngredient(null, "GrassSpear", 1);
            recipe.AddIngredient(ItemID.DarkLance, 1); //you need 1 DirtBlock
            recipe.AddTile(TileID.Anvils); //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}